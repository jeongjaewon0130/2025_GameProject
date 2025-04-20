using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.LowLevel;

public class KeyPadManager : MonoBehaviour
{
    public Animator doorAnimator;

    public GameObject keypadUI;
    public GameObject interactPrompt;

    public TMP_Text inputDisplay;
    private string currentInput = "";
    public string correctCode = "122";

    private bool isPlayerNear = false;

    private MouseCursor playerLook;

    void Start()
    {
        playerLook = FindObjectOfType<MouseCursor>();
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            OpenUI();
        }

        if (keypadUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseUI();
        }
    }

    public void OpenUI()
    {
        keypadUI.SetActive(true);
        interactPrompt.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (playerLook != null)
            playerLook.enabled = false;
    }

    public void CloseUI()
    {
        keypadUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerLook != null)
            playerLook.enabled = true;
    }

    public void AddDigit(string digit)
    {
        if (currentInput.Length < 3)
        {
            currentInput += digit;
            inputDisplay.text = currentInput;
        }
    }

    public void ClearInput()
    {
        currentInput = "";
        inputDisplay.text = "";
    }

    public void Submit()
    {
        if (currentInput == correctCode)
        {
            Debug.Log("정답!");

            if(doorAnimator != null)
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
}
