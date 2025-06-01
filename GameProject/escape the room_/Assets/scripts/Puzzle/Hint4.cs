using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hint4 : MonoBehaviour
{
    public PzleHint4SO hintData;
    public TMP_Text titleText;
    public TMP_Text bodyText;

    void Start()
    {
        titleText.text = hintData.hintTitle;
        bodyText.text = string.Join("\n", hintData.hintLines);
    }
}
