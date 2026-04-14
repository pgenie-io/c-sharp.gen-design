using Npgsql;
using Pgenie.Artifacts.Myspace.MusicCatalogue.Types;

namespace Pgenie.Artifacts.Myspace.MusicCatalogue.Statements;

/// <summary>
/// Type-safe binding for the <c>select_album_with_tracks</c> query.
/// </summary>
/// <param name="Id">Maps to <c>$id</c> in the template.</param>
/// <remarks>
/// SQL template:
/// <code><![CDATA[
/// select id, name, tracks, disc
/// from album
/// where id = $id
/// ]]></code>
/// Source path: <c>./queries/select_album_with_tracks.sql</c>
/// <para>
/// Generated from SQL queries using <a href="https://pgenie.io">pGenie</a>.
/// </para>
/// </remarks>
public record SelectAlbumWithTracks(
    long Id) : IStatement<IReadOnlyList<SelectAlbumWithTracks.ResultRow>>
{
    // -------------------------------------------------------------------------
    // Result type
    // -------------------------------------------------------------------------

    /// <summary>Row of the result returned by <see cref="SelectAlbumWithTracks"/>.</summary>
    /// <param name="Id">Maps to the <c>id</c> result column.</param>
    /// <param name="Name">Maps to the <c>name</c> result column.</param>
    /// <param name="Tracks">Maps to the <c>tracks</c> result column.</param>
    /// <param name="Disc">Maps to the <c>disc</c> result column. Nullable.</param>
    public record ResultRow(
        long Id,
        string Name,
        TrackInfo[] Tracks,
        DiscInfo? Disc);

    // -------------------------------------------------------------------------
    // Statement implementation
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    public string Sql =>
        """
        select id, name, tracks, disc
        from album
        where id = @id
        """;

    /// <inheritdoc/>
    public void BindParams(NpgsqlCommand command)
    {
        command.Parameters.AddWithValue("id", Id);
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
                Tracks: reader.GetFieldValue<TrackInfo[]>(2),
                Disc: reader.IsDBNull(3) ? null : reader.GetFieldValue<DiscInfo>(3)));
        }
        return output;
    }

    /// <inheritdoc/>
    public IReadOnlyList<ResultRow> DecodeAffectedRows(long affectedRows) =>
        throw new InvalidOperationException(
            $"{nameof(SelectAlbumWithTracks)} returns rows; {nameof(DecodeAffectedRows)} is not applicable.");
}
