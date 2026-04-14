using Npgsql;
using Pgenie.Artifacts.Myspace.MusicCatalogue.Types;

namespace Pgenie.Artifacts.Myspace.MusicCatalogue.Statements;

/// <summary>
/// Type-safe binding for the <c>insert_album</c> query.
/// </summary>
/// <param name="Name">Maps to <c>$name</c> in the template.</param>
/// <param name="Released">Maps to <c>$released</c> in the template.</param>
/// <param name="Format">Maps to <c>$format</c> in the template.</param>
/// <param name="Recording">Maps to <c>$recording</c> in the template.</param>
/// <remarks>
/// SQL template:
/// <code><![CDATA[
/// insert into album (name, released, format, recording)
/// values ($name, $released, $format, $recording)
/// returning id
/// ]]></code>
/// Source path: <c>./queries/insert_album.sql</c>
/// <para>
/// Generated from SQL queries using <a href="https://pgenie.io">pGenie</a>.
/// </para>
/// </remarks>
public record InsertAlbum(
    string Name,
    DateOnly Released,
    AlbumFormat Format,
    RecordingInfo Recording) : IStatement<InsertAlbum.Result>
{
    // -------------------------------------------------------------------------
    // Result type
    // -------------------------------------------------------------------------

    /// <summary>Result of the <see cref="InsertAlbum"/> statement.</summary>
    /// <param name="Id">Maps to the <c>id</c> result column.</param>
    public record Result(long Id);

    // -------------------------------------------------------------------------
    // Statement implementation
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    public string Sql =>
        """
        insert into album (name, released, format, recording)
        values (@name, @released, @format, @recording)
        returning id
        """;

    /// <inheritdoc/>
    public void BindParams(NpgsqlCommand command)
    {
        command.Parameters.AddWithValue("name", Name);
        command.Parameters.AddWithValue("released", Released);
        command.Parameters.AddWithValue("format", Format);
        command.Parameters.AddWithValue("recording", Recording);
    }

    /// <inheritdoc/>
    public bool ReturnsRows => true;

    /// <inheritdoc/>
    public Result DecodeReader(NpgsqlDataReader reader)
    {
        reader.Read();
        return new Result(Id: reader.GetInt64(0));
    }

    /// <inheritdoc/>
    public Result DecodeAffectedRows(long affectedRows) =>
        throw new InvalidOperationException(
            $"{nameof(InsertAlbum)} returns rows; {nameof(DecodeAffectedRows)} is not applicable.");
}
