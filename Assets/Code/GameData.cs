using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class GameData : MonoBehaviour
{

    public static List<PlayerMap> k_Players;
    public static List<int> k_RawRewiredPlayerIds;

    // Use this for initialization
    void Start()
    {
        k_Players = new List<PlayerMap>();
        k_RawRewiredPlayerIds = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
