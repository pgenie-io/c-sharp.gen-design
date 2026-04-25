using Pgenie.Artifacts.Myspace.MusicCatalogue.Statements;
using Pgenie.Artifacts.Myspace.MusicCatalogue.Types;

namespace Pgenie.Artifacts.Myspace.MusicCatalogue.Tests.Statements;

public class InsertAlbumIT : AbstractDatabaseIT
{
    [Fact]
    public void ExecutesWithDefaultValues()
    {
        var result = Execute(new InsertAlbum("", new DateOnly(2000, 1, 1), AlbumFormat.Vinyl, new RecordingInfo(null, null, null, null)));
        Assert.NotNull(result);
    }
}
