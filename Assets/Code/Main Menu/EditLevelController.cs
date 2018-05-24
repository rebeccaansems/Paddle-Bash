using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Rewired;
using System.Linq;

public class EditLevelController : MonoBehaviour
{
    public LevelGameData LevelData;

    public GameObject[] AllEditableOptions;
    public int EditableIndex;

    private GameObject overallController;
    private GameObject currentEditable, prevEditable;
    private bool canAdjust = true;

    void Start()
    {
        overallController = GameObject.FindGameObjectWithTag("Overall Controller");

        prevEditable = AllEditableOptions[EditableIndex + 1];
        currentEditable = AllEditableOptions[EditableIndex];
    }

    void Update()
    {
        if (currentEditable != prevEditable)
        {
            UpdateSelected();
        }

        if (canAdjust)
        {
            foreach (PlayerData player in GameData.GetNonNullPlayers().Where(x => x.PanelData.PlayerLocked == true))
            {
                Vector2 input = ReInput.players.GetPlayer(player.GamePlayerId).GetAxis2D("Horizontal Menu", "Vertical Menu");
                if (input.y > 0f && Mathf.Abs(input.x) < 0.05f)
                {
                    AdjustEditable(-1);
                }
                else if (input.y < 0f && Mathf.Abs(input.x) < 0.05f)
                {
                    AdjustEditable(1);
                }
                else if (input.x > 0f && Mathf.Abs(input.y) < 0.05f)
                {
                    Debug.Log("Right");
                }
                else if (input.x < 0f && Mathf.Abs(input.y) < 0.05f)
                {
                    Debug.Log("Left");
                }
            }

            currentEditable = AllEditableOptions[EditableIndex];
        }

    }

    void AdjustEditable(int adjust)
    {
        EditableIndex += adjust;

        if (AllEditableOptions.Length - 1 < EditableIndex)
        {
            EditableIndex = 0;
        }
        else if (EditableIndex < 0)
        {
            EditableIndex = AllEditableOptions.Length - 1;
        }

        currentEditable = AllEditableOptions[EditableIndex];
    }

    void UpdateSelected()
    {
        Select(currentEditable);
        Unselect(prevEditable);

        prevEditable = currentEditable;

        StartCoroutine(WaitFor());
    }

    void Select(GameObject newEditable)
    {
        newEditable.GetComponentsInChildren<Text>()[0].fontStyle = FontStyle.Italic;
        newEditable.GetComponentsInChildren<Text>()[0].color = Color.red;

        newEditable.GetComponentsInChildren<Text>()[1].color = Color.red;

        newEditable.GetComponentsInChildren<Image>()[0].color = Color.red;
        newEditable.GetComponentsInChildren<Image>()[1].color = Color.red;
    }

    void Unselect(GameObject oldEditable)
    {
        oldEditable.GetComponentsInChildren<Text>()[0].fontStyle = FontStyle.Normal;
        oldEditable.GetComponentsInChildren<Text>()[0].color = Color.white;

        oldEditable.GetComponentsInChildren<Text>()[1].color = Color.white;

        oldEditable.GetComponentsInChildren<Image>()[0].color = Color.white;
        oldEditable.GetComponentsInChildren<Image>()[1].color = Color.white;
    }

    IEnumerator WaitFor()
    {
        canAdjust = false;
        yield return new WaitForSeconds(0f);
        canAdjust = true;
    }
}
