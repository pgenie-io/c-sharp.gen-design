using Pgenie.Artifacts.Myspace.MusicCatalogue.Statements;

namespace Pgenie.Artifacts.Myspace.MusicCatalogue.Tests.Statements;

public class SelectAlbumFieldsIT : AbstractDatabaseIT
{
    [Fact]
    public void ExecutesWithDefaultValues()
    {
        var result = Execute(new SelectAlbumFields(false, false, false, false, false, false, 0L));
        Assert.NotNull(result);
    }
}
