using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelData : MonoBehaviour
{

    public Text PlayerName;
    public int PlayerId, ColorNumber;

    private void Start()
    {
        PlayerName.text = "EMPTY";
    }

    public void ChangeNameDisplayText()
    {
        PlayerName.text = "PLAYER 0" + PlayerId + 1;
    }
}
