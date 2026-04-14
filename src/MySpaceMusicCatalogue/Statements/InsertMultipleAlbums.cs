using Npgsql;
using Pgenie.Artifacts.Myspace.MusicCatalogue.Types;

namespace Pgenie.Artifacts.Myspace.MusicCatalogue.Statements;

/// <summary>
/// Type-safe binding for the <c>insert_multiple_albums</c> query.
/// </summary>
/// <param name="Name">Maps to <c>$name</c> in the template.</param>
/// <param name="Released">Maps to <c>$released</c> in the template.</param>
/// <param name="Format">Maps to <c>$format</c> in the template.</param>
/// <remarks>
/// SQL template:
/// <code><![CDATA[
/// -- This is an example of a bulk-insert (batch-insert) technique.
/// -- We pass in all fields as arrays of the same size, and we unnest it to insert multiple rows at once.
/// insert into album (name, released, format)
/// select *
/// from unnest(
///   $name::text[],
///   $released::date[],
///   $format::album_format[]
/// )
/// returning id
/// ]]></code>
/// Source path: <c>./queries/insert_multiple_albums.sql</c>
/// <para>
/// Generated from SQL queries using <a href="https://pgenie.io">pGenie</a>.
/// </para>
/// </remarks>
public record InsertMultipleAlbums(
    IReadOnlyList<string> Name,
    IReadOnlyList<DateOnly> Released,
    IReadOnlyList<AlbumFormat> Format) : IStatement<IReadOnlyList<InsertMultipleAlbums.ResultRow>>
{
    // -------------------------------------------------------------------------
    // Result type
    // -------------------------------------------------------------------------

    /// <summary>Row of the result returned by <see cref="InsertMultipleAlbums"/>.</summary>
    /// <param name="Id">Maps to the <c>id</c> result column.</param>
    public record ResultRow(long Id);

    // -------------------------------------------------------------------------
    // Statement implementation
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    public string Sql =>
        """
        -- This is an example of a bulk-insert (batch-insert) technique.
        -- We pass in all fields as arrays of the same size, and we unnest it to insert multiple rows at once.
        insert into album (name, released, format)
        select *
        from unnest(
          @name::text[],
          @released::date[],
          @format::album_format[]
        )
        returning id
        """;

    /// <inheritdoc/>
    public void BindParams(NpgsqlCommand command)
    {
        command.Parameters.AddWithValue("name", Name.ToArray());
        command.Parameters.AddWithValue("released", Released.ToArray());
        command.Parameters.AddWithValue("format", Format.ToArray());
    }

    /// <inheritdoc/>
    public bool ReturnsRows => true;

    /// <inheritdoc/>
    public IReadOnlyList<ResultRow> DecodeReader(NpgsqlDataReader reader)
    {
        var output = new List<ResultRow>();
        while (reader.Read())
            output.Add(new ResultRow(Id: reader.GetInt64(0)));
        return output;
    }

    /// <inheritdoc/>
    public IReadOnlyList<ResultRow> DecodeAffectedRows(long affectedRows) =>
        throw new InvalidOperationException(
            $"{nameof(InsertMultipleAlbums)} returns rows; {nameof(DecodeAffectedRows)} is not applicable.");
}
