using Npgsql;

namespace Pgenie.Artifacts.Myspace.MusicCatalogue;

/// <summary>
/// Implemented by each query's parameter+result class. Provides a uniform way
/// to prepare and execute statements against an <see cref="NpgsqlConnection"/>.
/// </summary>
/// <typeparam name="TResult">
/// The result type returned by <see cref="DecodeReader"/> or
/// <see cref="DecodeAffectedRows"/>.
/// </typeparam>
/// <remarks>
/// Generated from SQL queries using <a href="https://pgenie.io">pGenie</a>.
/// </remarks>
public interface IStatement<TResult>
{
    /// <summary>
    /// The SQL text for this statement. Parameter placeholders use the
    /// <c>@paramName</c> syntax supported by Npgsql.
    /// </summary>
    string Sql { get; }

    /// <summary>Bind parameters to the command.</summary>
    void BindParams(NpgsqlCommand command);

    /// <summary>
    /// Whether this statement returns rows (i.e. is a <c>SELECT</c> or contains
    /// a <c>RETURNING</c> clause).
    /// </summary>
    bool ReturnsRows { get; }

    /// <summary>
    /// Decode a data reader into the statement's result type.
    /// </summary>
    /// <param name="reader">
    /// The data reader positioned before the first row.
    /// </param>
    TResult DecodeReader(NpgsqlDataReader reader);

    /// <summary>Decode an affected-row count into the statement's result type.</summary>
    /// <param name="affectedRows">The number of rows affected.</param>
    TResult DecodeAffectedRows(long affectedRows);

    /// <summary>Execute this statement using the provided connection.</summary>
    /// <param name="connection">The Npgsql connection to use.</param>
    /// <returns>The decoded statement result.</returns>
    TResult Execute(NpgsqlConnection connection)
    {
        using var command = new NpgsqlCommand(Sql, connection);
        BindParams(command);
        if (ReturnsRows)
        {
            using var reader = command.ExecuteReader();
            return DecodeReader(reader);
        }
        else
        {
            var affected = (long)command.ExecuteNonQuery();
            return DecodeAffectedRows(affected);
        }
    }
}
