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
        string assetPath = "Assets/Resources/LevelData";
        string fileName = "/LevelData.asset";

        int count = Directory.GetFiles(assetPath,
            "*.asset", SearchOption.AllDirectories).Length + 1;

        LevelData asset = ScriptableObject.CreateInstance<LevelData>();

        AssetDatabase.CreateAsset(asset, assetPath + fileName);
        AssetDatabase.RenameAsset(assetPath + fileName, "LevelData" + count + ".asset");
        AssetDatabase.SaveAssets();

        Debug.Log("ASSET " + count + " CREATED SUCCESSFUL");
    }
}
#endif
