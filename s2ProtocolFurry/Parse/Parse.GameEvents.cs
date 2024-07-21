using s2ProtocolFurry.Events.GameEvents;
using s2ProtocolFurry.NNetGame;
using System.Collections.Generic;

namespace s2ProtocolFurry.Parse
{
    public static partial class Parse
    {
        public static GameEvents GameEvents(Dictionary<string, object> dic)
        {
            List<GameEvent> gameevents = new();

            foreach (var entry in dic)
            {
                if (entry.Value is Dictionary<string, object> gameDic)
                {
                    GameEvent gameEvent = GetGameEvent(gameDic);

                    GameEvent detailEvent = gameEvent.EventType switch
                    {
                        GameEventType.SBankFileEvent => GetSBankFileEvent(gameDic, gameEvent),
                        GameEventType.SBankKeyEvent => GetSBankKeyEvent(gameDic, gameEvent),
                        GameEventType.SBankSectionEvent => GetSBankSectionEvent(gameDic, gameEvent),
                        GameEventType.SBankSignatureEvent => GetSBankSignatureEvent(gameDic, gameEvent),
                        GameEventType.SBankValueEvent => GetSBankValueEvent(gameDic, gameEvent),
                        GameEventType.SCameraUpdateEvent => GetSCameraUpdateEvent(gameDic, gameEvent),
                        GameEventType.SCmdEvent => GetSCmdEvent(gameDic, gameEvent),
                        GameEventType.SCmdUpdateTargetPointEvent => GetSCmdUpdateTargetPointEvent(gameDic, gameEvent),
                        GameEventType.SCommandManagerStateEvent => GetSCommandManagerStateEvent(gameDic, gameEvent),
                        GameEventType.SControlGroupUpdateEvent => GetSControlGroupUpdateEvent(gameDic, gameEvent),
                        GameEventType.SGameUserLeaveEvent => GetSGameUserLeaveEvent(gameDic, gameEvent),
                        GameEventType.SSelectionDeltaEvent => GetSSelectionDeltaEvent(gameDic, gameEvent),
                        GameEventType.SSetSyncLoadingTimeEvent => GetSSetSyncLoadingTimeEvent(gameDic, gameEvent),
                        GameEventType.SSetSyncPlayingTimeEvent => GetSSetSyncPlayingTimeEvent(gameDic, gameEvent),
                        GameEventType.STriggerDialogControlEvent => GetSTriggerDialogControlEvent(gameDic, gameEvent),
                        GameEventType.STriggerPingEvent => GetSTriggerPingEvent(gameDic, gameEvent),
                        GameEventType.STriggerSoundLengthSyncEvent => new STriggerSoundLengthSyncEvent(gameEvent),
                        GameEventType.SUserFinishedLoadingSyncEvent => new SUserFinishedLoadingSyncEvent(gameEvent),
                        GameEventType.SUserOptionsEvent => GetSUserOptionsEvent(gameDic, gameEvent),
                        GameEventType.SCmdUpdateTargetUnitEvents => GetSCmdUpdateTargetUnitEvent(gameDic, gameEvent),
                        GameEventType.STriggerKeyPressedEvent => GetSTriggerKeyPressedEvent(gameDic, gameEvent),
                        GameEventType.SUnitClickEvent => GetSUnitClickEvent(gameDic, gameEvent),
                        GameEventType.SDecrementGameTimeRemainingEvent => GetSDecrementGameTimeRemainingEvent(gameDic, gameEvent),
                        GameEventType.STriggerChatMessageEvent => GetSTriggerChatMessageEvent(gameDic, gameEvent),
                        GameEventType.STriggerMouseClickedEvent => GetSTriggerMouseClickedEvent(gameDic, gameEvent),
                        //GameEventType.STriggerSoundtrackDoneEvent => GetSTriggerSoundtrackDoneEvent(gameDic, gameEvent),
                        //GameEventType.SCameraSaveEvent => GetSCameraSaveEvent(gameDic, gameEvent),
                        //GameEventType.STriggerCutsceneBookmarkFiredEvent => GetSTriggerCutsceneBookmarkFiredEvent(gameDic, gameEvent),
                        //GameEventType.STriggerCutsceneEndSceneFiredEvent => GetSTriggerCutsceneEndSceneFiredEvent(gameDic, gameEvent),
                        //GameEventType.STriggerSoundLengthQueryEvent => GetSTriggerSoundLengthQueryEvent(gameDic, gameEvent),
                        //GameEventType.STriggerSoundOffsetEvent => GetSTriggerSoundOffsetEvent(gameDic, gameEvent),
                        //GameEventType.STriggerTargetModeUpdateEvent => GetSTriggerTargetModeUpdateEvent(gameDic, gameEvent),
                        //GameEventType.STriggerTransmissionCompleteEvent => GetSTriggerTransmissionCompleteEvent(gameDic, gameEvent),
                        //GameEventType.SAchievementAwardedEvent => GetSAchievementAwardedEvent(gameDic, gameEvent),
                        //GameEventType.STriggerTransmissionOffsetEvent => GetSTriggerTransmissionOffsetEvent(gameDic, gameEvent),
                        //GameEventType.STriggerButtonPressedEvent => GetSTriggerButtonPressedEvent(gameDic, gameEvent),
                        //GameEventType.STriggerGameMenuItemSelectedEvent => GetSTriggerGameMenuItemSelectedEvent(gameDic, gameEvent),
                        //GameEventType.STriggerMouseMovedEvent => GetSTriggerMouseMovedEvent(gameDic, gameEvent),
                        _ => GetUnknownEvent(gameDic, gameEvent)
                    };
                    gameevents.Add(detailEvent);
                }
            }
            return new GameEvents(gameevents);
        }

