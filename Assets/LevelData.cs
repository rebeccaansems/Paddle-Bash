using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LevelData : ScriptableObject {

    public string LevelName;
    public int SceneNumber;
    public Scene Scene;
    public Color LevelColor;
    public AudioClip[] BackgroundMusic;

}
