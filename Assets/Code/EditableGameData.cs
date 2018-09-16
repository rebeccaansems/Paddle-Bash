using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditableGameData : MonoBehaviour
{
    public List<List<Tuple<string, int>>> AllEditableData;

    private List<Tuple<string, int>> AllValuesRounds;
    private List<Tuple<string, int>> AllValuesTimeLimit;
    private List<Tuple<string, int>> AllValuesScoreLimit;
    private List<Tuple<string, int>> AllValuesSpeed;

    public void Start()
    {
        AllEditableData = new List<List<Tuple<string, int>>>();
        AllValuesRounds = new List<Tuple<string, int>>();
        AllValuesTimeLimit = new List<Tuple<string, int>>();
        AllValuesScoreLimit = new List<Tuple<string, int>>();
        AllValuesSpeed = new List<Tuple<string, int>>();

        AllEditableData.Add(AllValuesRounds);
        AllEditableData.Add(AllValuesTimeLimit);
        AllEditableData.Add(AllValuesScoreLimit);
        AllEditableData.Add(AllValuesSpeed);

        AllValuesRounds.AddRange(new List<Tuple<string, int>>
        {
            new Tuple<string, int>("1", 1),
            new Tuple<string, int>("3", 3),
            new Tuple<string, int>("5", 5),
            new Tuple<string, int>("10", 10),
            new Tuple<string, int>("UNLIMITED", -1),
        });

        AllValuesTimeLimit.AddRange(new List<Tuple<string, int>>
        {
            new Tuple<string, int>("30s", 30),
            new Tuple<string, int>("60s", 60),
            new Tuple<string, int>("180s", 180),
            new Tuple<string, int>("300s", 300),
            new Tuple<string, int>("UNLIMITED", -1),
        });

        AllValuesScoreLimit.AddRange(new List<Tuple<string, int>>
        {
            new Tuple<string, int>("1", 1),
            new Tuple<string, int>("3", 3),
            new Tuple<string, int>("5", 5),
            new Tuple<string, int>("10", 10),
            new Tuple<string, int>("UNLIMITED", -1),
        });

        AllValuesSpeed.AddRange(new List<Tuple<string, int>>
        {
            new Tuple<string, int>("0.25x", 0),
            new Tuple<string, int>("0.5x", 1),
            new Tuple<string, int>("1x", 2),
            new Tuple<string, int>("1.5x", 3),
            new Tuple<string, int>("2x", 4),
        });
    }
}
