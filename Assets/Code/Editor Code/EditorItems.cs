#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;

public class EditorItems
{
    [MenuItem("Tools/Clear PlayerPrefs")]
    private static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("Tools/In Game/Screenshot")]
    private static void Screenshot()
    {
        ScreenCapture.CaptureScreenshot(Application.productName + "-" + DateTime.Now.ToString("hhmmss")+".png");
        Debug.Log("CLICK: " + Application.productName + "-" + DateTime.Now.ToString("hhmmss") + ".png");
    }

    [MenuItem("Tools/In Game/Create Scene Data Objects")]
    public static void CreateAsset()
    {
        LevelData asset = ScriptableObject.CreateInstance<LevelData>();

        //AssetDatabase.CreateAsset(asset, "Scene/Data/LevelData.asset");
        //AssetDatabase.SaveAssets();

        //EditorUtility.FocusProjectWindow();

        //Selection.activeObject = asset;
    }
}
#endif
