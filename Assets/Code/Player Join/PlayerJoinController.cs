using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;
using System.Linq;

public class PlayerJoinController : MonoBehaviour
{
    public Animator StartGameAnimator;

    private int gamePlayerIdCounter = 0;

    void Update()
    {
        for (int i = 0; i < ReInput.players.allPlayerCount - 1; i++)
        {
            if (ReInput.players.GetPlayer(i).GetButtonDown("Enter") && !GameData.k_RawRewiredPlayerIds.Contains(ReInput.players.GetPlayer(i).id))
            {
                GameData.k_Players[System.Array.IndexOf(GameData.k_Players, null)] =  new PlayerData(ReInput.players.GetPlayer(i).id, gamePlayerIdCounter);
                GameData.k_RawRewiredPlayerIds.Add(ReInput.players.GetPlayer(i).id);
                gamePlayerIdCounter++;
            }
        }
        
        int readyPlayers = GameData.k_Players.Where(x => x != null && x.PanelData != null && x.PanelData.PlayerLocked == true).Count();
        if (readyPlayers > 1 && StartGameAnimator.GetBool("GameCanStart") == false)
        {
            StartGameAnimator.SetBool("GameCanStart", true);
        }
        else if (readyPlayers < 1 && StartGameAnimator.GetBool("GameCanStart") == true)
        {
            StartGameAnimator.SetBool("GameCanStart", false);
        }
        else if (readyPlayers > 1 && StartGameAnimator.GetBool("GameCanStart") == true)
        {
            foreach (PlayerData player in GameData.GetNonNullPlayers().Where(x => x.PanelData.PlayerLocked == true))
            {
                if (ReInput.players.GetPlayer(player.RewiredPlayerId).GetButtonDown("Enter"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
    }
}
