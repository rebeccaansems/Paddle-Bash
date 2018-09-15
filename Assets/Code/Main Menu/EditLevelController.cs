using Rewired;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EditLevelController : MonoBehaviour
{
    public GameObject[] AllEditableValues;

    private GameObject overallController;
    private int currentEditable;
    private bool canUpdateEditable;

    private void Start()
    {
        canUpdateEditable = true;

        overallController = GameObject.FindGameObjectWithTag("Overall Controller");
        HighLightEditableItem(true, AllEditableValues[currentEditable]);
    }

    void Update()
    {
        if (GameData.k_CurrentMenuScreen != GameData.MenuScreens.EditLevel)
        {
            GameData.k_CurrentMenuScreen = GameData.MenuScreens.EditLevel;
        }

        foreach (PlayerData player in GameData.GetNonNullPlayers().Where(x => x.PanelData.PlayerLocked == true))
        {
            if (ReInput.players.GetPlayer(player.RewiredPlayerId).GetButtonDown("Enter") && !GameData.k_InputBlocked)
            {
                //LevelSelectAnimator.SetBool("isOnLevelSelectScreen", false);
                //ContinueAnimator.SetBool("GameCanStart", false);

                //StartCoroutine(DisableInput());

                //EditLevelCanvas.alpha = 1;
                //EditLevelCanvas.interactable = true;

                //this.GetComponent<LevelSelectContoller>().enabled = false;
                //this.GetComponent<EditLevelController>().enabled = true;

                //EditLevelAnimator.SetBool("isOnEditLevelScreen", true);
            }
            else if (Mathf.Abs(ReInput.players.GetPlayer(player.GamePlayerId).GetAxis("Vertical Menu")) > 0.5f && canUpdateEditable)
            {
                StartCoroutine(WaitForEditUpdate(ReInput.players.GetPlayer(player.GamePlayerId)));
                //else if (LevelSelectAnimator.GetBool("isOnLevelSelectScreen") == true)
                //{
                //if (ReInput.players.GetPlayer(player.GamePlayerId).GetButtonDown("Back") && !GameData.k_InputBlocked)
                //{
                //    this.GetComponent<LevelSelectContoller>().enabled = false;
                //    this.GetComponent<PlayerJoinController>().enabled = true;

                //    ContinueAnimator.SetBool("GameCanStart", false);
                //    LevelSelectAnimator.SetBool("isOnLevelSelectScreen", false);
                //    PlayerPanelsAnimator.SetBool("IsOnPlayerScreen", true);

                //    foreach (Animator anim in SinglePlayerPanels)
                //    {
                //        anim.SetBool("IsOnPlayerScreen", true);
                //        anim.SetBool("PlayerLockedIn", true);
                //    }
                //    StartCoroutine(DisableInput());
                //}
                //else if (ReInput.players.GetPlayer(player.GamePlayerId).GetAxis("Horizontal Menu") > 0 && CurrentLevelAnimator.GetBool("newLevel") == false)
                //{
                //    PlayerPanelsAnimator.SetBool("IsOnPlayerScreen", false);
                //    CurrentLevelAnimator.SetBool("newLevel", true);
                //    StartCoroutine(FinishLevelChangeAnimation(1));
                //}
                //else if (ReInput.players.GetPlayer(player.GamePlayerId).GetAxis("Horizontal Menu") < 0 && CurrentLevelAnimator.GetBool("newLevel") == false)
                //{
                //    PlayerPanelsAnimator.SetBool("IsOnPlayerScreen", false);
                //    CurrentLevelAnimator.SetBool("newLevel", true);
                //    StartCoroutine(FinishLevelChangeAnimation(-1));
                //}
                //}
            }
        }
    }

    private IEnumerator WaitForEditUpdate(Player currentPlayer)
    {
        canUpdateEditable = false;
        UpdateEditable(currentPlayer.GetAxis("Vertical Menu"));
        yield return new WaitForSeconds(0.3f);
        canUpdateEditable = true;
    }

    public void UpdateEditable(float change)
    {
        if (change > 0)
        {
            change = -1;
        }
        else if (change < 0)
        {
            change = 1;
        }

        HighLightEditableItem(false, AllEditableValues[currentEditable]);
        currentEditable += (int)change;

        if (currentEditable < 0)
        {
            currentEditable = AllEditableValues.Length - 1;
        }
        else if (currentEditable == AllEditableValues.Length)
        {
            currentEditable = 0;
        }
        HighLightEditableItem(true, AllEditableValues[currentEditable]);
    }

    private void HighLightEditableItem(bool highlight, GameObject EditItem)
    {
        if (highlight)
        {
            ChangeColorStyle(EditItem, Color.yellow, FontStyle.Italic);
        }
        else
        {
            ChangeColorStyle(EditItem, Color.white, FontStyle.Normal);
        }
    }

    private void ChangeColorStyle(GameObject EditItem, Color color, FontStyle style)
    {
        foreach (Image image in EditItem.GetComponentsInChildren<Image>())
        {
            image.color = color;
        }

        foreach (Text text in EditItem.GetComponentsInChildren<Text>())
        {
            text.color = color;
            text.fontStyle = style;
        }
    }

}
