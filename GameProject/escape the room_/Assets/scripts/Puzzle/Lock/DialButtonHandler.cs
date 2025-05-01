using UnityEngine;
using TMPro;

public class DialButtonHandler : MonoBehaviour
{
    public TMP_Text dialText;              // 텍스트 표시
    public Animator dialAnimator;          // 다이얼 회전 애니메이션
    public LockManager lockManager;        // 전체 자물쇠 상태 확인

    private int currentNumber = 0;
    private bool isLocked = false;

    public void OnClickDial()
    {
        if (isLocked) return;

        currentNumber = (currentNumber + 1) % 10;
        dialText.text = currentNumber.ToString();

        if (dialAnimator != null)
            dialAnimator.SetTrigger("Turn");

        if (lockManager != null)
            lockManager.CheckCombination();
    }

    public int GetCurrentNumber()
    {
        return currentNumber;
    }

    public void ResetToZero()
    {
        currentNumber = 0;
        dialText.text = "0";

        if (dialAnimator != null)
            dialAnimator.SetTrigger("Reset");
    }

    public void LockInput(bool state)
    {
        isLocked = state;
    }
}
