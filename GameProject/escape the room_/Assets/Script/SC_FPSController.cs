using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 🔧 추가: AudioListener나 Camera 중복 제거
        CleanupExtraListenersAndCameras();

        // 🔧 추가: 카메라 초기화로 흑백 등 비정상 상태 방지
        ResetCameraSettings();
    }

    void Update()
    {
        // 움직임 처리
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        // 카메라 회전
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    // ✅ 추가 함수: 카메라/리스너 중복 정리
    void CleanupExtraListenersAndCameras()
    {
        // AudioListener가 2개 이상이면 첫 번째만 남기고 제거
        AudioListener[] listeners = FindObjectsOfType<AudioListener>();
        for (int i = 1; i < listeners.Length; i++)
        {
            Destroy(listeners[i]);
        }

        // Main Camera가 여러 개 있을 수 있으니 불필요한 건 제거
        Camera[] cameras = FindObjectsOfType<Camera>();
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] != playerCamera)
            {
                Destroy(cameras[i].gameObject);
            }
        }
    }

    // ✅ 추가 함수: 카메라 시각 초기화 (흑백 방지)
    void ResetCameraSettings()
    {
        if (playerCamera != null)
        {
            playerCamera.backgroundColor = Color.black;
            playerCamera.clearFlags = CameraClearFlags.Skybox;

            // 혹시 Post Processing 관련해서 이상한 값이 있는 경우 대비
            if (playerCamera.GetComponent<UnityEngine.Rendering.Volume>() != null)
            {
                var vol = playerCamera.GetComponent<UnityEngine.Rendering.Volume>();
                vol.enabled = true;
            }
        }
    }
}