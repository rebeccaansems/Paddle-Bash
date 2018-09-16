using Rewired;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EditLevelController : MonoBehaviour
{
    public GameObject[] AllEditableValues, AllPlayersPaddles;
    public Animator ContinueAnimator, LevelSelectAnimator, EditLevelAnimator;
    public Image CurrentLevelArtImage;
    public Text CurrentLevelText;

    private GameObject overallController;
    private int[] currentEditableValues;
    private int currentEditableItem;
    private bool canUpdateEditable;

    private void OnEnable()
    {
        overallController = GameObject.FindGameObjectWithTag("Overall Controller");

        canUpdateEditable = true;
        currentEditableValues = new int[] { 2, 2, 2, 2 };

        HighLightEditableItem(false, AllEditableValues[currentEditableItem]);
        currentEditableItem = 0;
        HighLightEditableItem(true, AllEditableValues[currentEditableItem]);

        CurrentLevelArtImage.sprite = SessionData.Instance.AllMapImages[SessionData.Instance.CurrentLevel];
        CurrentLevelText.text = "MAP " + (SessionData.Instance.CurrentLevel + 1).ToString("00");

        StartCoroutine(UpdatePlayerPanels(0.6f, true));
    }

    void Update()
    {
        if (SessionData.Instance.CurrentMenuScreen != SessionData.MenuScreens.EditLevel)
        {
            SessionData.Instance.CurrentMenuScreen = SessionData.MenuScreens.EditLevel;
        }

        ContinueAnimator.SetBool("GameCanStart", currentEditableItem == this.GetComponent<EditableGameData>().AllEditableData.Count);

        foreach (PlayerData player in SessionData.Instance.GetNonNullPlayers().Where(x => x.PanelData.PlayerLocked == true))
        {
            if (ReInput.players.GetPlayer(player.RewiredPlayerId).GetButtonDown("Enter") && currentEditableItem == this.GetComponent<EditableGameData>().AllEditableData.Count
                && !SessionData.Instance.InputBlocked)
            {
                overallController.GetComponent<GameData>().NumberRounds = this.GetComponent<EditableGameData>().AllEditableData[0][currentEditableValues[0]].Second;
                overallController.GetComponent<GameData>().TimeLimit = this.GetComponent<EditableGameData>().AllEditableData[1][currentEditableValues[1]].Second;
                overallController.GetComponent<GameData>().ScoreLimit = this.GetComponent<EditableGameData>().AllEditableData[2][currentEditableValues[2]].Second;
                overallController.GetComponent<GameData>().SetSpeed(this.GetComponent<EditableGameData>().AllEditableData[3][currentEditableValues[3]].Second);

                overallController.GetComponent<LevelLoader>().LoadLevel(SessionData.Instance.GameplayLevels[SessionData.Instance.CurrentLevel]);
            }
            else if (Mathf.Abs(ReInput.players.GetPlayer(player.GamePlayerId).GetAxis("Vertical Menu")) > 0.5f && canUpdateEditable)
            {
                StartCoroutine(WaitForSelectedEditableUpdate(ReInput.players.GetPlayer(player.GamePlayerId)));
            }
            else if (Mathf.Abs(ReInput.players.GetPlayer(player.GamePlayerId).GetAxis("Horizontal Menu")) > 0.5f && canUpdateEditable)
            {
                StartCoroutine(WaitForEditValuegUpdate(ReInput.players.GetPlayer(player.GamePlayerId)));
            }
            else if (ReInput.players.GetPlayer(player.GamePlayerId).GetButtonDown("Back") && !SessionData.Instance.InputBlocked)
            {
                ContinueAnimator.SetBool("GameCanStart", false);
                EditLevelAnimator.SetBool("isOnEditLevelScreen", false);
                LevelSelectAnimator.SetBool("isOnLevelSelectScreen", true);
                StartCoroutine(UpdatePlayerPanels(0.35f, false));
                StartCoroutine(DisableInput());

                this.GetComponent<LevelSelectContoller>().enabled = true;
                this.GetComponent<EditLevelController>().enabled = false;
            }
        }
    }

    private IEnumerator WaitForSelectedEditableUpdate(Player currentPlayer)
    {
        canUpdateEditable = false;
        UpdateSelectedEditable(currentPlayer.GetAxis("Vertical Menu"));
        yield return new WaitForSeconds(0.3f);
        canUpdateEditable = true;
    }

    private void UpdateSelectedEditable(float change)
    {
        if (change > 0)
        {
            change = -1;
        }
        else if (change < 0)
        {
            change = 1;
        }

        HighLightEditableItem(false, AllEditableValues[currentEditableItem]);
        currentEditableItem += (int)change;

        if (currentEditableItem < 0)
        {
            currentEditableItem = AllEditableValues.Length - 1;
        }
        else if (currentEditableItem == AllEditableValues.Length)
        {
            currentEditableItem = 0;
        }
        HighLightEditableItem(true, AllEditableValues[currentEditableItem]);
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

    private IEnumerator WaitForEditValuegUpdate(Player currentPlayer)
    {
        canUpdateEditable = false;
        UpdateEditableValue(currentPlayer.GetAxis("Horizontal Menu"));
        yield return new WaitForSeconds(0.3f);
        canUpdateEditable = true;
    }

    private void UpdateEditableValue(float change)
    {
        if (change > 0)
        {
            change = 1;
        }
        else if (change < 0)
        {
            change = -1;
        }

        currentEditableValues[currentEditableItem] += (int)change;

        if (currentEditableItem != this.GetComponent<EditableGameData>().AllEditableData.Count)
        {
            if (currentEditableValues[currentEditableItem] < 0)
            {
                currentEditableValues[currentEditableItem] = this.GetComponent<EditableGameData>().AllEditableData[currentEditableItem].Count - 1;
            }
            else if (currentEditableValues[currentEditableItem] == AllEditableValues.Length)
            {
                currentEditableValues[currentEditableItem] = 0;
            }

            AllEditableValues[currentEditableItem].GetComponentsInChildren<Text>()[1].text =
                this.GetComponent<EditableGameData>().AllEditableData[currentEditableItem][currentEditableValues[currentEditableItem]].First;
        }
    }

    IEnumerator DisableInput()
    {
        SessionData.Instance.InputBlocked = true;
        yield return new WaitForSeconds(3);
        SessionData.Instance.InputBlocked = false;
    }

    IEnumerator UpdatePlayerPanels(float waitTime, bool showPlayers)
    {
        yield return new WaitForSeconds(waitTime);
        for (int i = 0; i < SessionData.Instance.GetNonNullPlayers().Length; i++)
        {
            AllPlayersPaddles[i].SetActive(showPlayers);
            AllPlayersPaddles[i].GetComponent<PaddleBeam>().SetColor(SessionData.Instance.GetNonNullPlayers()[i].PlayerColor);
        }
        yield return new WaitForSeconds(0.08f);
        for (int i = 0; i < SessionData.Instance.GetNonNullPlayers().Length; i++)
        {
            AllPlayersPaddles[i].transform.GetChild(1).gameObject.SetActive(showPlayers);
        }
    }
}
