using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class HintJsonToSOConverter : MonoBehaviour
{
    [System.Serializable]
    public class HintEntry
    {
        public string hintTitle;
        public string eventText;
    }

    [System.Serializable]
    public class HintEntryArray
    {
        public HintEntry[] entries;
    }

#if UNITY_EDITOR
    [MenuItem("Tools/Convert Hint JSON to SO")]
    public static void Convert()
    {
        string jsonPath = Application.dataPath + "/Resources/Hints/Hint4.json";
        if (!File.Exists(jsonPath))
        {
            Debug.LogError("Hint4.json 파일이 존재하지 않습니다.");
            return;
        }

        string jsonText = File.ReadAllText(jsonPath);
        string wrappedJson = "{\"entries\":" + jsonText + "}";
        HintEntryArray data = JsonUtility.FromJson<HintEntryArray>(wrappedJson);

        if (data.entries.Length == 0)
        {
            Debug.LogWarning("힌트 데이터가 비어 있습니다.");
            return;
        }

        PzleHint4SO hintSO = ScriptableObject.CreateInstance<PzleHint4SO>();
        hintSO.hintTitle = data.entries[0].hintTitle;
        hintSO.hintLines = new List<string>();

        foreach (var entry in data.entries)
        {
            hintSO.hintLines.Add(entry.eventText);
        }

        string savePath = "Assets/Data/Hints/NewHint.asset";
        AssetDatabase.CreateAsset(hintSO, savePath);
        AssetDatabase.SaveAssets();
        Debug.Log("힌트 ScriptableObject 생성 완료: " + savePath);
    }
#endif
}
