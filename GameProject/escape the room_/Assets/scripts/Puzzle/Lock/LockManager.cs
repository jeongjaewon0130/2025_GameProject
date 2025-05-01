using UnityEngine;

public class LockManager : MonoBehaviour
{
    public DialButtonHandler[] dials;
    public int[] correctCode = { 2, 0, 1, 5 };
    public Animator doorAnimator;

    public GameObject dialUIPanel; // 다이얼 UI 패널 (닫기용)
    public DialUIController uiController; // UI 열기 방지 + 프롬프트 숨기기

    private bool isUnlocked = false;

    public void CheckCombination()
    {
        if (isUnlocked) return;

        for (int i = 0; i < dials.Length; i++)
        {
            if (dials[i].GetCurrentNumber() != correctCode[i])
            {
                ResetAllDials(); // 실패시 리셋
                return;
            }
        }

        // ✅ 성공!
        isUnlocked = true;

        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open");
        }

        // 다이얼 잠금
        foreach (var dial in dials)
        {
            dial.LockInput(true);
        }

        // UI 닫기
        if (dialUIPanel != null)
            dialUIPanel.SetActive(false);

        // UI 열기 차단 + 프롬프트 숨김
        if (uiController != null)
        {
            uiController.LockInput(true);      // F키 비활성화
            uiController.SetUnlocked(true);    // 프롬프트 비활성화
        }
    }

    void ResetAllDials()
    {
        foreach (var dial in dials)
        {
            dial.ResetToZero();
        }
    }
}
