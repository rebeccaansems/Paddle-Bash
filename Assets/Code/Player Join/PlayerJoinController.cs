using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;
using System.Linq;

public class PlayerJoinController : MonoBehaviour
{
    public Animator StartGameAnimator, PlayerPanelsAnimator;
    public CanvasGroup JoinCanvas, LevelSelectCanvas;
    public Animator[] SinglePlayerPanels;

    private int gamePlayerIdCounter = 0;

    private void Start()
    {
        JoinCanvas.alpha = 1;
        JoinCanvas.interactable = true;

        LevelSelectCanvas.alpha = 0;
        LevelSelectCanvas.interactable = false;
    }

    void Update()
    {
        for (int i = 0; i < ReInput.players.allPlayerCount - 1; i++)
        {
            if (ReInput.players.GetPlayer(i).GetButtonDown("Enter") && !GameData.k_RawRewiredPlayerIds.Contains(ReInput.players.GetPlayer(i).id))
            {
                GameData.k_Players[System.Array.IndexOf(GameData.k_Players, null)] = new PlayerData(ReInput.players.GetPlayer(i).id, gamePlayerIdCounter);
                GameData.k_RawRewiredPlayerIds.Add(ReInput.players.GetPlayer(i).id);
                gamePlayerIdCounter++;
            }
        }

        int readyPlayers = GameData.k_Players.Where(x => x != null && x.PanelData != null && x.PanelData.PlayerLocked == true).Count();
        if (readyPlayers > 1 && StartGameAnimator.GetBool("GameCanStart") == false)
        {
            StartGameAnimator.SetBool("GameCanStart", true);
        }
        else if (readyPlayers <= 1 && StartGameAnimator.GetBool("GameCanStart") == true)
        {
            StartGameAnimator.SetBool("GameCanStart", false);
        }
        else if (readyPlayers > 1 && StartGameAnimator.GetBool("GameCanStart") == true)
        {
            foreach (PlayerData player in GameData.GetNonNullPlayers().Where(x => x.PanelData.PlayerLocked == true))
            {
                if (ReInput.players.GetPlayer(player.RewiredPlayerId).GetButtonDown("Enter"))
                {
                    foreach(Animator anim in SinglePlayerPanels)
                    {
                        anim.SetBool("IsOnPlayerScreen", false);
                    }
                    PlayerPanelsAnimator.SetBool("IsOnPlayerScreen", false);
                    //Animate levels in
                }
            }
        }
    }
}
