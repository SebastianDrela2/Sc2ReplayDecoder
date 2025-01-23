using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Immutable;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using s2ProtocolFurry.Decoder;

namespace s2ProtocolFurry.Events.TrackerEvents;

public class TrackerEvents(EventDecoderBuffer storage)
{
    public EventDecoderBuffer Storage = storage;

    public RefList<SPlayerSetupEvent> PlayerSetup => Storage.PlayerSetup.Data;
    public RefList<SPlayerStatsEvent> PlayerStats => Storage.PlayerStats.Data;
    public RefList<SUnitBornEvent> UnitBorn => Storage.UnitBorn.Data;
    public RefList<SUnitDiedEvent> UnitDied => Storage.UnitDied.Data;
    public RefList<SUnitOwnerChangeEvent> UnitOwnerChange => Storage.UnitOwnerChange.Data;
    public RefList<SUnitPositionsEvent> UnitPositions => Storage.UnitPositions.Data;
    public RefList<SUnitTypeChangeEvent> UnitTypeChange => Storage.UnitTypeChange.Data;
    public RefList<SUpgradeEvent> Upgrade => Storage.Upgrade.Data;
    public RefList<SUnitInitEvent> UnitInit => Storage.UnitInit.Data;
    public RefList<SUnitDoneEvent> UnitDone => Storage.UnitDone.Data;
}
