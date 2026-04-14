using Npgsql;
using Pgenie.Artifacts.Myspace.MusicCatalogue.Types;

namespace Pgenie.Artifacts.Myspace.MusicCatalogue.Statements;

/// <summary>
/// Type-safe binding for the <c>update_album_recording_returning</c> query.
/// </summary>
/// <param name="Recording">Maps to <c>$recording</c> in the template. Nullable.</param>
/// <param name="Id">Maps to <c>$id</c> in the template.</param>
/// <remarks>
/// SQL template:
/// <code><![CDATA[
/// -- Update album recording information
/// update album
/// set recording = $recording
/// where id = $id
/// returning *
/// ]]></code>
/// Source path: <c>./queries/update_album_recording_returning.sql</c>
/// <para>
/// Generated from SQL queries using <a href="https://pgenie.io">pGenie</a>.
/// </para>
/// </remarks>
public record UpdateAlbumRecordingReturning(
    RecordingInfo? Recording,
    long Id) : IStatement<IReadOnlyList<UpdateAlbumRecordingReturning.ResultRow>>
{
    // -------------------------------------------------------------------------
    // Result type
    // -------------------------------------------------------------------------

    /// <summary>Row of the result returned by <see cref="UpdateAlbumRecordingReturning"/>.</summary>
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
        TrackInfo[]? Tracks,
        DiscInfo? Disc);

    // -------------------------------------------------------------------------
    // Statement implementation
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    public string Sql =>
        """
        -- Update album recording information
        update album
        set recording = @recording
        where id = @id
        returning *
        """;

    /// <inheritdoc/>
    public void BindParams(NpgsqlCommand command)
    {
        command.Parameters.AddWithValue("recording", (object?)Recording ?? DBNull.Value);
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
            $"{nameof(UpdateAlbumRecordingReturning)} returns rows; {nameof(DecodeAffectedRows)} is not applicable.");
}
