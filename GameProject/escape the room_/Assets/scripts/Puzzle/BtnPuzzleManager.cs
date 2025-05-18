using System.Collections.Generic;
using UnityEngine;

public class BtnPuzzleManager : MonoBehaviour
{
    public GameObject doorToOpen;
    public List<PuzzleButton> buttons = new List<PuzzleButton>();
    public HintData hintData;

    private List<int> correctOrder = new List<int>();
    private int currentOrder = 0;

    void Start()
    {
        LoadCorrectOrderFromHintData();
    }

    void LoadCorrectOrderFromHintData()
    {
        if (hintData == null)
        {
            Debug.LogError("HintData가 할당되지 않았습니다.");
            return;
        }

        correctOrder.Clear();

        foreach (var entry in hintData.entries)
        {
            correctOrder.Add(entry.buttonIndex);
        }
    }

    public void ButtonPressed(PuzzleButton button)
    {
        // 이미 눌린 버튼이면 무시 (사운드 중복 방지)
        if (button.IsActivated()) return;

        if (button.buttonOrder == correctOrder[currentOrder])
        {
            button.Activate();
            Debug.Log("올바른 버튼을 눌렀습니다: " + button.buttonOrder);
            currentOrder++;

            if (currentOrder >= correctOrder.Count)
            {
                Debug.Log("퍼즐 완료! 문이 열립니다.");
                if (doorToOpen != null)
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
            button.ResetButton(); // ResetButton 안에서 사운드+라이트 처리
        }
    }
}
