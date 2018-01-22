using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerJoinController : MonoBehaviour
{
    private int gamePlayerIdCounter = 0;

    void Update()
    {
        foreach (Player player in ReInput.players.AllPlayers)
        {
            if (player.GetButtonDown("Enter") && !GameData.k_RawRewiredPlayerIds.Contains(player.id))
            {
                GameData.k_RawRewiredPlayerIds.Add(player.id);
                AssignNextPlayer(gamePlayerIdCounter);
            }
        }
    }

    void AssignNextPlayer(int rewiredPlayerId)
    {
        int gamePlayerId = GameData.k_Players.Count;
        GameData.k_Players.Add(new PlayerMap(rewiredPlayerId, gamePlayerId));
        Player rewiredPlayer = ReInput.players.GetPlayer(rewiredPlayerId);
        gamePlayerIdCounter++;
    }
}
