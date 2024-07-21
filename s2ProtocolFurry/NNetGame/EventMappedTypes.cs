namespace s2ProtocolFurry.NNetGame
{
    public static class EventMappedTypes
    {
        public static readonly Dictionary<GameEventId, (int TypeId, string Name)> GameEventMappedTypes = new Dictionary<GameEventId, (int TypeId, string Name)>
        {
            { GameEventId.SUserFinishedLoadingSyncEvent, (82, "NNet.Game.SUserFinishedLoadingSyncEvent") },
            { GameEventId.SUserOptionsEvent, (81, "NNet.Game.SUserOptionsEvent") },
            { GameEventId.SBankFileEvent, (74, "NNet.Game.SBankFileEvent") },
            { GameEventId.SBankSectionEvent, (76, "NNet.Game.SBankSectionEvent") },
            { GameEventId.SBankKeyEvent, (77, "NNet.Game.SBankKeyEvent") },
            { GameEventId.SBankValueEvent, (78, "NNet.Game.SBankValueEvent") },
            { GameEventId.SBankSignatureEvent, (80, "NNet.Game.SBankSignatureEvent") },
            { GameEventId.SCameraSaveEvent, (85, "NNet.Game.SCameraSaveEvent") },
            { GameEventId.SSaveGameEvent, (86, "NNet.Game.SSaveGameEvent") },
            { GameEventId.SSaveGameDoneEvent, (82, "NNet.Game.SSaveGameDoneEvent") },
            { GameEventId.SLoadGameDoneEvent, (82, "NNet.Game.SLoadGameDoneEvent") },
            { GameEventId.SCommandManagerResetEvent, (87, "NNet.Game.SCommandManagerResetEvent") },
            { GameEventId.SGameCheatEvent, (90, "NNet.Game.SGameCheatEvent") },
            { GameEventId.SCmdEvent, (100, "NNet.Game.SCmdEvent") },
            { GameEventId.SSelectionDeltaEvent, (109, "NNet.Game.SSelectionDeltaEvent") },
            { GameEventId.SControlGroupUpdateEvent, (110, "NNet.Game.SControlGroupUpdateEvent") },
            { GameEventId.SSelectionSyncCheckEvent, (112, "NNet.Game.SSelectionSyncCheckEvent") },
            { GameEventId.SResourceTradeEvent, (114, "NNet.Game.SResourceTradeEvent") },
            { GameEventId.STriggerChatMessageEvent, (115, "NNet.Game.STriggerChatMessageEvent") },
            { GameEventId.SAICommunicateEvent, (118, "NNet.Game.SAICommunicateEvent") },
            { GameEventId.SSetAbsoluteGameSpeedEvent, (119, "NNet.Game.SSetAbsoluteGameSpeedEvent") },
            { GameEventId.SAddAbsoluteGameSpeedEvent, (120, "NNet.Game.SAddAbsoluteGameSpeedEvent") },
            { GameEventId.STriggerPingEvent, (121, "NNet.Game.STriggerPingEvent") },
            { GameEventId.SBroadcastCheatEvent, (122, "NNet.Game.SBroadcastCheatEvent") },
            { GameEventId.SAllianceEvent, (123, "NNet.Game.SAllianceEvent") },
            { GameEventId.SUnitClickEvent, (124, "NNet.Game.SUnitClickEvent") },
            { GameEventId.SUnitHighlightEvent, (125, "NNet.Game.SUnitHighlightEvent") },
            { GameEventId.STriggerReplySelectedEvent, (126, "NNet.Game.STriggerReplySelectedEvent") },
            { GameEventId.SHijackReplayGameEvent, (131, "NNet.Game.SHijackReplayGameEvent") },
            { GameEventId.STriggerSkippedEvent, (82, "NNet.Game.STriggerSkippedEvent") },
            { GameEventId.STriggerSoundLengthQueryEvent, (136, "NNet.Game.STriggerSoundLengthQueryEvent") },
            { GameEventId.STriggerSoundOffsetEvent, (143, "NNet.Game.STriggerSoundOffsetEvent") },
            { GameEventId.STriggerTransmissionOffsetEvent, (144, "NNet.Game.STriggerTransmissionOffsetEvent") },
            { GameEventId.STriggerTransmissionCompleteEvent, (145, "NNet.Game.STriggerTransmissionCompleteEvent") },
            { GameEventId.SCameraUpdateEvent, (149, "NNet.Game.SCameraUpdateEvent") },
            { GameEventId.STriggerAbortMissionEvent, (82, "NNet.Game.STriggerAbortMissionEvent") },
            { GameEventId.STriggerPurchaseMadeEvent, (132, "NNet.Game.STriggerPurchaseMadeEvent") },
            { GameEventId.STriggerPurchaseExitEvent, (82, "NNet.Game.STriggerPurchaseExitEvent") },
            { GameEventId.STriggerPlanetMissionLaunchedEvent, (133, "NNet.Game.STriggerPlanetMissionLaunchedEvent") },
            { GameEventId.STriggerPlanetPanelCanceledEvent, (82, "NNet.Game.STriggerPlanetPanelCanceledEvent") },
            { GameEventId.STriggerDialogControlEvent, (135, "NNet.Game.STriggerDialogControlEvent") },
            { GameEventId.STriggerSoundLengthSyncEvent, (139, "NNet.Game.STriggerSoundLengthSyncEvent") },
            { GameEventId.STriggerConversationSkippedEvent, (150, "NNet.Game.STriggerConversationSkippedEvent") },
            { GameEventId.STriggerMouseClickedEvent, (153, "NNet.Game.STriggerMouseClickedEvent") },
            { GameEventId.STriggerMouseMovedEvent, (154, "NNet.Game.STriggerMouseMovedEvent") },
            { GameEventId.SAchievementAwardedEvent, (155, "NNet.Game.SAchievementAwardedEvent") },
            { GameEventId.STriggerHotkeyPressedEvent, (156, "NNet.Game.STriggerHotkeyPressedEvent") },
            { GameEventId.STriggerTargetModeUpdateEvent, (157, "NNet.Game.STriggerTargetModeUpdateEvent") },
            { GameEventId.STriggerPlanetPanelReplayEvent, (82, "NNet.Game.STriggerPlanetPanelReplayEvent") },
            { GameEventId.STriggerSoundtrackDoneEvent, (158, "NNet.Game.STriggerSoundtrackDoneEvent") },
            { GameEventId.STriggerPlanetMissionSelectedEvent, (159, "NNet.Game.STriggerPlanetMissionSelectedEvent") },
            { GameEventId.STriggerKeyPressedEvent, (160, "NNet.Game.STriggerKeyPressedEvent") },
            { GameEventId.STriggerMovieFunctionEvent, (171, "NNet.Game.STriggerMovieFunctionEvent") },
            { GameEventId.STriggerPlanetPanelBirthCompleteEvent, (82, "NNet.Game.STriggerPlanetPanelBirthCompleteEvent") },
            { GameEventId.STriggerPlanetPanelDeathCompleteEvent, (82, "NNet.Game.STriggerPlanetPanelDeathCompleteEvent") },
            { GameEventId.SResourceRequestEvent, (161, "NNet.Game.SResourceRequestEvent") },
            { GameEventId.SResourceRequestFulfillEvent, (162, "NNet.Game.SResourceRequestFulfillEvent") },
            { GameEventId.SResourceRequestCancelEvent, (163, "NNet.Game.SResourceRequestCancelEvent") },
            { GameEventId.STriggerResearchPanelExitEvent, (82, "NNet.Game.STriggerResearchPanelExitEvent") },
            { GameEventId.STriggerResearchPanelPurchaseEvent, (82, "NNet.Game.STriggerResearchPanelPurchaseEvent") },
            { GameEventId.STriggerResearchPanelSelectionChangedEvent, (165, "NNet.Game.STriggerResearchPanelSelectionChangedEvent") },
            { GameEventId.STriggerCommandErrorEvent, (164, "NNet.Game.STriggerCommandErrorEvent") },
            { GameEventId.STriggerMercenaryPanelExitEvent, (82, "NNet.Game.STriggerMercenaryPanelExitEvent") },
            { GameEventId.STriggerMercenaryPanelPurchaseEvent, (82, "NNet.Game.STriggerMercenaryPanelPurchaseEvent") },
            { GameEventId.STriggerMercenaryPanelSelectionChangedEvent, (166, "NNet.Game.STriggerMercenaryPanelSelectionChangedEvent") },
            { GameEventId.STriggerVictoryPanelExitEvent, (82, "NNet.Game.STriggerVictoryPanelExitEvent") },
            { GameEventId.STriggerBattleReportPanelExitEvent, (82, "NNet.Game.STriggerBattleReportPanelExitEvent") },
            { GameEventId.STriggerBattleReportPanelPlayMissionEvent, (167, "NNet.Game.STriggerBattleReportPanelPlayMissionEvent") },
            { GameEventId.STriggerBattleReportPanelPlaySceneEvent, (168, "NNet.Game.STriggerBattleReportPanelPlaySceneEvent") },
            { GameEventId.STriggerBattleReportPanelSelectionChangedEvent, (168, "NNet.Game.STriggerBattleReportPanelSelectionChangedEvent") },
            { GameEventId.STriggerVictoryPanelPlayMissionAgainEvent, (133, "NNet.Game.STriggerVictoryPanelPlayMissionAgainEvent") },
            { GameEventId.STriggerMovieStartedEvent, (82, "NNet.Game.STriggerMovieStartedEvent") },
            { GameEventId.STriggerMovieFinishedEvent, (82, "NNet.Game.STriggerMovieFinishedEvent") },
            { GameEventId.SDecrementGameTimeRemainingEvent, (169, "NNet.Game.SDecrementGameTimeRemainingEvent") },
            { GameEventId.STriggerPortraitLoadedEvent, (170, "NNet.Game.STriggerPortraitLoadedEvent") },
            { GameEventId.STriggerCustomDialogDismissedEvent, (172, "NNet.Game.STriggerCustomDialogDismissedEvent") },
            { GameEventId.STriggerGameMenuItemSelectedEvent, (173, "NNet.Game.STriggerGameMenuItemSelectedEvent") },
            { GameEventId.STriggerMouseWheelEvent, (175, "NNet.Game.STriggerMouseWheelEvent") },
            { GameEventId.STriggerPurchasePanelSelectedPurchaseItemChangedEvent, (132, "NNet.Game.STriggerPurchasePanelSelectedPurchaseItemChangedEvent") },
            { GameEventId.STriggerPurchasePanelSelectedPurchaseCategoryChangedEvent, (176, "NNet.Game.STriggerPurchasePanelSelectedPurchaseCategoryChangedEvent") },
            { GameEventId.STriggerButtonPressedEvent, (177, "NNet.Game.STriggerButtonPressedEvent") },
            { GameEventId.STriggerGameCreditsFinishedEvent, (82, "NNet.Game.STriggerGameCreditsFinishedEvent") },
            { GameEventId.STriggerCutsceneBookmarkFiredEvent, (178, "NNet.Game.STriggerCutsceneBookmarkFiredEvent") },
            { GameEventId.STriggerCutsceneEndSceneFiredEvent, (179, "NNet.Game.STriggerCutsceneEndSceneFiredEvent") },
            { GameEventId.STriggerCutsceneConversationLineEvent, (180, "NNet.Game.STriggerCutsceneConversationLineEvent") },
            { GameEventId.STriggerCutsceneConversationLineMissingEvent, (181, "NNet.Game.STriggerCutsceneConversationLineMissingEvent") },
            { GameEventId.SGameUserLeaveEvent, (182, "NNet.Game.SGameUserLeaveEvent") },
            { GameEventId.SGameUserJoinEvent, (183, "NNet.Game.SGameUserJoinEvent") },
            { GameEventId.SCommandManagerStateEvent, (185, "NNet.Game.SCommandManagerStateEvent") },
            { GameEventId.SCmdUpdateTargetPointEvent, (186, "NNet.Game.SCmdUpdateTargetPointEvent") },
            { GameEventId.SCmdUpdateTargetUnitEvent, (187, "NNet.Game.SCmdUpdateTargetUnitEvent") },
            { GameEventId.STriggerAnimLengthQueryByNameEvent, (140, "NNet.Game.STriggerAnimLengthQueryByNameEvent") },
            { GameEventId.STriggerAnimLengthQueryByPropsEvent, (141, "NNet.Game.STriggerAnimLengthQueryByPropsEvent") },
            { GameEventId.STriggerAnimOffsetEvent, (142, "NNet.Game.STriggerAnimOffsetEvent") },
            { GameEventId.SCatalogModifyEvent, (188, "NNet.Game.SCatalogModifyEvent") },
            { GameEventId.SHeroTalentTreeSelectedEvent, (189, "NNet.Game.SHeroTalentTreeSelectedEvent") },
            { GameEventId.STriggerProfilerLoggingFinishedEvent, (82, "NNet.Game.STriggerProfilerLoggingFinishedEvent") },
            { GameEventId.SHeroTalentTreeSelectionPanelToggledEvent, (190, "NNet.Game.SHeroTalentTreeSelectionPanelToggledEvent") },
            { GameEventId.SSetSyncLoadingTimeEvent, (191, "NNet.Game.SSetSyncLoadingTimeEvent") },
            { GameEventId.SSetSyncPlayingTimeEvent, (191, "NNet.Game.SSetSyncPlayingTimeEvent") },
            { GameEventId.SPeerSetSyncLoadingTimeEvent, (191, "NNet.Game.SPeerSetSyncLoadingTimeEvent") },
            { GameEventId.SPeerSetSyncPlayingTimeEvent, (191, "NNet.Game.SPeerSetSyncPlayingTimeEvent") }
        };

        public static readonly Dictionary<TrackerEventId, (int typeid, string name)> TrackedEventMappedTypes = new Dictionary<TrackerEventId, (int typeid, string name)>
        {
            { TrackerEventId.SPlayerStatsEvent, (197, "NNet.Replay.Tracker.SPlayerStatsEvent") },
            { TrackerEventId.SUnitBornEvent, (199, "NNet.Replay.Tracker.SUnitBornEvent") },
            { TrackerEventId.SUnitDiedEvent, (200, "NNet.Replay.Tracker.SUnitDiedEvent") },
            { TrackerEventId.SUnitOwnerChangeEvent, (201, "NNet.Replay.Tracker.SUnitOwnerChangeEvent") },
            { TrackerEventId.SUnitTypeChangeEvent, (202, "NNet.Replay.Tracker.SUnitTypeChangeEvent") },
            { TrackerEventId.SUpgradeEvent, (203, "NNet.Replay.Tracker.SUpgradeEvent") },
            { TrackerEventId.SUnitInitEvent, (204, "NNet.Replay.Tracker.SUnitInitEvent") },
            { TrackerEventId.SUnitDoneEvent, (205, "NNet.Replay.Tracker.SUnitDoneEvent") },
            { TrackerEventId.SUnitPositionsEvent, (207, "NNet.Replay.Tracker.SUnitPositionsEvent") },
            { TrackerEventId.SPlayerSetupEvent, (208, "NNet.Replay.Tracker.SPlayerSetupEvent") }
        };

        public static readonly Dictionary<MessageEventId, (int typeid, string name)> MessageEventTypes = new Dictionary<MessageEventId, (int typeid, string name)>
        {
            { MessageEventId.SChatMessage, (192, "NNet.Game.SChatMessage") },
            { MessageEventId.SPingMessage, (193, "NNet.Game.SPingMessage") },
            { MessageEventId.SLoadingProgressMessage, (194, "NNet.Game.SLoadingProgressMessage") },
            { MessageEventId.SServerPingMessage, (82, "NNet.Game.SServerPingMessage") },
            { MessageEventId.SReconnectNotifyMessage, (195, "NNet.Game.SReconnectNotifyMessage") }
        };

        public const int GameEventIdTypeId = 0;
        public const int MessageEventIdTypeId = 1;
        public const int TrackerEventIdTypeId = 2;

        // The typeid of NNet.SVarUint32 (the type used to encode gameloop deltas).
        public const int SVarUint32TypeId = 7;

        // The typeid of NNet.Replay.SGameUserId (the type used to encode player ids).
        public const int ReplayUserIdTypeId = 8;

        // The typeid of NNet.Replay.SHeader (the type used to store replay game version and length).
        public const int ReplayHeaderTypeId = 18;

        // The typeid of NNet.Game.SDetails (the type used to store overall replay details).
        public const int GameDetailsTypeId = 40;

        // The typeid of NNet.Replay.SInitData (the type used to store the initial lobby).
        public const int ReplayInitDataTypeId = 73;
    }
}
