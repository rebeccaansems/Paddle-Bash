using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameController : MonoBehaviour
{
    public Text CurrentScore;

    void Start()
    {
        string currScore = "";
        foreach (PlayerData players in GameData.k_Players.Where(x => x != null))
        {
            currScore += players.Score + " | ";
        }
        CurrentScore.text = currScore.TrimEnd('|', ' ');
    }
}
