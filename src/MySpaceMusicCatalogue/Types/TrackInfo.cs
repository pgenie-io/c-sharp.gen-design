namespace Pgenie.Artifacts.Myspace.MusicCatalogue.Types;

/// <summary>
/// Representation of the <c>track_info</c> user-declared PostgreSQL
/// composite (record) type.
/// </summary>
/// <param name="Title">Maps to <c>title</c>. Nullable.</param>
/// <param name="DurationSeconds">Maps to <c>duration_seconds</c>. Nullable.</param>
/// <param name="Tags">
/// Maps to <c>tags</c>. Nullable array of nullable text values.
/// </param>
/// <remarks>
/// Generated from SQL queries using <a href="https://pgenie.io">pGenie</a>.
/// <para>
/// All fields are nullable, matching the PostgreSQL column definitions.
/// Register this type with Npgsql via
/// <see cref="TypeMapper.Register(Npgsql.NpgsqlDataSourceBuilder)"/> before
/// opening connections.
/// </para>
/// </remarks>
public record TrackInfo(
    string? Title,
    int? DurationSeconds,
    string?[]? Tags);
