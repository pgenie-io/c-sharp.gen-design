using Npgsql;
using Pgenie.Artifacts.Myspace.MusicCatalogue.Types;

namespace Pgenie.Artifacts.Myspace.MusicCatalogue.Statements;

/// <summary>
/// Type-safe binding for the <c>select_album_by_id</c> query.
/// </summary>
/// <param name="Id">Maps to <c>$id</c> in the template. Nullable.</param>
/// <remarks>
/// SQL template:
/// <code><![CDATA[
/// -- Example of a query selecting 0 or 1 row.
/// select *
/// from album
/// where id = $id
/// limit 1
/// ]]></code>
/// Source path: <c>./queries/select_album_by_id.sql</c>
/// <para>
/// Generated from SQL queries using <a href="https://pgenie.io">pGenie</a>.
/// </para>
/// </remarks>
public record SelectAlbumById(
    long? Id) : IStatement<SelectAlbumById.ResultRow?>
{
    // -------------------------------------------------------------------------
    // Result type
    // -------------------------------------------------------------------------

    /// <summary>
    /// Result row of the <see cref="SelectAlbumById"/> statement.
    /// Returns <see langword="null"/> when no matching row is found.
    /// </summary>
    /// <param name="Id">Maps to the <c>id</c> result column.</param>
    /// <param name="Name">Maps to the <c>name</c> result column.</param>
    /// <param name="Released">Maps to the <c>released</c> result column. Nullable.</param>
    /// <param name="Format">Maps to the <c>format</c> result column. Nullable.</param>
    /// <param name="Recording">Maps to the <c>recording</c> result column. Nullable.</param>
    /// <param name="Tracks">Maps to the <c>tracks</c> result column. Nullable.</param>
    /// <param name="Disc">Maps to the <c>disc</c> result column. Nullable.</param>
    public record ResultRow(
        long Id,
        string Name,
        DateOnly? Released,
        AlbumFormat? Format,
        RecordingInfo? Recording,
        TrackInfo?[]? Tracks,
        DiscInfo? Disc);

    // -------------------------------------------------------------------------
    // Statement implementation
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    public string Sql =>
        """
        -- Example of a query selecting 0 or 1 row.
        select *
        from album
        where id = @id
        limit 1
        """;

    /// <inheritdoc/>
    public void BindParams(NpgsqlCommand command)
    {
        command.Parameters.AddWithValue("id", (object?)Id ?? DBNull.Value);
    }

    /// <inheritdoc/>
    public bool ReturnsRows => true;

    /// <inheritdoc/>
    public ResultRow? DecodeReader(NpgsqlDataReader reader)
    {
        if (!reader.Read()) return null;

        return new ResultRow(
            Id: reader.GetInt64(0),
            Name: reader.GetString(1),
            Released: reader.IsDBNull(2) ? null : reader.GetFieldValue<DateOnly>(2),
            Format: reader.IsDBNull(3) ? null : reader.GetFieldValue<AlbumFormat>(3),
            Recording: reader.IsDBNull(4) ? null : reader.GetFieldValue<RecordingInfo>(4),
            Tracks: reader.IsDBNull(5) ? null : reader.GetFieldValue<TrackInfo?[]>(5),
            Disc: reader.IsDBNull(6) ? null : reader.GetFieldValue<DiscInfo>(6));
    }

    /// <inheritdoc/>
    public ResultRow? DecodeAffectedRows(long affectedRows) =>
        throw new InvalidOperationException(
            $"{nameof(SelectAlbumById)} returns rows; {nameof(DecodeAffectedRows)} is not applicable.");
}
