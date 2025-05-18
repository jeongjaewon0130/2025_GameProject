using UnityEngine;

[System.Serializable]
public class HintEntry
{
    public string hintTitle;
    public string time;
    public string eventText;
    public int buttonIndex;
}

[System.Serializable]
public class HintEntryArray
{
    public HintEntry[] entries;
}

[CreateAssetMenu(fileName = "HintData", menuName = "Puzzle/HintData")]
public class HintData : ScriptableObject
{
    public HintEntry[] entries;
}

