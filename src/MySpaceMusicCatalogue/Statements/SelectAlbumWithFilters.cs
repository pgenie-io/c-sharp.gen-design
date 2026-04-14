using Npgsql;
using Pgenie.Artifacts.Myspace.MusicCatalogue.Types;

namespace Pgenie.Artifacts.Myspace.MusicCatalogue.Statements;

/// <summary>
/// Type-safe binding for the <c>select_album_with_filters</c> query.
/// </summary>
/// <param name="ArtistName">Maps to <c>$artist_name</c> in the template. Nullable.</param>
/// <param name="GenreName">Maps to <c>$genre_name</c> in the template. Nullable.</param>
/// <param name="Format">Maps to <c>$format</c> in the template. Nullable.</param>
/// <param name="ReleasedAfter">Maps to <c>$released_after</c> in the template. Nullable.</param>
/// <param name="NameLike">Maps to <c>$name_like</c> in the template. Nullable.</param>
/// <param name="OrderByName">Maps to <c>$order_by_name</c> in the template.</param>
/// <param name="OrderByReleased">Maps to <c>$order_by_released</c> in the template.</param>
/// <remarks>
/// SQL template:
/// <code><![CDATA[
/// SELECT
///   album.id,
///   album.name,
///   album.released,
///   album.format
/// FROM album
/// LEFT JOIN album_artist ON album_artist.album = album.id
/// LEFT JOIN artist ON artist.id = album_artist.artist
/// LEFT JOIN album_genre ON album_genre.album = album.id
/// LEFT JOIN genre ON genre.id = album_genre.genre
/// WHERE
///   ($artist_name::text IS NULL OR artist.name = $artist_name)
///   AND ($genre_name::text IS NULL OR genre.name = $genre_name)
///   AND ($format::album_format IS NULL OR album.format = $format)
///   AND ($released_after::timestamp IS NULL OR album.released >= $released_after)
///   AND ($name_like::text IS NULL OR album.name LIKE $name_like)
/// ORDER BY
///   CASE WHEN $order_by_name THEN album.name END ASC,
///   CASE WHEN $order_by_released THEN album.released END DESC
/// ]]></code>
/// Source path: <c>./queries/select_album_with_filters.sql</c>
/// <para>
/// Generated from SQL queries using <a href="https://pgenie.io">pGenie</a>.
/// </para>
/// </remarks>
public record SelectAlbumWithFilters(
    string? ArtistName,
    string? GenreName,
    AlbumFormat? Format,
    DateTime? ReleasedAfter,
    string? NameLike,
    bool OrderByName,
    bool OrderByReleased) : IStatement<IReadOnlyList<SelectAlbumWithFilters.ResultRow>>
{
    // -------------------------------------------------------------------------
    // Result type
    // -------------------------------------------------------------------------

    /// <summary>Row of the result returned by <see cref="SelectAlbumWithFilters"/>.</summary>
    /// <param name="Id">Maps to the <c>id</c> result column.</param>
    /// <param name="Name">Maps to the <c>name</c> result column.</param>
    /// <param name="Released">Maps to the <c>released</c> result column. Nullable.</param>
    /// <param name="Format">Maps to the <c>format</c> result column. Nullable.</param>
    public record ResultRow(
        long Id,
        string Name,
        DateOnly? Released,
        AlbumFormat? Format);

    // -------------------------------------------------------------------------
    // Statement implementation
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    public string Sql =>
        """
        SELECT
          album.id,
          album.name,
          album.released,
          album.format
        FROM album
        LEFT JOIN album_artist ON album_artist.album = album.id
        LEFT JOIN artist ON artist.id = album_artist.artist
        LEFT JOIN album_genre ON album_genre.album = album.id
        LEFT JOIN genre ON genre.id = album_genre.genre
        WHERE
          (@artist_name::text IS NULL OR artist.name = @artist_name)
          AND (@genre_name::text IS NULL OR genre.name = @genre_name)
          AND (@format::album_format IS NULL OR album.format = @format)
          AND (@released_after::timestamp IS NULL OR album.released >= @released_after)
          AND (@name_like::text IS NULL OR album.name LIKE @name_like)
        ORDER BY
          CASE WHEN @order_by_name THEN album.name END ASC,
          CASE WHEN @order_by_released THEN album.released END DESC
        """;

    /// <inheritdoc/>
    public void BindParams(NpgsqlCommand command)
    {
        command.Parameters.AddWithValue("artist_name", (object?)ArtistName ?? DBNull.Value);
        command.Parameters.AddWithValue("genre_name", (object?)GenreName ?? DBNull.Value);
        command.Parameters.AddWithValue("format", (object?)Format ?? DBNull.Value);
        command.Parameters.AddWithValue("released_after", (object?)ReleasedAfter ?? DBNull.Value);
        command.Parameters.AddWithValue("name_like", (object?)NameLike ?? DBNull.Value);
        command.Parameters.AddWithValue("order_by_name", OrderByName);
        command.Parameters.AddWithValue("order_by_released", OrderByReleased);
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
                Format: reader.IsDBNull(3) ? null : reader.GetFieldValue<AlbumFormat>(3)));
        }
        return output;
    }

    /// <inheritdoc/>
    public IReadOnlyList<ResultRow> DecodeAffectedRows(long affectedRows) =>
        throw new InvalidOperationException(
            $"{nameof(SelectAlbumWithFilters)} returns rows; {nameof(DecodeAffectedRows)} is not applicable.");
}
