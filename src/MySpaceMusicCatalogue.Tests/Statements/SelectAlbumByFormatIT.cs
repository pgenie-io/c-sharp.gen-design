using Pgenie.Artifacts.Myspace.MusicCatalogue.Statements;
using Pgenie.Artifacts.Myspace.MusicCatalogue.Types;

namespace Pgenie.Artifacts.Myspace.MusicCatalogue.Tests.Statements;

public class SelectAlbumByFormatIT : AbstractDatabaseIT
{
    [Fact]
    public void ExecutesWithDefaultValues()
    {
        var result = Execute(new SelectAlbumByFormat(AlbumFormat.Vinyl));
        Assert.NotNull(result);
    }
}
