using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public int buttonOrder; // �� ��ư�� ���� (0~7)
    public BtnPuzzleManager puzzleManager; // ���� �Ŵ��� ����

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
