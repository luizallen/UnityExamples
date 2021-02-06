using UnityEditor;
using UnityEngine;

public class AssetCreator
{
    [MenuItem("Assets/Create/ScriptableObjects/Job")]
    public static void CreateJob()
    {
        var asset = ScriptableObject.CreateInstance<Job>();

        AssetDatabase.CreateAsset(asset, "Assets/Prefabs/Jobs/NewScriptableObject.asset");
        AssetDatabase.SaveAssets();

        asset.InitStats();

        Selection.activeObject = asset;
    }
}
