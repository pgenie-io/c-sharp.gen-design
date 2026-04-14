using Npgsql;
using Pgenie.Artifacts.Myspace.MusicCatalogue.Types;

namespace Pgenie.Artifacts.Myspace.MusicCatalogue;

/// <summary>
/// Registers all PostgreSQL custom types (enumerations and composites) used in
/// this library with an <see cref="NpgsqlDataSourceBuilder"/>.
/// </summary>
/// <remarks>
/// Call <see cref="Register"/> once when configuring your
/// <see cref="NpgsqlDataSourceBuilder"/> before building the data source:
/// <code>
/// var builder = new NpgsqlDataSourceBuilder(connectionString);
/// builder.Register();
/// await using var dataSource = builder.Build();
/// </code>
/// </remarks>
public static class TypeMapper
{
    /// <summary>
    /// Maps all PostgreSQL custom enum and composite types to their C#
    /// counterparts on the given <paramref name="builder"/>.
    /// </summary>
    /// <param name="builder">The data source builder to configure.</param>
    /// <returns>The same builder, for chaining.</returns>
    public static NpgsqlDataSourceBuilder Register(
        this NpgsqlDataSourceBuilder builder)
    {
        builder.MapEnum<AlbumFormat>("album_format");
        builder.MapComposite<RecordingInfo>("recording_info");
        builder.MapComposite<TrackInfo>("track_info");
        builder.MapComposite<DiscInfo>("disc_info");
        return builder;
    }
}
