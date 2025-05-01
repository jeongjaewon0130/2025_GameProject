using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIPuzzle : MonoBehaviour
{
    public GameObject interactionText;             // "Press E" 텍스트
    public GameObject interactionPanel;            // 퍼즐 UI 패널
    public MonoBehaviour playerMovementScript;     // 플레이어 이동 스크립트
    public Rigidbody playerRigidbody;              // 플레이어 Rigidbody
    public CameraRot cameraRotScript;              // 카메라 회전 스크립트

    public Button[] numberButtons = new Button[9]; // 숫자 버튼들 (1~9)

    private bool playerInRange = false;
    private bool isInteracting = false;
    private Vector3 savedVelocity;
    private List<int> buttonSequence = new List<int>();

    void Start()
    {
        interactionText.SetActive(false);
        interactionPanel.SetActive(false);

        // 버튼 클릭 이벤트 연결
        for (int i = 0; i < numberButtons.Length; i++)
        {
            int number = i + 1;
            if (numberButtons[i] != null)
                numberButtons[i].onClick.AddListener(() => OnNumberButtonClicked(number));
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isInteracting)
        {
            StartInteraction();
        }
        else if (isInteracting && Input.GetKeyDown(KeyCode.Escape))
        {
            EndInteraction();
        }

        if (isInteracting)
        {
            CheckNumberKeyInput();
        }
    }

    void CheckNumberKeyInput()
    {
        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                AddNumberToSequence(i);
            }
        }
    }

    void OnNumberButtonClicked(int number)
    {
        if (isInteracting)
        {
            AddNumberToSequence(number);
        }
    }

    void AddNumberToSequence(int number)
    {
        buttonSequence.Add(number);

        if (buttonSequence.Count == 2)
        {
            if (buttonSequence[0] == 1 && buttonSequence[1] == 5)
            {
                Debug.Log("통과");
            }
            else
            {
                Debug.Log("실패");
            }

            buttonSequence.Clear();
        }
    }

    void StartInteraction()
    {
        isInteracting = true;
        interactionPanel.SetActive(true);
        interactionText.SetActive(false);
        buttonSequence.Clear();

        if (playerMovementScript != null) playerMovementScript.enabled = false;

        if (playerRigidbody != null)
        {
            savedVelocity = playerRigidbody.velocity;
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;
            playerRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        if (cameraRotScript != null)
            cameraRotScript.enabled = false; // 카메라 회전 비활성화

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void EndInteraction()
    {
        isInteracting = false;
        interactionPanel.SetActive(false);

        if (playerMovementScript != null) playerMovementScript.enabled = true;

        if (playerRigidbody != null)
        {
            playerRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            playerRigidbody.velocity = savedVelocity;
        }

        if (cameraRotScript != null)
            cameraRotScript.enabled = true; // 카메라 회전 다시 활성화

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerInRange)
        {
            interactionText.SetActive(true);
        }

        buttonSequence.Clear();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (!isInteracting)
                interactionText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactionText.SetActive(false);
            if (isInteracting)
            {
                EndInteraction();
            }
        }
    }
}
