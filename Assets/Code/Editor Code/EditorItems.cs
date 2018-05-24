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

    [MenuItem("Tools/Script Object/Level Data Objects")]
    public static void CreateSceneDataAsset()
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

    [MenuItem("Tools/Script Object/Level Game Data Objects")]
    public static void CreateLevelGameDataAsset()
    {
        string assetPath = "Assets/Resources";
        string fileName = "/GameData.asset";

        int count = Directory.GetFiles(assetPath,
            "*.asset", SearchOption.AllDirectories).Length + 1;

        LevelGameData asset = ScriptableObject.CreateInstance<LevelGameData>();

        AssetDatabase.CreateAsset(asset, assetPath + fileName);
        AssetDatabase.SaveAssets();

        Debug.Log("GAME DATA ASSET CREATED SUCCESSFUL");
    }
}
#endif
