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
            Debug.Log("����! ���� �����ϴ�.");
            doorToOpen.GetComponent<Animator>().SetTrigger("Open");
        }
        else
        {
            Debug.Log("�����Դϴ�.");
        }

        isActivated = true;
    }
}
