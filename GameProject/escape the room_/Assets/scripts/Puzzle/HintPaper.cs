using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintPaper : MonoBehaviour
{
    public GameObject hintUI;
    public GameObject interactText;

    public PlayerMove playerMove;
    public CameraRot cameraRot;
    private MouseCursor playerLook;

    private bool isPlayerNearby = false;

    void Start()
    {
        playerLook = FindObjectOfType<MouseCursor>();
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            OpenUI();
        }

        if (hintUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseUI();
        }
    }

    public void OpenUI()
    {
        hintUI.SetActive(true);
        interactText.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (playerLook != null)
            playerLook.enabled = false;

        if (playerMove != null)
            playerMove.SetCanMove(false);

        if (cameraRot != null)
            cameraRot.enabled = false;
    }

    public void CloseUI()
    {
        hintUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerLook != null)
            playerLook.enabled = true;

        if (playerMove != null)
            playerMove.SetCanMove(true);

        if (cameraRot != null)
            cameraRot.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            interactText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            interactText.SetActive(false);
            CloseUI();
        }
    }
}