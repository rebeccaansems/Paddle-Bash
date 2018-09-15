using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public int[] AllGameplayLevels;

    public void LoadLevel(string name)
    {
        LoadLevel(SceneManager.GetSceneByName(name).buildIndex);
    }

    public void LoadLevel(int sceneIndex)
    {
        SceneManager.LoadScene("Loading");
        StartCoroutine(LoadAsync(sceneIndex));
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync("Loading");
    }
}
