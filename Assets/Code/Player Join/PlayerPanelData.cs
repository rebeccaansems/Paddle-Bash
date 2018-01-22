using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelData : MonoBehaviour
{
    public Text PlayerName;
    public PaddleBeam PaddleBeam;

    public int PlayerId, ColorNumber;

    private Animator animator;

    private int prevColorNumber = 0;

    private void Start()
    {
        PlayerName.text = "EMPTY";
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameData.k_Players.Count > PlayerId && animator.GetBool("PlayerJoined") == false)
        {
            animator.SetBool("PlayerJoined", true);
        }
        else if (GameData.k_Players.Count > PlayerId)
        {
            if (prevColorNumber != ColorNumber)
            {
                prevColorNumber = ColorNumber;
                UpdateColors();
            }
        }
    }

    public void ChangeNameDisplayText()
    {
        PlayerName.text = "PLAYER 0" + (PlayerId + 1);
    }

    public void UpdateColors()
    {
        if (PaddleBeam != null)
        {
            PaddleBeam.SetColor(ColorNumber);
        }
    }
}
