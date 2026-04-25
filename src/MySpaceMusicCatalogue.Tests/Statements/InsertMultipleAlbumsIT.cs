using Pgenie.Artifacts.Myspace.MusicCatalogue.Statements;

namespace Pgenie.Artifacts.Myspace.MusicCatalogue.Tests.Statements;

public class InsertMultipleAlbumsIT : AbstractDatabaseIT
{
    [Fact]
    public void ExecutesWithDefaultValues()
    {
        var result = Execute(new InsertMultipleAlbums([], [], []));
        Assert.NotNull(result);
    }
}
