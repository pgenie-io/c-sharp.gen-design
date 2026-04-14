using Npgsql;

namespace Pgenie.Artifacts.Myspace.MusicCatalogue.Statements;

/// <summary>
/// Type-safe binding for the <c>select_genre_by_artist</c> query.
/// </summary>
/// <param name="Artist">Maps to <c>$artist</c> in the template.</param>
/// <remarks>
/// SQL template:
/// <code><![CDATA[
/// select id, genre.name
/// from genre
/// left join album_genre on album_genre.genre = genre.id
/// left join album_artist on album_artist.album = album_genre.album
/// where album_artist.artist = $artist
/// ]]></code>
/// Source path: <c>./queries/select_genre_by_artist.sql</c>
/// <para>
/// Generated from SQL queries using <a href="https://pgenie.io">pGenie</a>.
/// </para>
/// </remarks>
public record SelectGenreByArtist(
    int Artist) : IStatement<IReadOnlyList<SelectGenreByArtist.ResultRow>>
{
    // -------------------------------------------------------------------------
    // Result type
    // -------------------------------------------------------------------------

    /// <summary>Row of the result returned by <see cref="SelectGenreByArtist"/>.</summary>
    /// <param name="Id">Maps to the <c>id</c> result column.</param>
    /// <param name="Name">Maps to the <c>name</c> result column.</param>
    public record ResultRow(int Id, string Name);

    // -------------------------------------------------------------------------
    // Statement implementation
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    public string Sql =>
        """
        select id, genre.name
        from genre
        left join album_genre on album_genre.genre = genre.id
        left join album_artist on album_artist.album = album_genre.album
        where album_artist.artist = @artist
        """;

    /// <inheritdoc/>
    public void BindParams(NpgsqlCommand command)
    {
        command.Parameters.AddWithValue("artist", Artist);
    }

    /// <inheritdoc/>
    public bool ReturnsRows => true;

    /// <inheritdoc/>
    public IReadOnlyList<ResultRow> DecodeReader(NpgsqlDataReader reader)
    {
        var output = new List<ResultRow>();
        while (reader.Read())
            output.Add(new ResultRow(Id: reader.GetInt32(0), Name: reader.GetString(1)));
        return output;
    }

    /// <inheritdoc/>
    public IReadOnlyList<ResultRow> DecodeAffectedRows(long affectedRows) =>
        throw new InvalidOperationException(
            $"{nameof(SelectGenreByArtist)} returns rows; {nameof(DecodeAffectedRows)} is not applicable.");
}
