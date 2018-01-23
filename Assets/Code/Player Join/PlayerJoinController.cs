using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerJoinController : MonoBehaviour
{
    private int gamePlayerIdCounter = 0;

    void Update()
    {
        for (int i = 0; i < ReInput.players.allPlayerCount - 1; i++)
        {
            if (ReInput.players.GetPlayer(i).GetButtonDown("Enter") && !GameData.k_RawRewiredPlayerIds.Contains(ReInput.players.GetPlayer(i).id))
            {
                GameData.k_RawRewiredPlayerIds.Add(ReInput.players.GetPlayer(i).id);
                GameData.k_Players.Add(new PlayerData(ReInput.players.GetPlayer(i).id, gamePlayerIdCounter));
                gamePlayerIdCounter++;
            }
        }
    }
}
