using Pgenie.Artifacts.Myspace.MusicCatalogue.Statements;

namespace Pgenie.Artifacts.Myspace.MusicCatalogue.Tests.Statements;

public class UpdateAlbumReleasedIT : AbstractDatabaseIT
{
    [Fact]
    public void ExecutesWithDefaultValues()
    {
        var result = Execute(new UpdateAlbumReleased(null, 0L));
        Assert.True(result >= 0L);
    }
}
