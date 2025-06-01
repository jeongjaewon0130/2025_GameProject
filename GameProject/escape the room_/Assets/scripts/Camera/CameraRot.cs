using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraRot : MonoBehaviour
{
    [SerializeField] private float mouseSpeed = 8f;
    private float mouseX = 0f;
    private float mouseY = 0f;

    public bool canLook = true;

    private float savedMouseX;
    private float savedMouseY;

    public bool isLocked = false;

    public void LockRotation()
    {
        isLocked = true;
    }

    public void UnlockRotation()
    {
        isLocked = false;
    }

    void Update()
    {
        if (!canLook || isLocked) return;

        mouseX += Input.GetAxis("Mouse X") * mouseSpeed;
        mouseY += Input.GetAxis("Mouse Y") * mouseSpeed;

        mouseX = Mathf.Clamp(mouseX, -50f, 30f);
        mouseY = Mathf.Clamp(mouseY, -50f, 30f);

        transform.localEulerAngles = new Vector3(-mouseY, mouseX, 0);
    }


    public void SaveRotation()
    {
        savedMouseX = mouseX;
        savedMouseY = mouseY;
    }

    public void RestoreRotation()
    {
        mouseX = savedMouseX;
        mouseY = savedMouseY;
        transform.localEulerAngles = new Vector3(-mouseY, mouseX, 0);
    }

    public void SetCanLook(bool value)
    {
        canLook = value;
    }
}
