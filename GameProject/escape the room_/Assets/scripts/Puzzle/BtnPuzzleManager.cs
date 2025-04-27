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
            Debug.Log("올바른 버튼을 눌렀습니다: " + button.buttonOrder);
            currentOrder++;

            if (currentOrder >= buttons.Count)
            {
                Debug.Log("퍼즐 완료! 문이 열립니다.");
                doorToOpen.GetComponent<Animator>().SetTrigger("Open");
            }
        }
        else
        {
            Debug.Log("틀렸습니다! 퍼즐이 초기화됩니다.");
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
