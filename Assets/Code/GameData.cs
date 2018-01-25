using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using System.Linq;

public class GameData : MonoBehaviour
{
    public static PlayerData[] k_Players;
    public static List<int> k_RawRewiredPlayerIds;

    void Start()
    {
        k_Players = new PlayerData[4];
        k_RawRewiredPlayerIds = new List<int>();
    }

    public static PlayerData[] GetNonNullPlayers()
    {
        return k_Players.Where(x => x != null).ToArray();
    }
}
