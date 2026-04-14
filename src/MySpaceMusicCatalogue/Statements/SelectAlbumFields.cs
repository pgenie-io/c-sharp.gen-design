using Npgsql;
using Pgenie.Artifacts.Myspace.MusicCatalogue.Types;

namespace Pgenie.Artifacts.Myspace.MusicCatalogue.Statements;

/// <summary>
/// Type-safe binding for the <c>select_album_fields</c> query.
/// </summary>
/// <param name="IncludeName">Maps to <c>$include_name</c> in the template.</param>
/// <param name="IncludeReleased">Maps to <c>$include_released</c> in the template.</param>
/// <param name="IncludeFormat">Maps to <c>$include_format</c> in the template.</param>
/// <param name="IncludeRecording">Maps to <c>$include_recording</c> in the template.</param>
/// <param name="IncludeTracks">Maps to <c>$include_tracks</c> in the template.</param>
/// <param name="IncludeDisc">Maps to <c>$include_disc</c> in the template.</param>
/// <param name="Id">Maps to <c>$id</c> in the template.</param>
/// <remarks>
/// SQL template:
/// <code><![CDATA[
/// -- Demonstrates static query equivalent of dynamic field selection.
/// -- Boolean flags control which fields are included in the result,
/// -- returning NULL for fields the caller opts out of.
/// SELECT
///   album.id,
///   CASE WHEN $include_name      THEN album.name      END AS name,
///   CASE WHEN $include_released  THEN album.released  END AS released,
///   CASE WHEN $include_format    THEN album.format    END AS format,
///   CASE WHEN $include_recording THEN album.recording END AS recording,
///   CASE WHEN $include_tracks    THEN album.tracks    END AS tracks,
///   CASE WHEN $include_disc      THEN album.disc      END AS disc
/// FROM album
/// WHERE album.id = $id
/// ]]></code>
/// Source path: <c>./queries/select_album_fields.sql</c>
/// <para>
/// Generated from SQL queries using <a href="https://pgenie.io">pGenie</a>.
/// </para>
/// </remarks>
public record SelectAlbumFields(
    bool IncludeName,
    bool IncludeReleased,
    bool IncludeFormat,
    bool IncludeRecording,
    bool IncludeTracks,
    bool IncludeDisc,
    long Id) : IStatement<IReadOnlyList<SelectAlbumFields.ResultRow>>
{
    // -------------------------------------------------------------------------
    // Result type
    // -------------------------------------------------------------------------

    /// <summary>Row of the result returned by <see cref="SelectAlbumFields"/>.</summary>
    /// <param name="Id">Maps to the <c>id</c> result column.</param>
    /// <param name="Name">Maps to the <c>name</c> result column. Nullable.</param>
    /// <param name="Released">Maps to the <c>released</c> result column. Nullable.</param>
    /// <param name="Format">Maps to the <c>format</c> result column. Nullable.</param>
    /// <param name="Recording">Maps to the <c>recording</c> result column. Nullable.</param>
    /// <param name="Tracks">Maps to the <c>tracks</c> result column. Nullable.</param>
    /// <param name="Disc">Maps to the <c>disc</c> result column. Nullable.</param>
    public record ResultRow(
        long Id,
        string? Name,
        DateOnly? Released,
        AlbumFormat? Format,
        RecordingInfo? Recording,
        TrackInfo[]? Tracks,
        DiscInfo? Disc);

    // -------------------------------------------------------------------------
    // Statement implementation
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    public string Sql =>
        """
        -- Demonstrates static query equivalent of dynamic field selection.
        -- Boolean flags control which fields are included in the result,
        -- returning NULL for fields the caller opts out of.
        SELECT
          album.id,
          CASE WHEN @include_name      THEN album.name      END AS name,
          CASE WHEN @include_released  THEN album.released  END AS released,
          CASE WHEN @include_format    THEN album.format    END AS format,
          CASE WHEN @include_recording THEN album.recording END AS recording,
          CASE WHEN @include_tracks    THEN album.tracks    END AS tracks,
          CASE WHEN @include_disc      THEN album.disc      END AS disc
        FROM album
        WHERE album.id = @id
        """;

    /// <inheritdoc/>
    public void BindParams(NpgsqlCommand command)
    {
        command.Parameters.AddWithValue("include_name", IncludeName);
        command.Parameters.AddWithValue("include_released", IncludeReleased);
        command.Parameters.AddWithValue("include_format", IncludeFormat);
        command.Parameters.AddWithValue("include_recording", IncludeRecording);
        command.Parameters.AddWithValue("include_tracks", IncludeTracks);
        command.Parameters.AddWithValue("include_disc", IncludeDisc);
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
                Name: reader.IsDBNull(1) ? null : reader.GetString(1),
                Released: reader.IsDBNull(2) ? null : reader.GetFieldValue<DateOnly>(2),
                Format: reader.IsDBNull(3) ? null : reader.GetFieldValue<AlbumFormat>(3),
                Recording: reader.IsDBNull(4) ? null : reader.GetFieldValue<RecordingInfo>(4),
                Tracks: reader.IsDBNull(5) ? null : reader.GetFieldValue<TrackInfo[]>(5),
                Disc: reader.IsDBNull(6) ? null : reader.GetFieldValue<DiscInfo>(6)));
        }
        return output;
    }

    /// <inheritdoc/>
    public IReadOnlyList<ResultRow> DecodeAffectedRows(long affectedRows) =>
        throw new InvalidOperationException(
            $"{nameof(SelectAlbumFields)} returns rows; {nameof(DecodeAffectedRows)} is not applicable.");
}
