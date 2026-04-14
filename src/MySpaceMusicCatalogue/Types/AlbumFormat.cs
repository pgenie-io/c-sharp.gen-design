using NpgsqlTypes;

namespace Pgenie.Artifacts.Myspace.MusicCatalogue.Types;

/// <summary>
/// Representation of the <c>album_format</c> user-declared PostgreSQL
/// enumeration type.
/// </summary>
/// <remarks>
/// Generated from SQL queries using <a href="https://pgenie.io">pGenie</a>.
/// <para>
/// Register this type with Npgsql via
/// <see cref="TypeMapper.Register(Npgsql.NpgsqlDataSourceBuilder)"/> before
/// opening connections.
/// </para>
/// </remarks>
public enum AlbumFormat
{
    /// <summary>Corresponds to the PostgreSQL enum variant <c>Vinyl</c>.</summary>
    [PgName("Vinyl")] Vinyl,

    /// <summary>Corresponds to the PostgreSQL enum variant <c>CD</c>.</summary>
    [PgName("CD")] Cd,

    /// <summary>Corresponds to the PostgreSQL enum variant <c>Cassette</c>.</summary>
    [PgName("Cassette")] Cassette,

    /// <summary>Corresponds to the PostgreSQL enum variant <c>Digital</c>.</summary>
    [PgName("Digital")] Digital,

    /// <summary>Corresponds to the PostgreSQL enum variant <c>DVD-Audio</c>.</summary>
    [PgName("DVD-Audio")] DvdAudio,

    /// <summary>Corresponds to the PostgreSQL enum variant <c>SACD</c>.</summary>
    [PgName("SACD")] Sacd,
}