        private static GameEvent GetGameEvent(Dictionary<string, object> dic)
        {
            int userId = GetUserId(dic);
            int eventId = GetInt(dic, "_eventid");
            string type = GetString(dic, "_event");
            int bits = GetInt(dic, "_bits");
            int gameloop = GetInt(dic, "_gameloop");
            return new GameEvent(userId, eventId, type, bits, gameloop);
        }

        private static int GetUserId(Dictionary<string, object> dic)
        {
            if (dic.TryGetValue("_userid", out var userValue) && userValue is Dictionary<string, object> userDic)
            {
                return GetInt(userDic, "m_userId");
            }
            return 0;
        }

        private static UnknownGameEvent GetUnknownEvent(Dictionary<string, object> dic, GameEvent gameEvent)
        {
            return new UnknownGameEvent(gameEvent, GetString(dic, "_event"));
        }

        private static STriggerDialogControlEvent GetSTriggerDialogControlEvent(Dictionary<string, object> dic, GameEvent gameEvent)
        {
            long m_controlId = GetBigInt(dic, "m_controlId");
            int? mouseButton = GetMouseButton(dic);
            string? textChanged = GetTextChanged(dic);
            long m_eventType = GetBigInt(dic, "m_eventType");
            return new STriggerDialogControlEvent(gameEvent, m_controlId, mouseButton, textChanged, m_eventType);
        }

        private static int? GetMouseButton(Dictionary<string, object> dic)
        {
            if (dic.TryGetValue("m_eventData", out var eventData) && eventData is Dictionary<string, object> mouseDic)
            {
                return GetNullableInt(mouseDic, "MouseButton");
            }
            return null;
        }

        private static string? GetTextChanged(Dictionary<string, object> dic)
        {
            if (dic.TryGetValue("m_eventData", out var eventData) && eventData is Dictionary<string, object> textDic)
            {
                return GetNullableString(textDic, "TextChanged");
            }
            return null;
        }

        private static SSetSyncPlayingTimeEvent GetSSetSyncPlayingTimeEvent(Dictionary<string, object> dic, GameEvent gameEvent)
        {
            int m_syncTime = GetInt(dic, "m_syncTime");
            return new SSetSyncPlayingTimeEvent(gameEvent, m_syncTime);
        }

        private static SSetSyncLoadingTimeEvent GetSSetSyncLoadingTimeEvent(Dictionary<string, object> dic, GameEvent gameEvent)
        {
            int m_syncTime = GetInt(dic, "m_syncTime");
            return new SSetSyncLoadingTimeEvent(gameEvent, m_syncTime);
        }

