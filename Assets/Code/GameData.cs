using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class GameData : MonoBehaviour
{
    public static List<PlayerData> k_Players;
    public static List<int> k_RawRewiredPlayerIds;

    void Start()
    {
        k_Players = new List<PlayerData>();
        k_RawRewiredPlayerIds = new List<int>();
    }
}
