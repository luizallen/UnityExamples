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

    [MenuItem("Assets/Create/ScriptableObjects/ModifierCondition/JobCondition")]
    public static void CreateModifierJobCondition()
    {
        var asset = ScriptableObject.CreateInstance<ModConditionJob>();

        AssetDatabase.CreateAsset(asset, "Assets/Prefabs/Modifiers/NewScriptableObject.asset");
        AssetDatabase.SaveAssets();

        Selection.activeObject = asset;
    }

    [MenuItem("Assets/Create/ScriptableObjects/ModifierCondition/ElementalCondition")]
    public static void CreateModifierElementalCondition()
    {
        var asset = ScriptableObject.CreateInstance<ModConditionElement>();

        AssetDatabase.CreateAsset(asset, "Assets/Prefabs/Modifiers/NewScriptableObject.asset");
        AssetDatabase.SaveAssets();

        Selection.activeObject = asset;
    }
}
