using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public bool isCorrectButton = false;
    public GameObject doorToOpen;

    private bool isActivated = false;

    public void OnButtonPressed()
    {
        if (isActivated) return;

        if (isCorrectButton)
        {
            Debug.Log("정답! 문이 열립니다.");
            doorToOpen.GetComponent<Animator>().SetTrigger("Open");
        }
        else
        {
            Debug.Log("오답입니다.");
        }

        isActivated = true;
    }
}
