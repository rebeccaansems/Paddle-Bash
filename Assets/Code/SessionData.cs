using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using System.Linq;

public class SessionData : MonoBehaviour
{
    private static SessionData _instance;

    public static SessionData Instance { get { return _instance; } }
    
    public PlayerData[] Players;
    public int[] GameplayLevels;
    [HideInInspector]
    public List<int> RawRewiredPlayerIds;
    public int CurrentLevel, ReadyPlayersJoined;
    public MenuScreens CurrentMenuScreen;
    public bool InputBlocked;

    public enum MenuScreens
    {
        PlayerJoin,
        LevelSelect,
        EditLevel
    }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        Players = new PlayerData[4];
        RawRewiredPlayerIds = new List<int>();
        InputBlocked = false;
        ReadyPlayersJoined = 0;
        CurrentMenuScreen = MenuScreens.PlayerJoin;
    }

    public PlayerData[] GetNonNullPlayers()
    {
        return Players.Where(x => x != null).ToArray();
    }
}

