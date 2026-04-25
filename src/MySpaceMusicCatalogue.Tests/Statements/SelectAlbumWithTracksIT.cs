using Pgenie.Artifacts.Myspace.MusicCatalogue.Statements;

namespace Pgenie.Artifacts.Myspace.MusicCatalogue.Tests.Statements;

public class SelectAlbumWithTracksIT : AbstractDatabaseIT
{
    [Fact]
    public void ExecutesWithDefaultValues()
    {
        var result = Execute(new SelectAlbumWithTracks(0L));
        Assert.NotNull(result);
    }
}
