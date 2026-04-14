using Npgsql;
using Pgenie.Artifacts.Myspace.MusicCatalogue.Types;

namespace Pgenie.Artifacts.Myspace.MusicCatalogue.Statements;

/// <summary>
/// Type-safe binding for the <c>select_album_by_name</c> query.
/// </summary>
/// <param name="Name">Maps to <c>$name</c> in the template.</param>
/// <remarks>
/// SQL template:
/// <code><![CDATA[
/// select
///   id,
///   name,
///   released,
///   format,
///   recording
/// from album
/// where name = $name
/// ]]></code>
/// Source path: <c>./queries/select_album_by_name.sql</c>
/// <para>
/// Generated from SQL queries using <a href="https://pgenie.io">pGenie</a>.
/// </para>
/// </remarks>
public record SelectAlbumByName(
    string Name) : IStatement<IReadOnlyList<SelectAlbumByName.ResultRow>>
{
    // -------------------------------------------------------------------------
    // Result type
    // -------------------------------------------------------------------------

    /// <summary>Row of the result returned by <see cref="SelectAlbumByName"/>.</summary>
    /// <param name="Id">Maps to the <c>id</c> result column.</param>
    /// <param name="Name">Maps to the <c>name</c> result column.</param>
    /// <param name="Released">Maps to the <c>released</c> result column. Nullable.</param>
    /// <param name="Format">Maps to the <c>format</c> result column. Nullable.</param>
    /// <param name="Recording">Maps to the <c>recording</c> result column. Nullable.</param>
    public record ResultRow(
        long Id,
        string Name,
        DateOnly? Released,
        AlbumFormat? Format,
        RecordingInfo? Recording);

    // -------------------------------------------------------------------------
    // Statement implementation
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    public string Sql =>
        """
        select
          id,
          name,
          released,
          format,
          recording
        from album
        where name = @name
        """;

    /// <inheritdoc/>
    public void BindParams(NpgsqlCommand command)
    {
        command.Parameters.AddWithValue("name", Name);
    }

    /// <inheritdoc/>
    public bool ReturnsRows => true;

    /// <inheritdoc/>
    public IReadOnlyList<ResultRow> DecodeReader(NpgsqlDataReader reader)
    {
        var output = new List<ResultRow>();
        while (reader.Read())
        {
            output.Add(new ResultRow(
                Id: reader.GetInt64(0),
                Name: reader.GetString(1),
                Released: reader.IsDBNull(2) ? null : reader.GetFieldValue<DateOnly>(2),
                Format: reader.IsDBNull(3) ? null : reader.GetFieldValue<AlbumFormat>(3),
                Recording: reader.IsDBNull(4) ? null : reader.GetFieldValue<RecordingInfo>(4)));
        }
        return output;
    }

    /// <inheritdoc/>
    public IReadOnlyList<ResultRow> DecodeAffectedRows(long affectedRows) =>
        throw new InvalidOperationException(
            $"{nameof(SelectAlbumByName)} returns rows; {nameof(DecodeAffectedRows)} is not applicable.");
}
