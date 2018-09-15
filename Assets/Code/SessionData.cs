using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using System.Linq;

public class SessionData : MonoBehaviour
{
    public static PlayerData[] k_Players;
    public static int[] k_GameplayLevels;
    public static List<int> k_RawRewiredPlayerIds;
    public static int k_CurrentLevel, k_ReadyPlayersJoined;
    public static MenuScreens k_CurrentMenuScreen;
    public static bool k_InputBlocked;

    public enum MenuScreens
    {
        PlayerJoin,
        LevelSelect,
        EditLevel
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        k_Players = new PlayerData[4];
        k_RawRewiredPlayerIds = new List<int>();
        k_InputBlocked = false;
        k_ReadyPlayersJoined = 0;
        k_CurrentMenuScreen = MenuScreens.PlayerJoin;
    }

    public static PlayerData[] GetNonNullPlayers()
    {
        return k_Players.Where(x => x != null).ToArray();
    }
}
