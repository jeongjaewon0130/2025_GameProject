using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnPuzzleManager : MonoBehaviour
{
    public GameObject doorToOpen;
    public List<PuzzleButton> buttons = new List<PuzzleButton>();

    private int currentOrder = 0;

    public void ButtonPressed(PuzzleButton button)
    {
        if (button.buttonOrder == currentOrder)
        {
            button.Activate();
            Debug.Log("�ùٸ� ��ư�� �������ϴ�: " + button.buttonOrder);
            currentOrder++;

            if (currentOrder >= buttons.Count)
            {
                Debug.Log("���� �Ϸ�! ���� �����ϴ�.");
                doorToOpen.GetComponent<Animator>().SetTrigger("Open");
            }
        }
        else
        {
            Debug.Log("Ʋ�Ƚ��ϴ�! ������ �ʱ�ȭ�˴ϴ�.");
            ResetPuzzle();
        }
    }

    private void ResetPuzzle()
    {
        currentOrder = 0;
        foreach (var button in buttons)
        {
            button.ResetButton();
        }
    }
}
