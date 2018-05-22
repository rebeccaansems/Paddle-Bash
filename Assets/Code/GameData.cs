using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using System.Linq;

public class GameData : MonoBehaviour
{
    public static PlayerData[] k_Players;
    public static List<int> k_RawRewiredPlayerIds;
    public static int k_CurrentLevel;
    public static bool k_InputBlocked;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        k_Players = new PlayerData[4];
        k_RawRewiredPlayerIds = new List<int>();
        k_InputBlocked = false;
    }

    public static PlayerData[] GetNonNullPlayers()
    {
        return k_Players.Where(x => x != null).ToArray();
    }
}