        private static SGameUserLeaveEvent GetSGameUserLeaveEvent(Dictionary<string, object> dic, GameEvent gameEvent)
        {
            int leaveReason = GetInt(dic, "m_leaveReason");
            return new SGameUserLeaveEvent(gameEvent, leaveReason);
        }

        private static SControlGroupUpdateEvent GetSControlGroupUpdateEvent(Dictionary<string, object> dic, GameEvent gameEvent)
        {
            int controlGroupUpdate = GetInt(dic, "m_controlGroupUpdate");
            return new SControlGroupUpdateEvent(gameEvent, controlGroupUpdate);
        }

        private static SCommandManagerStateEvent GetSCommandManagerStateEvent(Dictionary<string, object> dic, GameEvent gameEvent)
        {
            int state = GetInt(dic, "m_state");
            int? sequence = GetNullableInt(dic, "m_sequence");
            return new SCommandManagerStateEvent(gameEvent, state, sequence);
        }

        private static SCmdUpdateTargetPointEvent GetSCmdUpdateTargetPointEvent(Dictionary<string, object> dic, GameEvent gameEvent)
        {
            long x = 0;
            long y = 0;
            long z = 0;
            if (dic.TryGetValue("m_target", out var target) && target is Dictionary<string, object> targetDic)
            {
                x = GetBigInt(targetDic, "x");
                y = GetBigInt(targetDic, "y");
                z = GetBigInt(targetDic, "z");
            }
            return new SCmdUpdateTargetPointEvent(gameEvent, x, y, z);
        }

        private static SBankValueEvent GetSBankValueEvent(Dictionary<string, object> dic, GameEvent gameEvent)
        {
            string data = GetString(dic, "m_data");
            string name = GetString(dic, "m_name");
            int type = GetInt(dic, "m_type");
            return new SBankValueEvent(gameEvent, name, data, type);
        }

        private static SBankSignatureEvent GetSBankSignatureEvent(Dictionary<string, object> dic, GameEvent gameEvent)
        {
            string toonHandle = GetString(dic, "m_toonHandle");
            var signature = GetIntList(dic, "m_signature");
            return new SBankSignatureEvent(gameEvent, toonHandle, signature);
        }

        private static SBankSectionEvent GetSBankSectionEvent(Dictionary<string, object> dic, GameEvent gameEvent)
        {
            string name = GetString(dic, "m_name");
            return new SBankSectionEvent(gameEvent, name);
        }

        private static SBankKeyEvent GetSBankKeyEvent(Dictionary<string, object> dic, GameEvent gameEvent)
        {
            string data = GetString(dic, "m_data");
            string name = GetString(dic, "m_name");
            int type = GetInt(dic, "m_type");
            return new SBankKeyEvent(gameEvent, name, data, type);
        }

        private static SBankFileEvent GetSBankFileEvent(Dictionary<string, object> dic, GameEvent gameEvent)
        {
            string name = GetString(dic, "m_name");
            return new SBankFileEvent(gameEvent, name);
        }

        private static SCameraUpdateEvent GetSCameraUpdateEvent(Dictionary<string, object> dic, GameEvent gameEvent)
        {
            string? reason = GetNullableString(dic, "m_reason");
            int? distance = GetNullableInt(dic, "m_distance");
            int? yaw = GetNullableInt(dic, "m_yaw");
            int? pitch = GetNullableInt(dic, "m_pitch");
            bool follow = GetBool(dic, "m_follow");
            (long? targetX, long? targetY) = GetSCameraUpdateEventTarget(dic);
            return new SCameraUpdateEvent(gameEvent, reason, distance, targetX, targetY, yaw, pitch, follow);
        }

        private static (long?, long?) GetSCameraUpdateEventTarget(Dictionary<string, object> dic)
        {
            if (dic.TryGetValue("m_target", out object? target))
            {
                if (target != null)
                {
                    if (target is Dictionary<string, object> targetDic)
                    {
                        long x = GetBigInt(targetDic, "x");
                        long y = GetBigInt(targetDic, "y");
                        return (x, y);
                    }
                }
            }
            return (null, null);
        }

