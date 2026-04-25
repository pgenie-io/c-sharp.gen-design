using Pgenie.Artifacts.Myspace.MusicCatalogue.Statements;
using Pgenie.Artifacts.Myspace.MusicCatalogue.Types;

namespace Pgenie.Artifacts.Myspace.MusicCatalogue.Tests.Statements;

public class UpdateAlbumRecordingReturningIT : AbstractDatabaseIT
{
    [Fact]
    public void ExecutesWithDefaultValues()
    {
        var result = Execute(new UpdateAlbumRecordingReturning(null, 0L));
        Assert.NotNull(result);
    }
}
