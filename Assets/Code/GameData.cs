using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

    public int NumberRounds, TimeLimit, ScoreLimit;
    public float Speed;

    private List<float> allSpeeds = new List<float> { 0.25f, 0.5f, 1f, 1.5f, 2f };

    public void SetToDefaults()
    {
        NumberRounds = 5;
        TimeLimit = 180;
        ScoreLimit = 5;
        Speed = 1;
    }

    public void SetSpeed(int index)
    {
        Speed = allSpeeds[index];
    }
}
