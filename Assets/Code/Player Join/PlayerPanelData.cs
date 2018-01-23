using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class PlayerPanelData : MonoBehaviour
{
    public Text PlayerName;
    public PaddleBeam PaddleBeam;

    public int PlayerId, ColorNumber;

    private Animator animator;
    private Player currentPlayer;

    private bool playerJoined, playerLocked, canUpdateColor;

    private void Start()
    {
        playerJoined = false;
        playerLocked = false;
        canUpdateColor = true;

        PlayerName.text = "EMPTY";
        animator = GetComponent<Animator>();
        animator.SetInteger("Highlight", PlayerId);

        PaddleBeam.SetColor(0);
    }

    private void Update()
    {
        if (GameData.k_Players.Count > PlayerId && playerJoined == false)
        {
            playerJoined = true;
            animator.SetBool("PlayerJoined", true);
            currentPlayer = ReInput.players.GetPlayer(GameData.k_Players[PlayerId].RewiredPlayerId);
            GameData.k_Players[PlayerId].PanelData = this;
        }
        else if (playerJoined == true && playerLocked == false && animator.GetCurrentAnimatorStateInfo(0).IsName("Waiting Joined"))
        {
            if (currentPlayer.GetAxis("Horizontal Menu") != 0 && canUpdateColor)
            {
                StartCoroutine(WaitForColorUpdate());
            }

            if (currentPlayer.GetButtonDown("Enter"))
            {
                LockPlayer();
            }
        }
        else if (playerJoined == true && playerLocked == true && animator.GetCurrentAnimatorStateInfo(0).IsName("Waiting Locked"))
        {
            if (currentPlayer.GetButtonDown("Back"))
            {
                UnlockPlayer();
            }
        }
    }

    public void ChangeNameDisplayText()
    {
        PlayerName.text = "PLAYER 0" + (PlayerId + 1);
    }

    private IEnumerator WaitForColorUpdate()
    {
        canUpdateColor = false;
        UpdateColors(currentPlayer.GetAxis("Horizontal Menu"));
        yield return new WaitForSeconds(0.3f);
        canUpdateColor = true;
    }

    public void UpdateColors(float change)
    {
        if (change > 0)
        {
            change = 1;
        }
        else if (change < 0)
        {
            change = -1;
        }

        bool colorIsValid = true;
        UpdateCurrentColor((int)change);

        foreach (PlayerData player in GameData.k_Players)
        {
            if (player.PlayerColor == ColorNumber)
            {
                colorIsValid = false;
            }
        }

        while (colorIsValid == false)
        {
            UpdateCurrentColor((int)change);
            colorIsValid = true;

            foreach (PlayerData player in GameData.k_Players)
            {
                if (player.PlayerColor == ColorNumber)
                {
                    colorIsValid = false;
                }
            }

            if (change == 0)
            {
                change = 1;
            }
        }

        PaddleBeam.SetColor(ColorNumber);
    }

    private void UpdateCurrentColor(int change)
    {
        ColorNumber += change;

        if (ColorNumber < 0)
        {
            ColorNumber = 4;
        }
        else if (ColorNumber > 4)
        {
            ColorNumber = 0;
        }
    }

    private void LockPlayer()
    {
        playerLocked = true;
        animator.SetBool("PlayerLockedIn", true);
        GameData.k_Players[PlayerId].PlayerColor = ColorNumber;

        foreach (PlayerData player in GameData.k_Players)
        {
            if (player != GameData.k_Players[PlayerId] && !player.PanelData.playerLocked)
            {
                player.PanelData.UpdateColors(0);
            }
        }
    }

    private void UnlockPlayer()
    {
        playerLocked = false;
        animator.SetBool("PlayerLockedIn", false);
        GameData.k_Players[PlayerId].PlayerColor = -1;
    }
}
