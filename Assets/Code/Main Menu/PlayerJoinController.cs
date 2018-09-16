using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using System.Linq;

public class PlayerJoinController : MonoBehaviour
{
    public Animator ContinueAnimator, PlayerPanelsAnimator;
    public CanvasGroup JoinCanvas, LevelSelectCanvas, EditLevelCanvas;
    public Animator[] SinglePlayerPanels;

    private Animator levelSelectAnimator;

    private int gamePlayerIdCounter = 0;

    private void Start()
    {
        JoinCanvas.alpha = 1;
        JoinCanvas.interactable = true;

        LevelSelectCanvas.alpha = 0;
        LevelSelectCanvas.interactable = false;

        EditLevelCanvas.alpha = 0;
        EditLevelCanvas.interactable = false;

        this.GetComponent<LevelSelectContoller>().enabled = false;
        this.GetComponent<EditLevelController>().enabled = false;

        levelSelectAnimator = LevelSelectCanvas.GetComponentsInChildren<Animator>()[0];
    }

    void Update()
    {
        if (SessionData.Instance.CurrentMenuScreen != SessionData.MenuScreens.PlayerJoin)
        {
            SessionData.Instance.CurrentMenuScreen = SessionData.MenuScreens.PlayerJoin;
        }

        for (int i = 0; i < ReInput.players.allPlayerCount - 1; i++)
        {
            if (ReInput.players.GetPlayer(i).GetButtonDown("Enter") && !SessionData.Instance.RawRewiredPlayerIds.Contains(ReInput.players.GetPlayer(i).id))
            {
                SessionData.Instance.Players[System.Array.IndexOf(SessionData.Instance.Players, null)] = new PlayerData(ReInput.players.GetPlayer(i).id, gamePlayerIdCounter);
                SessionData.Instance.RawRewiredPlayerIds.Add(ReInput.players.GetPlayer(i).id);
                gamePlayerIdCounter++;
            }
        }

        int readyPlayers = SessionData.Instance.Players.Where(x => x != null && x.PanelData != null && x.PanelData.PlayerLocked == true).Count();
        if (readyPlayers > 1 && !ContinueAnimator.GetBool("GameCanStart"))
        {
            if (levelSelectAnimator.GetBool("isOnLevelSelectScreen") == false && !SessionData.Instance.InputBlocked)
            {
                ContinueAnimator.SetBool("GameCanStart", true);
            }
        }
        else if (readyPlayers <= 1 && ContinueAnimator.GetBool("GameCanStart"))
        {
            ContinueAnimator.SetBool("GameCanStart", false);
        }
        else if (readyPlayers > 1 && ContinueAnimator.GetBool("GameCanStart"))
        {
            foreach (PlayerData player in SessionData.Instance.GetNonNullPlayers().Where(x => x.PanelData.PlayerLocked == true))
            {
                if (ReInput.players.GetPlayer(player.RewiredPlayerId).GetButtonDown("Enter") && !SessionData.Instance.InputBlocked
                    && levelSelectAnimator.GetBool("isOnLevelSelectScreen") == false && SessionData.Instance.ReadyPlayersJoined == 2)
                {
                    foreach (Animator anim in SinglePlayerPanels)
                    {
                        anim.SetBool("IsOnPlayerScreen", false);
                    }
                    PlayerPanelsAnimator.SetBool("IsOnPlayerScreen", false);
                    ContinueAnimator.SetBool("GameCanStart", false);

                    StartCoroutine(DisableInput());

                    LevelSelectCanvas.alpha = 1;
                    LevelSelectCanvas.interactable = true;

                    this.GetComponent<PlayerJoinController>().enabled = false;
                    this.GetComponent<LevelSelectContoller>().enabled = true;
                    levelSelectAnimator.SetBool("isOnLevelSelectScreen", true);
                }
            }
        }
    }


    IEnumerator DisableInput()
    {
        SessionData.Instance.InputBlocked = true;
        yield return new WaitForSeconds(1);
        SessionData.Instance.InputBlocked = false;
    }
}
