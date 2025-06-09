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
            Debug.LogError("HintData�� �Ҵ���� �ʾҽ��ϴ�.");
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
        // �̹� ���� ��ư�̸� ����
        if (button.IsActivated()) return;

        // ������ ����Ʈ �ѱ� (Activate)
        button.Activate();
        pressedButtons.Add(button);

        Debug.Log("��ư ����: " + button.buttonOrder);

        if (pressedButtons.Count == correctOrder.Count)
        {
            // ��� ��ư ���� -> ���� üũ
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
                Debug.Log("���� ����! ���� �����ϴ�.");
                if (doorToOpen != null)
                    doorToOpen.GetComponent<Animator>().SetTrigger("Open");
                // ��ư ����Ʈ�� ���� ���� ����
            }
            else
            {
                Debug.Log("���� ����! �ʱ�ȭ�մϴ�.");
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
