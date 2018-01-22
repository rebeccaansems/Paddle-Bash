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
    private Player player;

    private int prevColorNumber = 0;
    private bool playerJoined, playerLocked;

    private void Start()
    {
        playerJoined = false;
        playerLocked = false;

        PlayerName.text = "EMPTY";
        animator = GetComponent<Animator>();
        UpdateColors();
    }

    private void Update()
    {
        if (GameData.k_Players.Count > PlayerId && playerJoined == false)
        {
            playerJoined = true;
            animator.SetBool("PlayerJoined", true);
            player = ReInput.players.GetPlayer(GameData.k_Players[PlayerId].rewiredPlayerId);
        }

        if (playerJoined == true && playerLocked == false && animator.GetCurrentAnimatorStateInfo(0).IsName("Waiting Joined"))
        {
            if (player.GetButtonDown("Enter"))
            {
                playerLocked = true;
                animator.SetBool("PlayerLockedIn", true);
            }

            if (prevColorNumber != ColorNumber)
            {
                prevColorNumber = ColorNumber;
                UpdateColors();
            }
        }

        if (playerJoined == true && playerLocked == true && animator.GetCurrentAnimatorStateInfo(0).IsName("Waiting Locked"))
        {
            if (player.GetButtonDown("Back"))
            {
                playerLocked = false;
                animator.SetBool("PlayerLockedIn", false);
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