        private static SCmdEvent GetSCmdEvent(Dictionary<string, object> gameDic, GameEvent gameEvent)
        {
            int? unitGroup = GetNullableInt(gameDic, "m_unitGroup");
            (int abilLink, int abilCmdIndex, string? abilCmdData) = GetAbil(gameDic);
            int cmdFalgs = GetInt(gameDic, "m_cmdFlags");
            int sequence = GetInt(gameDic, "m_sequence");
            int? otherUnit = GetNullableInt(gameDic, "m_otherUnit");
            (long? targetX, long? targetY, long? targetZ) = GetSCmdEventTarget(gameDic);
            return new SCmdEvent(gameEvent, unitGroup, abilLink, abilCmdIndex, abilCmdData, targetX, targetY, targetZ, cmdFalgs, sequence, otherUnit);
        }

        private static (long?, long?, long?) GetSCmdEventTarget(Dictionary<string, object> dic)
        {
            if (dic.TryGetValue("m_data", out object? data))
            {
                if (data != null)
                {
                    if (dic.TryGetValue("TargetPoint", out object? target))
                    {
                        if (target != null)
                        {
                            if (target is Dictionary<string, object> targetDic)
                            {
                                long x = GetBigInt(targetDic, "x");
                                long y = GetBigInt(targetDic, "y");
                                long z = GetBigInt(targetDic, "z");
                                return (x, y, z);
                            }
                        }
                    }
                }
            }
            return (null, null, null);
        }

        private static (int, int, string?) GetAbil(Dictionary<string, object> dic)
        {
            if (dic.ContainsKey("m_abil"))
            {
                if (dic["m_abil"] is Dictionary<string, object> abilDic)
                {
                    int link = GetInt(abilDic, "m_abilLink");
                    int cmdIndex = GetInt(abilDic, "m_abilCmdIndex");
                    string? cmdData = GetNullableString(abilDic, "m_abilCmdData");
                    return (link, cmdIndex, cmdData);
                }
            }
            return (0, 0, null);
        }

        private static SSelectionDeltaEvent GetSSelectionDeltaEvent(Dictionary<string, object> gameDic, GameEvent gameEvent)
        {
            var delta = GetSelectionDeltaEventDelta(gameDic);
            int controlGroupId = GetInt(gameDic, "m_controlGroupId");
            return new SSelectionDeltaEvent(gameEvent, delta, controlGroupId);
        }

        private static SelectionDeltaEventDelta GetSelectionDeltaEventDelta(Dictionary<string, object> pydic)
        {
            if (pydic.TryGetValue("m_delta", out object? deltaObj) && deltaObj is Dictionary<string, object> deltaDic)
            {
                List<int> addUnitTags = GetIntList(deltaDic, "m_addUnitTags");
                List<SelectionDeltaEventDeltaSubGroup> subgroups = new();
                List<int> zeroIndices = new();

                if (deltaDic.TryGetValue("m_addSubgroups", out object? subGroupsObj) && subGroupsObj is List<object> subGroupList)
                {
                    foreach (var ent in subGroupList)
                    {
                        if (ent is Dictionary<string, object> subDic)
                        {
                            subgroups.Add(new SelectionDeltaEventDeltaSubGroup(
                                GetInt(subDic, "m_unitLink"),
                                GetInt(subDic, "m_subgroupPriority"),
                                GetInt(subDic, "m_count"),
                                GetInt(subDic, "m_intraSubgroupPriority")
                            ));
                        }
                    }
                }

                if (deltaDic.TryGetValue("m_removeMask", out object? removeMaskObj) && removeMaskObj is Dictionary<string, object> removeDic)
                {
                    zeroIndices = GetIntList(removeDic, "ZeroIndices");
                }

                int subgroupIndex = GetInt(deltaDic, "m_subgroupIndex");
                return new SelectionDeltaEventDelta(addUnitTags, subgroups, zeroIndices, subgroupIndex);
            }

            return new SelectionDeltaEventDelta(new List<int>(), new List<SelectionDeltaEventDeltaSubGroup>(), new List<int>(), 0);
        }

