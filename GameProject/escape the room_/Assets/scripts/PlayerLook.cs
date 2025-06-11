using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform playerBody;
    public float sensitivity = 1f;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // 🔁 감도 불러오기
        sensitivity = PlayerPrefs.GetFloat("sensitivity", 1f);
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // 카메라 상하 회전
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 플레이어 좌우 회전
        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void OnSensitivityChanged(float value)
    {
        PlayerPrefs.SetFloat("sensitivity", value);
    }

}

