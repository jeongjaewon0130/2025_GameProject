using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.LowLevel;

public class KeyPadManager : MonoBehaviour
{
    public Camera mainCamera;
    public Transform keypadFocusPoint;
    public SC_FPSController playerMove;
    public Animator doorAnimator;
    public GameObject keypadUI;
    public GameObject interactPrompt;
    private string currentInput = "";
    public string correctCode = "112222";
    private bool isPlayerNear = false;
    private MouseCursor playerLook;
    public static KeyPadManager Instance;
    public GameObject playerArm;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public bool IsKeypadActive => keypadUI.activeSelf;

    void Start()
    {
        playerLook = FindObjectOfType<MouseCursor>();
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F 키 눌림 - UI 열기 시도");
            OpenUI();
        }

        if (keypadUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC 눌림 - UI 닫기 시도");
            CloseUI();
        }

    }

    public void OpenUI()
    {
        mainCamera.transform.position = keypadFocusPoint.position;
        mainCamera.transform.rotation = keypadFocusPoint.rotation;

        interactPrompt.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (playerLook != null)
            playerLook.enabled = false;

        if (playerMove != null)
            playerMove.SetCanMove(false);

        if (playerArm != null)
            playerArm.SetActive(false);
    }

    public void CloseUI()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerLook != null)
            playerLook.enabled = true;

        if (playerMove != null)
            playerMove.SetCanMove(true);

        if (playerArm != null)
            playerArm.SetActive(true);
    }

    public void AddDigit(string digit)
    {
        Debug.Log($"AddDigit 호출 - 현재Input(before): {currentInput} / digit: {digit} / InstanceID: {GetInstanceID()}");

        currentInput = currentInput + digit;

        Debug.Log($"AddDigit 호출 - 현재Input(after): {currentInput} / InstanceID: {GetInstanceID()}");
    }



    public void ClearInput()
    {
        Debug.Log("입력 초기화 ClearInput() 호출됨");
        currentInput = "";
    }

    public void Submit()
    {
        Debug.Log("현재 입력값: [" + currentInput + "]");

        if (currentInput.Trim() == correctCode)
        {
            Debug.Log("정답!");
            if (doorAnimator != null)
            {
                doorAnimator.SetTrigger("Open");
            }
        }
        else
        {
            Debug.Log("오답");
            ClearInput();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            interactPrompt.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            interactPrompt.SetActive(false);
            CloseUI();
        }
    }

    void LateUpdate()
    {
        if (IsKeypadActive && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Raycast 시도됨");

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Raycast 히트: " + hit.collider.name);

                if (hit.collider.TryGetComponent<KeyPadButton>(out var keyBtn))
                {
                    Debug.Log("KeyPadButton 감지됨");
                    keyBtn.OnPressed();
                }
                else if (hit.collider.TryGetComponent<SubmitButton>(out var submitBtn))
                {
                    Debug.Log("SubmitButton 감지됨");
                    submitBtn.OnPressed();
                }
                else if (hit.collider.TryGetComponent<ClearButton>(out var clearBtn))
                {
                    Debug.Log("ClearButton 감지됨");
                    clearBtn.OnPressed();
                }
            }
        }
    }
}
