namespace Pgenie.Artifacts.Myspace.MusicCatalogue.Types;

/// <summary>
/// Representation of the <c>recording_info</c> user-declared PostgreSQL
/// composite (record) type.
/// </summary>
/// <param name="StudioName">Maps to <c>studio_name</c>. Nullable.</param>
/// <param name="City">Maps to <c>city</c>. Nullable.</param>
/// <param name="Country">Maps to <c>country</c>. Nullable.</param>
/// <param name="RecordedDate">Maps to <c>recorded_date</c>. Nullable.</param>
/// <remarks>
/// Generated from SQL queries using <a href="https://pgenie.io">pGenie</a>.
/// <para>
/// All fields are nullable, matching the PostgreSQL column definitions.
/// Register this type with Npgsql via
/// <see cref="TypeMapper.Register(Npgsql.NpgsqlDataSourceBuilder)"/> before
/// opening connections.
/// </para>
/// </remarks>
public record RecordingInfo(
    string? StudioName,
    string? City,
    string? Country,
    DateOnly? RecordedDate);
