using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using System.Linq;

public class PlayerPanelData : MonoBehaviour
{
    public Text PlayerName;
    public PaddleBeam PaddleBeam;

    public int PlayerId, ColorNumber;
    public bool PlayerLocked;

    private Animator animator;
    private Player currentPlayer;

    private bool playerJoined, canUpdateColor;

    private void Start()
    {
        playerJoined = false;
        PlayerLocked = false;
        canUpdateColor = true;

        PlayerName.text = "EMPTY";
        animator = GetComponent<Animator>();
        animator.SetInteger("Highlight", PlayerId);

        ColorNumber = PlayerId;
    }

    private void Update()
    {
        if (GameData.k_Players[PlayerId] != null && playerJoined == false)
        {
            playerJoined = true;
            animator.SetBool("PlayerJoined", true);
            currentPlayer = ReInput.players.GetPlayer(GameData.k_Players[PlayerId].RewiredPlayerId);
            GameData.k_Players[PlayerId].PanelData = this;
            UpdateColors(1);
        }
        else if (playerJoined == true && PlayerLocked == false && animator.GetCurrentAnimatorStateInfo(0).IsName("Waiting Joined"))
        {
            if (currentPlayer.GetAxis("Horizontal Menu") != 0 && canUpdateColor)
            {
                StartCoroutine(WaitForColorUpdate());
            }

            if (currentPlayer.GetButtonDown("Enter"))
            {
                LockPlayer();
            }

            if (currentPlayer.GetButtonDown("Back"))
            {
                UnjoinedPlayer();
            }
        }
        else if (playerJoined == true && PlayerLocked == true && animator.GetCurrentAnimatorStateInfo(0).IsName("Waiting Locked"))
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

    public void ResetPlayerDisplayText()
    {
        PlayerName.text = "EMPTY";
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

        UpdateColorNumber((int)change);

        bool colorIsValid = false;
        while (colorIsValid == false)
        {
            colorIsValid = true;

            foreach (PlayerData player in GameData.GetNonNullPlayers())
            {
                if (player.PlayerColor == ColorNumber && player.PanelData.PlayerId != PlayerId)
                {
                    colorIsValid = false;
                }
            }

            if (colorIsValid == false)
            {
                UpdateColorNumber((int)change);
            }
        }

        PaddleBeam.SetColor(ColorNumber);
    }

    private void LockPlayer()
    {
        PlayerLocked = true;
        animator.SetBool("PlayerLockedIn", true);
        GameData.k_Players[PlayerId].PlayerColor = ColorNumber;

        foreach (PlayerData player in GameData.GetNonNullPlayers())
        {
            if (player.PanelData.PlayerId != PlayerId && !player.PanelData.PlayerLocked && player.PanelData.ColorNumber == ColorNumber)
            {
                player.PanelData.UpdateColors(1);
            }
        }
    }

    private void UnlockPlayer()
    {
        PlayerLocked = false;
        animator.SetBool("PlayerLockedIn", false);
        GameData.k_Players[PlayerId].PlayerColor = -1;
    }

    private void UnjoinedPlayer()
    {
        animator.SetBool("PlayerJoined", false);

        GameData.k_RawRewiredPlayerIds.Remove(GameData.k_Players[PlayerId].RewiredPlayerId);
        GameData.k_Players[PlayerId] = null;

        playerJoined = false;
        PlayerLocked = false;
        canUpdateColor = true;
    }

    private void UpdateColorNumber(int change)
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
}
