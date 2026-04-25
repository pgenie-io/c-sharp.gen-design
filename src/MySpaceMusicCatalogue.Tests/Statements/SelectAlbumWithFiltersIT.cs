using Pgenie.Artifacts.Myspace.MusicCatalogue.Statements;

namespace Pgenie.Artifacts.Myspace.MusicCatalogue.Tests.Statements;

public class SelectAlbumWithFiltersIT : AbstractDatabaseIT
{
    [Fact]
    public void ExecutesWithDefaultValues()
    {
        var result = Execute(new SelectAlbumWithFilters(null, null, null, null, null, false, false));
        Assert.NotNull(result);
    }
}