        private static STriggerPingEvent GetSTriggerPingEvent(Dictionary<string, object> gameDic, GameEvent gameEvent)
        {
            bool pingedMinimap = GetBool(gameDic, "m_pingedMinimap");
            int unitLink = GetInt(gameDic, "m_unitLink");
            bool unitIsUnderConstruction = GetBool(gameDic, "m_unitIsUnderConstruction");
            long option = GetBigInt(gameDic, "m_option");
            int unit = GetInt(gameDic, "m_unit");
            (long unitX, long unitY, long unitZ) = GetUnitPosition(gameDic);
            int? unitControlPlayerId = GetNullableInt(gameDic, "m_unitControlPlayerId");
            (long pointX, long pointY) = GetPoint(gameDic);
            int? unitUpkeepPlayerId = GetNullableInt(gameDic, "m_unitUpkeepPlayerId");
            return new STriggerPingEvent(gameEvent,
                                         pingedMinimap,
                                         unitLink,
                                         unitIsUnderConstruction,
                                         option,
                                         unit,
                                         unitX,
                                         unitY,
                                         unitZ,
                                         unitControlPlayerId,
                                         pointX,
                                         pointY,
                                         unitUpkeepPlayerId);
        }

        private static (long, long) GetPoint(Dictionary<string, object> pydic)
        {
            if (pydic.ContainsKey("m_point"))
            {
                if (pydic["m_point"] is Dictionary<string, object> pointDic)
                {
                    long x = GetBigInt(pointDic, "x");
                    long y = GetBigInt(pointDic, "y");
                    return (x, y);
                }
            }
            return (0, 0);
        }

        private static (long, long, long) GetUnitPosition(Dictionary<string, object> pydic)
        {
            if (pydic.ContainsKey("m_unitPosition"))
            {
                if (pydic["m_unitPosition"] is Dictionary<string, object> posDic)
                {
                    long x = GetBigInt(posDic, "x");
                    long y = GetBigInt(posDic, "y");
                    long z = GetBigInt(posDic, "z");
                    return (x, y, z);
                }
            }
            return (0, 0, 0);
        }

        private static SUserOptionsEvent GetSUserOptionsEvent(Dictionary<string, object> gameDic, GameEvent gameEvent)
        {
            bool testCheatsEnabled = GetBool(gameDic, "m_testCheatsEnabled");
            bool multiplayerCheatsEnabled = GetBool(gameDic, "m_multiplayerCheatsEnabled");
            bool gameFullyDownloaded = GetBool(gameDic, "m_gameFullyDownloaded");
            string hotkeyProfile = GetString(gameDic, "m_hotkeyProfile");
            bool useGalaxyAsserts = GetBool(gameDic, "m_useGalaxyAsserts");
            bool debugPauseEnabled = GetBool(gameDic, "m_debugPauseEnabled");
            bool cameraFollow = GetBool(gameDic, "m_cameraFollow");
            bool isMapToMapTransition = GetBool(gameDic, "m_isMapToMapTransition");
            int buildNum = GetInt(gameDic, "m_buildNum");
            int versionFlags = GetInt(gameDic, "m_versionFlags");
            bool developmentCheatsEnabled = GetBool(gameDic, "m_developmentCheatsEnabled");
            bool platformMac = GetBool(gameDic, "m_platformMac");
            int baseBuildNum = GetInt(gameDic, "m_baseBuildNum");
            bool syncChecksummingEnabled = GetBool(gameDic, "m_syncChecksummingEnabled");
            return new SUserOptionsEvent(gameEvent,
                                         testCheatsEnabled,
                                         multiplayerCheatsEnabled,
                                         gameFullyDownloaded,
                                         hotkeyProfile,
                                         useGalaxyAsserts,
                                         debugPauseEnabled,
                                         cameraFollow,
                                         isMapToMapTransition,
                                         buildNum,
                                         versionFlags,
                                         developmentCheatsEnabled,
                                         platformMac,
                                         baseBuildNum,
                                         syncChecksummingEnabled);
        }

