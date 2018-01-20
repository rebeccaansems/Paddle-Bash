using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour
{
    public Text ScoreText;

    public static int[] k_CurrentScores;

    public void Start()
    {
        if (k_CurrentScores == null)
        {
            k_CurrentScores = new int[3];
        }

        ScoreText.text = k_CurrentScores[0] + " | " + k_CurrentScores[1] + " | " + k_CurrentScores[2];
    }
}
