namespace Pgenie.Artifacts.Myspace.MusicCatalogue.Types;

/// <summary>
/// Representation of the <c>disc_info</c> user-declared PostgreSQL
/// composite (record) type.
/// </summary>
/// <param name="Name">Maps to <c>name</c>. Nullable.</param>
/// <param name="Recording">Maps to <c>recording</c>. Nullable.</param>
/// <remarks>
/// Generated from SQL queries using <a href="https://pgenie.io">pGenie</a>.
/// <para>
/// All fields are nullable, matching the PostgreSQL column definitions.
/// Register this type with Npgsql via
/// <see cref="TypeMapper.Register(Npgsql.NpgsqlDataSourceBuilder)"/> before
/// opening connections.
/// </para>
/// </remarks>
public record DiscInfo(
    string? Name,
    RecordingInfo? Recording);
