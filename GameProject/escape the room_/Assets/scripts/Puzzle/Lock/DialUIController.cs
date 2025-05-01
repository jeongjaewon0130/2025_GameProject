using UnityEngine;

public class DialUIController : MonoBehaviour
{
    public GameObject interactPrompt;   // F키 안내 UI
    public GameObject dialUI;           // 다이얼 UI 전체 패널

    public SC_FPSController playerMove; // FPS 이동 제어
    public CameraRot cameraRot;         // 카메라 회전 제어
    private MouseCursor playerLook;     // 마우스 커서 제어

    private bool isPlayerNear = false;
    private bool isUIOpen = false;
    private bool isLocked = false;
    private bool isUnlocked = false;

    void Start()
    {
        playerLook = FindObjectOfType<MouseCursor>();
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F) && !isUIOpen && !isLocked && !isUnlocked)
        {
            OpenUI();
        }

        if (isUIOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseUI();
        }
    }

    public void LockInput(bool state)
    {
        isLocked = state;
    }

    public void SetUnlocked(bool value)
    {
        isUnlocked = value;
        interactPrompt.SetActive(false);

        // ✅ 커서 다시 잠금 + 숨기기
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerLook != null)
            playerLook.enabled = true;

        if (playerMove != null)
            playerMove.enabled = true;

        if (cameraRot != null)
            cameraRot.enabled = true;
    }

    private void OpenUI()
    {
        dialUI.SetActive(true);
        isUIOpen = true;

        // ✅ 커서 해제 및 표시 (중요!)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (playerLook != null)
            playerLook.enabled = false;

        if (playerMove != null)
            playerMove.enabled = false;

        if (cameraRot != null)
            cameraRot.enabled = false;
    }

    private void CloseUI()
    {
        dialUI.SetActive(false);
        isUIOpen = false;

        // ✅ 커서 다시 잠금 + 숨기기
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerLook != null)
            playerLook.enabled = true;

        if (playerMove != null)
            playerMove.enabled = true;

        if (cameraRot != null)
            cameraRot.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isUnlocked)
        {
            isPlayerNear = true;
            interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            interactPrompt.SetActive(false);
            CloseUI(); // 나갈 때 닫기
        }
    }
}
