using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelData : MonoBehaviour
{

    public Text PlayerName;
    public PaddleBeam PaddleBeam;
    public int PlayerId, ColorNumber;

    private int prevColorNumber = 0;

    private void Start()
    {
        PlayerName.text = "EMPTY";
    }

    private void Update()
    {
        if (prevColorNumber != ColorNumber)
        {
            prevColorNumber = ColorNumber;
            UpdateColors();
        }
    }

    public void ChangeNameDisplayText()
    {
        PlayerName.text = "PLAYER 0" + PlayerId + 1;
    }

    public void UpdateColors()
    {
        if (PaddleBeam != null)
        {
            PaddleBeam.SetColor(ColorNumber);
        }
    }
}
