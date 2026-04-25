using DotNet.Testcontainers.Builders;
using Npgsql;
using Pgenie.Artifacts.Myspace.MusicCatalogue;
using Testcontainers.PostgreSql;

namespace Pgenie.Artifacts.Myspace.MusicCatalogue.Tests;

/// <summary>
/// Shared base for all statement integration tests.
///
/// <para>
/// The PostgreSQL container is started once for the entire process via a static
/// initialiser (singleton container pattern). Schema migrations are applied at
/// that same point. Testcontainers' Ryuk reaper container handles cleanup when
/// the process exits, so no explicit stop call is needed.
/// </para>
///
/// <para>
/// Each test class receives a fresh <see cref="NpgsqlDataSource"/> (created in
/// the constructor and disposed via <see cref="IDisposable"/>) so that
/// connection state does not bleed between tests.
/// </para>
/// </summary>
public abstract class AbstractDatabaseIT : IDisposable
{
    private static readonly string[] Migrations =
    [
        """
        create table "genre" (
          "id" int4 not null generated always as identity primary key,
          "name" text not null unique
        );

        create table "artist" (
          "id" int4 not null generated always as identity primary key,
          "name" text not null
        );

        create table "album" (
          "id" int4 not null generated always as identity primary key,
          "name" text not null,
          "released" date null
        );

        create table "album_genre" (
          "album" int4 not null references "album",
          "genre" int4 not null references "genre"
        );

        create table "album_artist" (
          "album" int4 not null references "album",
          "artist" int4 not null references "artist",
          "primary" bool not null,
          primary key ("album", "artist")
        );
        """,
        """
        alter table album
        alter column id type int8;

        alter table album_genre
        alter column album type int8;

        alter table album_artist
        alter column album type int8;
        """,
        """
        create type album_format as enum (
          'Vinyl',
          'CD',
          'Cassette',
          'Digital',
          'DVD-Audio',
          'SACD'
        );

        create type recording_info as (
          studio_name text,
          city text,
          country text,
          recorded_date date
        );

        alter table album
        add column format album_format null;

        alter table album
        add column recording recording_info null;
        """,
        """
        create type track_info as (
          title text,
          duration_seconds int4,
          tags text[]
        );

        create type disc_info as (
          name text,
          recording recording_info
        );

        alter table album
        add column tracks track_info[] null;

        alter table album
        add column disc disc_info null;
        """,
        """
        CREATE INDEX ON album (recording);
        """,
        """
        create extension if not exists ltree;

        alter table genre
        add column path ltree not null;

        create index on genre using gist (path);
        """
    ];

    /// <summary>Single container shared across all test classes in the suite.</summary>
    protected static readonly PostgreSqlContainer Pg = new PostgreSqlBuilder()
        .WithImage("postgres:18")
        .Build();

    static AbstractDatabaseIT()
    {
        Pg.StartAsync().GetAwaiter().GetResult();
        ApplyMigrations();
    }

    private static void ApplyMigrations()
    {
        using var conn = new NpgsqlConnection(Pg.GetConnectionString());
        conn.Open();
        foreach (var migration in Migrations)
        {
            using var cmd = new NpgsqlCommand(migration, conn);
            cmd.ExecuteNonQuery();
        }
    }

    protected NpgsqlDataSource DataSource { get; }

    protected AbstractDatabaseIT()
    {
        DataSource = new NpgsqlDataSourceBuilder(Pg.GetConnectionString())
            .Register()
            .Build();
    }

    public void Dispose() => DataSource.Dispose();

    protected TResult Execute<TResult>(IStatement<TResult> stmt)
    {
        using var conn = DataSource.OpenConnection();
        return stmt.Execute(conn);
    }
}
