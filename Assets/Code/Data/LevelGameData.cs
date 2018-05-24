using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelGameData : ScriptableObject
{
    public int NumberRoundsIndex;
    public int TimeLimitIndex;
    public int ScoreLimitIndex;
    public int NumberLivesIndex;
    public int SpeedIndex;

    public int NumberRounds
    {
        get
        {
            return NumberRoundsOptions[NumberRoundsIndex];
        }
        set
        {
            if (NumberRoundsOptions.Count - 1 > value)
            {
                NumberRoundsIndex = value;
            }
            else if (NumberRoundsOptions.Count < 0)
            {
                NumberRoundsIndex = NumberRoundsOptions.Count - 1;
            }
            else
            {
                NumberRoundsIndex = value;
            }
        }
    }

    public int TimeLimit
    {
        get
        {
            return TimeLimitOptions[TimeLimitIndex];
        }
        set
        {
            if (TimeLimitOptions.Count - 1 > value)
            {
                TimeLimitIndex = value;
            }
            else if (TimeLimitOptions.Count < 0)
            {
                TimeLimitIndex = TimeLimitOptions.Count - 1;
            }
            else
            {
                TimeLimitIndex = value;
            }
        }
    }

    public int ScoreLimit
    {
        get
        {
            return ScoreLimitOptions[ScoreLimitIndex];
        }
        set
        {
            if (ScoreLimitOptions.Count - 1 > value)
            {
                ScoreLimitIndex = value;
            }
            else if (ScoreLimitOptions.Count < 0)
            {
                ScoreLimitIndex = ScoreLimitOptions.Count - 1;
            }
            else
            {
                ScoreLimitIndex = value;
            }
        }
    }

    public int NumberLives
    {
        get
        {
            return NumberLivesOptions[NumberLivesIndex];
        }
        set
        {
            if (NumberLivesOptions.Count - 1 > value)
            {
                NumberLivesIndex = value;
            }
            else if (NumberLivesOptions.Count < 0)
            {
                NumberLivesIndex = NumberLivesOptions.Count - 1;
            }
            else
            {
                NumberLivesIndex = value;
            }
        }
    }

    public float Speed
    {
        get
        {
            return SpeedOptions[SpeedIndex];
        }
        set
        {
            if (SpeedOptions.Count - 1 > value)
            {
                SpeedIndex = (int)value;
            }
            else if (ScoreLimitOptions.Count < 0)
            {
                SpeedIndex = SpeedOptions.Count - 1;
            }
            else
            {
                SpeedIndex = (int)value;
            }
        }
    }

    public List<int> NumberRoundsOptions;
    public List<int> TimeLimitOptions;
    public List<int> ScoreLimitOptions;
    public List<int> NumberLivesOptions;
    public List<float> SpeedOptions;

}
