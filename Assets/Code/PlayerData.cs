using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    public Text ScoreText, ResetText;

    public static int[] k_CurrentScores, k_BallResets;

    public void Start()
    {
        if (k_CurrentScores == null)
        {
            k_CurrentScores = new int[3];
            k_BallResets = new int[] { 3, 3, 3 };
        }

        ScoreText.text = k_CurrentScores[0] + " | " + k_CurrentScores[1] + " | " + k_CurrentScores[2];
    }

    public void Update()
    {
        ResetText.text = k_BallResets[0] + " | " + k_BallResets[1] + " | " + k_BallResets[2];
    }
}
