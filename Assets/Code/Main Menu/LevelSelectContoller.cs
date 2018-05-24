using Rewired;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectContoller : MonoBehaviour
{
    public Animator LevelSelectAnimator, PlayerPanelsAnimator, CurrentLevelAnimator, ContinueAnimator;
    public Animator[] SinglePlayerPanels;

    public Image CurrentLevel;
    private LevelData[] levelData;

    public Text CurrentLevelText;

    private GameObject overallController;

    private void Start()
    {
        overallController = GameObject.FindGameObjectWithTag("Overall Controller");
        var levelObj = Resources.LoadAll("LevelData", typeof(LevelData)).ToArray();
        levelData = Array.ConvertAll(levelObj, item => item as LevelData);
    }

    public void Update()
    {
        if (GameData.k_CurrentMenuScreen != GameData.MenuScreens.LevelSelect)
        {
            GameData.k_CurrentMenuScreen = GameData.MenuScreens.LevelSelect;
        }

        ContinueAnimator.SetBool("GameCanStart", !GameData.k_InputBlocked && LevelSelectAnimator.GetBool("isOnLevelSelectScreen"));

        foreach (PlayerData player in GameData.GetNonNullPlayers().Where(x => x.PanelData.PlayerLocked == true))
        {
            if (ReInput.players.GetPlayer(player.RewiredPlayerId).GetButtonDown("Enter") && !GameData.k_InputBlocked)
            {
                overallController.GetComponent<LevelLoader>().LoadLevel(levelData[GameData.k_CurrentLevel].SceneNumber);
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
            GameData.k_CurrentLevel = levelData.Length - 1;
        }
        else if (GameData.k_CurrentLevel == levelData.Length)
        {
            GameData.k_CurrentLevel = 0;
        }

        CurrentLevel.sprite = levelData[GameData.k_CurrentLevel].LevelArt;
        CurrentLevelText.text = levelData[GameData.k_CurrentLevel].LevelName;

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
