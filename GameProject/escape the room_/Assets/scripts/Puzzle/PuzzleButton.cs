using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public int buttonOrder; // 이 버튼의 순서 (0~7)
    public BtnPuzzleManager puzzleManager; // 퍼즐 매니저 연결

    private bool isActivated = false;

    public void OnButtonPressed()
    {
        if (isActivated) return;

        puzzleManager.ButtonPressed(this);
    }

    public void Activate()
    {
        isActivated = true;
    }

    public void ResetButton()
    {
        isActivated = false;
    }
}
