using System.Collections.Generic;
using UnityEngine;

public class BtnPuzzleManager : MonoBehaviour
{
    public GameObject doorToOpen;
    public List<PuzzleButton> buttons = new List<PuzzleButton>();
    public HintData hintData;

    private List<int> correctOrder = new List<int>();
    private List<PuzzleButton> pressedButtons = new List<PuzzleButton>();

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
        // 이미 눌린 버튼이면 무시
        if (button.IsActivated()) return;

        // 무조건 라이트 켜기 (Activate)
        button.Activate();
        pressedButtons.Add(button);

        Debug.Log("버튼 눌림: " + button.buttonOrder);

        if (pressedButtons.Count == correctOrder.Count)
        {
            // 모든 버튼 눌림 -> 정답 체크
            bool isCorrect = true;
            for (int i = 0; i < correctOrder.Count; i++)
            {
                if (pressedButtons[i].buttonOrder != correctOrder[i])
                {
                    isCorrect = false;
                    break;
                }
            }

            if (isCorrect)
            {
                Debug.Log("퍼즐 성공! 문이 열립니다.");
                if (doorToOpen != null)
                    doorToOpen.GetComponent<Animator>().SetTrigger("Open");
                // 버튼 라이트는 켜진 상태 유지
            }
            else
            {
                Debug.Log("퍼즐 실패! 초기화합니다.");
                ResetPuzzle();
            }
        }
    }

    private void ResetPuzzle()
    {
        pressedButtons.Clear();

        foreach (var button in buttons)
        {
            button.ResetButton();
        }
    }
}
