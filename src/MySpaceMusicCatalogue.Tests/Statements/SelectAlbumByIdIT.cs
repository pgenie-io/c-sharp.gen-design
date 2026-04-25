using Pgenie.Artifacts.Myspace.MusicCatalogue.Statements;

namespace Pgenie.Artifacts.Myspace.MusicCatalogue.Tests.Statements;

public class SelectAlbumByIdIT : AbstractDatabaseIT
{
    [Fact]
    public void ExecutesWithDefaultValues()
    {
        var result = Execute(new SelectAlbumById(null));
        Assert.Null(result);
    }
}
