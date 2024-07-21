namespace s2ProtocolFurry.Events.GameEvents
{
    internal class SUserOptionsEvent
    {
        public SUserOptionsEvent(GameEvent gameEvent, bool testCheatsEnabled, bool multiplayerCheatsEnabled, bool gameFullyDownloaded, string hotkeyProfile, bool useGalaxyAsserts, bool debugPauseEnabled, bool cameraFollow, bool isMapToMapTransition, int buildNum, int versionFlags, bool developmentCheatsEnabled, bool platformMac, int baseBuildNum, bool syncChecksummingEnabled)
        {
            GameEvent = gameEvent;
            TestCheatsEnabled = testCheatsEnabled;
            MultiplayerCheatsEnabled = multiplayerCheatsEnabled;
            GameFullyDownloaded = gameFullyDownloaded;
            HotkeyProfile = hotkeyProfile;
            UseGalaxyAsserts = useGalaxyAsserts;
            DebugPauseEnabled = debugPauseEnabled;
            CameraFollow = cameraFollow;
            IsMapToMapTransition = isMapToMapTransition;
            BuildNum = buildNum;
            VersionFlags = versionFlags;
            DevelopmentCheatsEnabled = developmentCheatsEnabled;
            PlatformMac = platformMac;
            BaseBuildNum = baseBuildNum;
            SyncChecksummingEnabled = syncChecksummingEnabled;
        }

        public GameEvent GameEvent { get; }
        public bool TestCheatsEnabled { get; }
        public bool MultiplayerCheatsEnabled { get; }
        public bool GameFullyDownloaded { get; }
        public string HotkeyProfile { get; }
        public bool UseGalaxyAsserts { get; }
        public bool DebugPauseEnabled { get; }
        public bool CameraFollow { get; }
        public bool IsMapToMapTransition { get; }
        public int BuildNum { get; }
        public int VersionFlags { get; }
        public bool DevelopmentCheatsEnabled { get; }
        public bool PlatformMac { get; }
        public int BaseBuildNum { get; }
        public bool SyncChecksummingEnabled { get; }
    }
}