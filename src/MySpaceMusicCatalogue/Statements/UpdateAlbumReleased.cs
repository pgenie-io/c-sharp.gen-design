using Npgsql;

namespace Pgenie.Artifacts.Myspace.MusicCatalogue.Statements;

/// <summary>
/// Type-safe binding for the <c>update_album_released</c> query.
/// </summary>
/// <param name="Released">Maps to <c>$released</c> in the template. Nullable.</param>
/// <param name="Id">Maps to <c>$id</c> in the template.</param>
/// <remarks>
/// SQL template:
/// <code><![CDATA[
/// update album
/// set released = $released
/// where id = $id
/// ]]></code>
/// Source path: <c>./queries/update_album_released.sql</c>
/// <para>
/// Generated from SQL queries using <a href="https://pgenie.io">pGenie</a>.
/// </para>
/// </remarks>
public record UpdateAlbumReleased(
    DateOnly? Released,
    long Id) : IStatement<long>
{
    // -------------------------------------------------------------------------
    // Statement implementation
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    public string Sql =>
        """
        update album
        set released = @released
        where id = @id
        """;

    /// <inheritdoc/>
    public void BindParams(NpgsqlCommand command)
    {
        command.Parameters.AddWithValue("released", (object?)Released ?? DBNull.Value);
        command.Parameters.AddWithValue("id", Id);
    }

    /// <inheritdoc/>
    public bool ReturnsRows => false;

    /// <summary>Returns the number of rows affected by the statement.</summary>
    public long DecodeAffectedRows(long affectedRows) => affectedRows;

    /// <inheritdoc/>
    public long DecodeReader(NpgsqlDataReader reader) =>
        throw new InvalidOperationException(
            $"{nameof(UpdateAlbumReleased)} does not return rows; {nameof(DecodeReader)} is not applicable.");
}
