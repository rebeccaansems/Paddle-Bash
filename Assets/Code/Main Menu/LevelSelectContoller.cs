using Rewired;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectContoller : MonoBehaviour
{
    public int[] GameplayLevels;

    public Animator LevelSelectAnimator, PlayerPanelsAnimator, CurrentLevelAnimator, ContinueAnimator;
    public Animator[] SinglePlayerPanels;

    public Image CurrentLevel;
    public Sprite[] GameplayLevelsArt;

    public Text CurrentLevelText;

    private GameObject overallController;

    private void Start()
    {
        overallController = GameObject.FindGameObjectWithTag("Overall Controller");
    }

    public void Update()
    {
        ContinueAnimator.SetBool("GameCanStart", !GameData.k_InputBlocked && LevelSelectAnimator.GetBool("isOnLevelSelectScreen"));

        foreach (PlayerData player in GameData.GetNonNullPlayers().Where(x => x.PanelData.PlayerLocked == true))
        {
            if (ReInput.players.GetPlayer(player.RewiredPlayerId).GetButtonDown("Enter") && !GameData.k_InputBlocked)
            {
                overallController.GetComponent<LevelLoader>().LoadLevel(GameplayLevels[GameData.k_CurrentLevel]);
            }
            else if (LevelSelectAnimator.GetBool("isOnLevelSelectScreen") == true)
            {
                if (ReInput.players.GetPlayer(player.GamePlayerId).GetButtonDown("Back") && !GameData.k_InputBlocked)
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

        GameData.k_CurrentLevel += change;
        if (GameData.k_CurrentLevel < 0)
        {
            GameData.k_CurrentLevel = GameplayLevels.Length - 1;
        }
        else if (GameData.k_CurrentLevel == GameplayLevels.Length)
        {
            GameData.k_CurrentLevel = 0;
        }

        CurrentLevel.sprite = GameplayLevelsArt[GameData.k_CurrentLevel];
        CurrentLevelText.text = "LEVEL " + (GameData.k_CurrentLevel + 1).ToString("00");

        yield return new WaitForSeconds(0.5f);

        CurrentLevelAnimator.SetBool("newLevel", false);
    }

    IEnumerator DisableInput()
    {
        GameData.k_InputBlocked = true;
        yield return new WaitForSeconds(3);
        GameData.k_InputBlocked = false;
    }
}
