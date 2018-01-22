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
    private bool playerJoined, playerLocked, canUpdateColor;

    private void Start()
    {
        playerJoined = false;
        playerLocked = false;
        canUpdateColor = true;

        PlayerName.text = "EMPTY";
        animator = GetComponent<Animator>();
        UpdateColors(0);
    }

    private void Update()
    {
        if (GameData.k_Players.Count > PlayerId && playerJoined == false)
        {
            playerJoined = true;
            animator.SetBool("PlayerJoined", true);
            player = ReInput.players.GetPlayer(GameData.k_Players[PlayerId].rewiredPlayerId);
        }
        else if (playerJoined == true && playerLocked == false && animator.GetCurrentAnimatorStateInfo(0).IsName("Waiting Joined"))
        {
            if (player.GetButtonDown("Enter"))
            {
                playerLocked = true;
                animator.SetBool("PlayerLockedIn", true);
            }

            if (player.GetAxis("Horizontal Menu") != 0 && canUpdateColor)
            {
                StartCoroutine(WaitForColorUpdate());
            }
        }
        else if (playerJoined == true && playerLocked == true && animator.GetCurrentAnimatorStateInfo(0).IsName("Waiting Locked"))
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

    private IEnumerator WaitForColorUpdate()
    {
        canUpdateColor = false;
        UpdateColors(player.GetAxis("Horizontal Menu"));
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

        ColorNumber += (int)change;

        if (ColorNumber < 0)
        {
            ColorNumber = 4;
        }
        else if (ColorNumber > 4)
        {
            ColorNumber = 0;
        }

        PaddleBeam.SetColor(ColorNumber);
    }
}
