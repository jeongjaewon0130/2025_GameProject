using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintPaper : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject hintUI;
    public GameObject interactText;
    public TMP_Text hintTitleText;
    public TMP_Text hintBodyText;

    [Header("Puzzle Data")]
    public PzleHint4SO hintData;
    public ScriptableObject hintText;

    [Header("Player Control")]
    public SC_FPSController playerMove;
    public CameraRot cameraRot;
    private MouseCursor playerLook;

    private bool isPlayerNearby = false;

    private float savedRotationX = 0f;

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

        if (cameraRot != null)
            cameraRot.SaveRotation();

        if (playerMove != null)
            savedRotationX = playerMove.GetRotationX();

        if (playerMove != null)
            playerMove.SetCanMove(false);

        if (cameraRot != null)
            cameraRot.SetCanLook(false);

        if (playerLook != null)
            playerLook.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        ShowHintText();
    }

    public void CloseUI()
    {
        hintUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (cameraRot != null)
        {
            cameraRot.RestoreRotation();
            cameraRot.SetCanLook(true);
        }

        if (playerMove != null)
        {
            playerMove.SetCanMove(true);
            playerMove.SetRotationX(savedRotationX);
        }

        if (cameraRot != null)
            cameraRot.SetCanLook(true);

        if (playerLook != null)
            playerLook.enabled = true;
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

    void ShowHintText()
    {
        if (hintData == null)
        {
            Debug.LogWarning("힌트 데이터(ScriptableObject)가 할당되지 않았습니다.");
            hintTitleText.text = "힌트 없음";
            hintBodyText.text = "";
            return;
        }

        hintTitleText.text = hintData.hintTitle;

        string result = "";
        foreach (string line in hintData.hintLines)
        {
            result += line + "\n";
        }

        hintBodyText.text = result.TrimEnd();
    }
}