        private static SCmdUpdateTargetUnitEvent GetSCmdUpdateTargetUnitEvent(Dictionary<string, object> dic, GameEvent gameEvent)
        {
            if (dic.TryGetValue("m_target", out object? target))
            {
                if (target is Dictionary<string, object> targetDic)
                {
                    int m_snapshotControlPlayerId = GetInt(targetDic, "m_snapshotControlPlayerId");
                    (long pointX, long pointY, long pointZ) = GetSnapshotPoint(targetDic);
                    int m_snapshotUpkeepPlayerId = GetInt(targetDic, "m_snapshotUpkeepPlayerId");
                    int m_timer = GetInt(targetDic, "m_timer");
                    int m_targetUnitFlags = GetInt(targetDic, "m_targetUnitFlags");
                    int m_snapshotUnitLink = GetInt(targetDic, "m_snapshotUnitLink");
                    int m_tag = GetInt(targetDic, "m_tag");
                    return new SCmdUpdateTargetUnitEvent(gameEvent,
                                                         m_snapshotControlPlayerId,
                                                         pointX,
                                                         pointY,
                                                         pointZ,
                                                         m_snapshotUpkeepPlayerId,
                                                         m_timer,
                                                         m_targetUnitFlags,
                                                         m_snapshotUnitLink,
                                                         m_tag);
                }
            }
            return new SCmdUpdateTargetUnitEvent(gameEvent,
                                         0,
                                         0,
                                         0,
                                         0,
                                         0,
                                         0,
                                         0,
                                         0,
                                         0);
        }

        private static (long, long, long) GetSnapshotPoint(Dictionary<string, object> dic)
        {
            if (dic.TryGetValue("m_snapshotPoint", out object? point))
            {
                if (point is Dictionary<string, object> pointDic)
                {
                    return (GetBigInt(pointDic, "x"), GetBigInt(pointDic, "y"), GetBigInt(pointDic, "z"));
                }
            }
            return (0, 0, 0);
        }

        private static STriggerKeyPressedEvent GetSTriggerKeyPressedEvent(Dictionary<string, object> dic, GameEvent gameEvent)
        {
            int flags = GetInt(dic, "m_flags");
            int key = GetInt(dic, "m_key");
            return new STriggerKeyPressedEvent(gameEvent, flags, key);
        }

        private static SUnitClickEvent GetSUnitClickEvent(Dictionary<string, object> dic, GameEvent gameEvent)
        {
            int unitTag = GetInt(dic, "m_unitTag");
            return new SUnitClickEvent(gameEvent, unitTag);
        }

        private static SDecrementGameTimeRemainingEvent GetSDecrementGameTimeRemainingEvent(Dictionary<string, object> dic, GameEvent gameEvent)
        {
            int decerementSeconds = GetInt(dic, "m_decerementSeconds");
            return new SDecrementGameTimeRemainingEvent(gameEvent, decerementSeconds);
        }

        private static STriggerChatMessageEvent GetSTriggerChatMessageEvent(Dictionary<string, object> dic, GameEvent gameEvent)
        {
            string chatMessage = GetString(dic, "m_chatMessage");
            return new STriggerChatMessageEvent(gameEvent, chatMessage);
        }

        private static STriggerMouseClickedEvent GetSTriggerMouseClickedEvent(Dictionary<string, object> dic, GameEvent gameEvent)
        {
            bool down = GetBool(dic, "m_down");
            int button = GetInt(dic, "m_button");
            int flags = GetInt(dic, "m_flags");
            (long posX, long posY) = GetPosUI(dic);
            return new STriggerMouseClickedEvent(gameEvent, down, button, flags, posX, posY);
        }

        private static (long posX, long posY) GetPosUI(Dictionary<string, object> dic)
        {
            if (dic.TryGetValue("m_posUI", out object? pos))
            {
                if (pos is Dictionary<string, object> posDic)
                {
                    return (GetBigInt(posDic, "x"), GetBigInt(posDic, "y"));
                }
            }
            return (0, 0);
        }
    }
}
