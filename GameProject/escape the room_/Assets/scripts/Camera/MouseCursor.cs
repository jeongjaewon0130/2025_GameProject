using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public GameObject keypadUI;

    void Start()
    {
        LockCursor();
    }

    void Update()
    {
        if (keypadUI.activeSelf)
        {
            UnlockCursor();
        }
        else
        {
            LockCursor();
        }
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
