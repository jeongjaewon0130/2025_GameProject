using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public int buttonOrder;                      // 이 버튼의 순서
    public BtnPuzzleManager puzzleManager;      // 퍼즐 매니저 연결

    [Header("Button Feedback")]
    public Light buttonLight;                   // 버튼 라이트
    public AudioSource audioSource;             // 사운드 재생기
    public AudioClip clickSound;                // 클릭 사운드

    private bool isActivated = false;           // 버튼이 이미 눌렸는지 여부

    public void OnButtonPressed()
    {
        // 이미 눌린 버튼은 무시
        if (isActivated) return;

        puzzleManager.ButtonPressed(this);
    }

    public void Activate()
    {
        isActivated = true;

        // 라이트 켜기
        if (buttonLight != null)
            buttonLight.enabled = true;

        // 사운드 재생
        if (audioSource != null && clickSound != null)
            audioSource.PlayOneShot(clickSound);
    }

    public void ResetButton()
    {
        isActivated = false;

        // 라이트 끄기
        if (buttonLight != null)
            buttonLight.enabled = false;
    }

    public bool IsActivated()
    {
        return isActivated;
    }
}
