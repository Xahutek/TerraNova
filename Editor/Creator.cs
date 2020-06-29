using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Creator : MonoBehaviour
{
    public class MakeResourceObject
    {
        [MenuItem("Assets/Scriptable Objects/CardResources/Create/New Resource")]
        public static void CreateCardResource()
        {
            CardResources asset = ScriptableObject.CreateInstance<CardResources>();

            AssetDatabase.CreateAsset(asset, "Assets/Scriptable Objects/CardResources/NewScripableObject.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }

        [MenuItem("Assets/Scriptable Objects/CardResources/Create/New Random Resource")]
        public static void CreateRandomCardResource()
        {
            RandomResource asset = ScriptableObject.CreateInstance<RandomResource>();

            AssetDatabase.CreateAsset(asset, "Assets/Scriptable Objects/CardResources/NewScripableObject.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }

        [MenuItem("Assets/Scriptable Objects/Cards/Create/New Base Card")]
        public static void CreateBaseCard()
        {
            BaseCard asset = ScriptableObject.CreateInstance<BaseCard>();

            AssetDatabase.CreateAsset(asset, "Assets/Scriptable Objects/Cards/NewScripableObject.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }

        [MenuItem("Assets/Scriptable Objects/Cards/Create/New Cypher Card")]
        public static void CreateCypherCard()
        {
            CypherCard asset = ScriptableObject.CreateInstance<CypherCard>();

            AssetDatabase.CreateAsset(asset, "Assets/Scriptable Objects/Cards/NewScripableObject.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }

        [MenuItem("Assets/Scriptable Objects/Cards/Create/New Chance Card")]
        public static void CreateChanceCard()
        {
            ChanceCard asset = ScriptableObject.CreateInstance<ChanceCard>();

            AssetDatabase.CreateAsset(asset, "Assets/Scriptable Objects/Cards/NewScripableObject.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }

        [MenuItem("Assets/Scriptable Objects/Cards/Create/New Strength Card")]
        public static void CreateStrengthCard()
        {
            StrengthCard asset = ScriptableObject.CreateInstance<StrengthCard>();

            AssetDatabase.CreateAsset(asset, "Assets/Scriptable Objects/Cards/NewScripableObject.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }

        [MenuItem("Assets/Scriptable Objects/Cards/Create/New Waystone Card")]
        public static void CreateWaystoneCard()
        {
            PilgrimsPathCard asset = ScriptableObject.CreateInstance<PilgrimsPathCard>();

            AssetDatabase.CreateAsset(asset, "Assets/Scriptable Objects/Cards/NewScripableObject.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
    }
}
