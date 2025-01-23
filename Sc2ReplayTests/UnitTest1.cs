using s2ProtocolFurry.Decoder;
using System.Diagnostics;

namespace Sc2ReplayTests;

public class TestExpectedData
{

}

[TestFixture]
public class Tests
{        
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
                 
    }

    public static IEnumerable<string> AllFiles => GetAllFiles();

    public const string ProtocolVersionsDirectory = @"E:\repo\SebastianDrela2\s2protocol\s2protocol\versions";

    [TestCaseSource(nameof(AllFiles))]
    public void TestEverything(string path)
    {
        var sw = Stopwatch.StartNew();
        var decoder = new Sc2ReplayDecoder(ProtocolVersionsDirectory);
        var initTime = sw.Elapsed;
        
        sw.Restart();
        var bytes = File.ReadAllBytes(path);
        using var stream = new MemoryStream(bytes);
        var ioTime = sw.Elapsed;

        sw.Restart();
        var replay = decoder.DecodeSc2Replay(stream);
        var decodeTime = sw.Elapsed;

        var totalTime = initTime + ioTime + decodeTime;

        Console.WriteLine($"Time:");
        Console.WriteLine($"  Init   : {initTime.TotalMilliseconds} ms ({(initTime / totalTime)} of total)");
        Console.WriteLine($"  IO     : {ioTime.TotalMilliseconds} ms ({(ioTime / totalTime)} of total)");
        Console.WriteLine($"  Decode : {decodeTime.TotalMilliseconds} ms ({(decodeTime / totalTime)} of total)");
        Console.WriteLine($"  Total  : {totalTime.TotalMilliseconds} ms");

        var data = replay.TrackerEvents;
        
        //
        Assert.That(replay.GameEvents.Gameevents.Count == 113914);

        Assert.That(replay.ChatMessages.Count is 880);

        Assert.That(data.PlayerSetup.Length is 14 && data.PlayerSetup.Data.All(x => x is not null));
        Assert.That(data.PlayerStats.Length is 4401 && data.PlayerStats.Data.All(x => x is not null));
        Assert.That(data.UnitBorn.Length is 16245 && data.UnitBorn.Data.All(x => x is not null));
        Assert.That(data.UnitDied.Length is 14528 && data.UnitDied.Data.All(x => x is not null));
        Assert.That(data.UnitDone.Length is 7 && data.UnitDone.Data.All(x => x is not null));
        Assert.That(data.UnitInit.Length is 7 && data.UnitInit.Data.All(x => x is not null));
        Assert.That(data.UnitOwnerChange.Length is 585 && data.UnitOwnerChange.Data.All(x => x is not null));
        Assert.That(data.UnitPositions.Length is 165 && data.UnitPositions.Data.All(x => x is not null));
        Assert.That(data.UnitTypeChange.Length is 6801 && data.UnitTypeChange.Data.All(x => x is not null));
        Assert.That(data.Upgrade.Length is 10728 && data.Upgrade.Data.All(x => x is not null));
        //
        
        //replay.TrackerEvents.Upgrade
        // var actual = (
        //     PlayerSetup: data.PlayerSetup.Length,
        //     PlayerStats: data.PlayerStats.Length,
        //     UnitBorn: data.UnitBorn.Length,
        //     UnitDied: data.UnitDied.Length,
        //     UnitOwnerChange: data.UnitOwnerChange.Length,
        //     UnitPositions: data.UnitPositions.Length,
        //     UnitTypeChange: data.UnitTypeChange.Length,
        //     Upgrade: data.Upgrade.Length,
        //     UnitInit: data.UnitInit.Length,
        //     UnitDone: data.UnitDone.Length
        // );
        //var expected = actual; // xD
        //Assert.That(actual, Is.Not.EqualTo(expected));
    }

    private static string[] GetAllFiles()
    {
        var path = Path.GetFullPath(@"../../../Replays");
        var files = Directory.GetFiles(path).Where(x => x.Contains("120"));

        return files.Take(1).ToArray();
    }
}