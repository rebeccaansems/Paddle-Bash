#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Linq;

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
        ScreenCapture.CaptureScreenshot(Application.productName + "-" + DateTime.Now.ToString("hhmmss") + ".png");
        Debug.Log("CLICK: " + Application.productName + "-" + DateTime.Now.ToString("hhmmss") + ".png");
    }

    [MenuItem("Tools/Create Scene Data Objects")]
    public static void CreateAsset()
    {
        int count = Directory.GetFiles("Assets/Scene/Data",
            "*.asset", SearchOption.AllDirectories).Length + 1;

        LevelData asset = ScriptableObject.CreateInstance<LevelData>();
        string assetPath = "Assets/Scene/Data/LevelData.asset";

        AssetDatabase.CreateAsset(asset, assetPath);
        AssetDatabase.SaveAssets();
        
        AssetDatabase.RenameAsset(assetPath, "LevelData" + count + ".asset");
        AssetDatabase.SaveAssets();

        Debug.Log("ASSET " + count + " CREATED SUCCESSFUL");
    }
}
#endif
