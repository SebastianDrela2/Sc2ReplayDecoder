using s2ProtocolFurry.Decoder;
using System.Diagnostics;

namespace Sc2ReplayTests;

[TestFixture]
public class Tests
{        
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
                 
    }

    public static IEnumerable<string> AllFiles => GetAllFiles();

    [TestCaseSource(nameof(AllFiles))]
    public void TestEverything(string path)
    {
        var protocols = @"C:\Users\Seba\source\repos\ParasiteReplayAnalyzer\s2protocol.NET\libs2\s2protocol\versions";

        var decoder = new Sc2ReplayDecoder(protocols);

        var watch = new Stopwatch();

        watch.Start();
        var replay = decoder.DecodeSc2Replay(path);
        watch.Stop();

        Assert.That(replay, Is.Not.Null);
    }

    [Test]
    public void tete()
    {

    }

    private static string[] GetAllFiles()
    {
        var path = Path.GetFullPath(@"../../../Replays");
        var files = Directory.GetFiles(path);

        return files;
    }
}