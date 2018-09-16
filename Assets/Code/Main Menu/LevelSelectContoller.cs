using Rewired;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectContoller : MonoBehaviour
{
    public Animator LevelSelectAnimator, PlayerPanelsAnimator, CurrentLevelAnimator, ContinueAnimator, EditLevelAnimator;
    public CanvasGroup EditLevelCanvas;
    public Animator[] SinglePlayerPanels;

    public Image CurrentLevel;

    public Text CurrentLevelText;

    public void Update()
    {
        if (SessionData.Instance.CurrentMenuScreen != SessionData.MenuScreens.LevelSelect)
        {
            SessionData.Instance.CurrentMenuScreen = SessionData.MenuScreens.LevelSelect;
        }

        ContinueAnimator.SetBool("GameCanStart", !SessionData.Instance.InputBlocked && LevelSelectAnimator.GetBool("isOnLevelSelectScreen"));

        foreach (PlayerData player in SessionData.Instance.GetNonNullPlayers().Where(x => x.PanelData.PlayerLocked == true))
        {
            if (ReInput.players.GetPlayer(player.RewiredPlayerId).GetButtonDown("Enter") && !SessionData.Instance.InputBlocked)
            {
                LevelSelectAnimator.SetBool("isOnLevelSelectScreen", false);
                ContinueAnimator.SetBool("GameCanStart", false);

                StartCoroutine(DisableInput());

                EditLevelCanvas.alpha = 1;
                EditLevelCanvas.interactable = true;

                this.GetComponent<LevelSelectContoller>().enabled = false;
                this.GetComponent<EditLevelController>().enabled = true;

                EditLevelAnimator.SetBool("isOnEditLevelScreen", true);
            }
            else if (LevelSelectAnimator.GetBool("isOnLevelSelectScreen") == true)
            {
                if (ReInput.players.GetPlayer(player.GamePlayerId).GetButtonDown("Back") && !SessionData.Instance.InputBlocked)
                {
                    this.GetComponent<LevelSelectContoller>().enabled = false;
                    this.GetComponent<PlayerJoinController>().enabled = true;

                    ContinueAnimator.SetBool("GameCanStart", false);
                    LevelSelectAnimator.SetBool("isOnLevelSelectScreen", false);
                    PlayerPanelsAnimator.SetBool("IsOnPlayerScreen", true);

                    foreach (Animator anim in SinglePlayerPanels)
                    {
                        anim.SetBool("IsOnPlayerScreen", true);
                        anim.SetBool("PlayerLockedIn", true);
                    }
                    StartCoroutine(DisableInput());
                }
                else if (ReInput.players.GetPlayer(player.GamePlayerId).GetAxis("Horizontal Menu") > 0 && CurrentLevelAnimator.GetBool("newLevel") == false)
                {
                    PlayerPanelsAnimator.SetBool("IsOnPlayerScreen", false);
                    CurrentLevelAnimator.SetBool("newLevel", true);
                    StartCoroutine(FinishLevelChangeAnimation(1));
                }
                else if (ReInput.players.GetPlayer(player.GamePlayerId).GetAxis("Horizontal Menu") < 0 && CurrentLevelAnimator.GetBool("newLevel") == false)
                {
                    PlayerPanelsAnimator.SetBool("IsOnPlayerScreen", false);
                    CurrentLevelAnimator.SetBool("newLevel", true);
                    StartCoroutine(FinishLevelChangeAnimation(-1));
                }
            }
        }
    }


    IEnumerator FinishLevelChangeAnimation(int change)
    {
        yield return new WaitForSeconds(0.3f);

        SessionData.Instance.CurrentLevel += change;
        if (SessionData.Instance.CurrentLevel < 0)
        {
            SessionData.Instance.CurrentLevel = SessionData.Instance.GameplayLevels.Length - 1;
        }
        else if (SessionData.Instance.CurrentLevel == SessionData.Instance.GameplayLevels.Length)
        {
            SessionData.Instance.CurrentLevel = 0;
        }

        CurrentLevel.sprite = SessionData.Instance.AllMapImages[SessionData.Instance.CurrentLevel];
        CurrentLevelText.text = "MAP " + (SessionData.Instance.CurrentLevel + 1).ToString("00");

        yield return new WaitForSeconds(0.5f);

        CurrentLevelAnimator.SetBool("newLevel", false);
    }

    IEnumerator DisableInput()
    {
        SessionData.Instance.InputBlocked = true;
        yield return new WaitForSeconds(3);
        SessionData.Instance.InputBlocked = false;
    }
}
