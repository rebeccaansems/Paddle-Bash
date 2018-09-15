using Rewired;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EditLevelController : MonoBehaviour
{
    public GameObject[] AllEditableValues;
    public EditableGameData EditableData;

    private GameObject overallController;
    private int[] currentEditableValues;
    private int currentEditableItem;
    private bool canUpdateEditable;

    private void Start()
    {
        overallController.GetComponent<GameData>().SetToDefaults();
        canUpdateEditable = true;
        currentEditableValues = new int[] { 2, 2, 2, 4, 2 };

        overallController = GameObject.FindGameObjectWithTag("Overall Controller");
        HighLightEditableItem(true, AllEditableValues[currentEditableItem]);
    }

    void Update()
    {
        if (SessionData.k_CurrentMenuScreen != SessionData.MenuScreens.EditLevel)
        {
            SessionData.k_CurrentMenuScreen = SessionData.MenuScreens.EditLevel;
        }

        foreach (PlayerData player in SessionData.GetNonNullPlayers().Where(x => x.PanelData.PlayerLocked == true))
        {
            if (ReInput.players.GetPlayer(player.RewiredPlayerId).GetButtonDown("Enter") && !SessionData.k_InputBlocked)
            {
                overallController.GetComponent<GameData>().NumberRounds = EditableData.AllEditableData[0][currentEditableValues[0]].Second;
                overallController.GetComponent<GameData>().TimeLimit = EditableData.AllEditableData[1][currentEditableValues[1]].Second;
                overallController.GetComponent<GameData>().ScoreLimit = EditableData.AllEditableData[2][currentEditableValues[2]].Second;
                overallController.GetComponent<GameData>().NumberLives = EditableData.AllEditableData[3][currentEditableValues[3]].Second;
                overallController.GetComponent<GameData>().SetSpeed(EditableData.AllEditableData[4][currentEditableValues[4]].Second);
            }
            else if (Mathf.Abs(ReInput.players.GetPlayer(player.GamePlayerId).GetAxis("Vertical Menu")) > 0.5f && canUpdateEditable)
            {
                StartCoroutine(WaitForSelectedEditableUpdate(ReInput.players.GetPlayer(player.GamePlayerId)));
            }
            else if (Mathf.Abs(ReInput.players.GetPlayer(player.GamePlayerId).GetAxis("Horizontal Menu")) > 0.5f && canUpdateEditable)
            {
                StartCoroutine(WaitForEditValuegUpdate(ReInput.players.GetPlayer(player.GamePlayerId)));
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

        if (currentEditableValues[currentEditableItem] < 0)
        {
            currentEditableValues[currentEditableItem] = EditableData.AllEditableData[currentEditableItem].Count - 1;
        }
        else if (currentEditableValues[currentEditableItem] == AllEditableValues.Length)
        {
            currentEditableValues[currentEditableItem] = 0;
        }

        AllEditableValues[currentEditableItem].GetComponentsInChildren<Text>()[1].text =
            EditableData.AllEditableData[currentEditableItem][currentEditableValues[currentEditableItem]].First;
    }

}
