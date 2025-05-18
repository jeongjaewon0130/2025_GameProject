using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintPaper : MonoBehaviour
{
    public GameObject hintUI;
    public GameObject interactText;

    public TextAsset hintJson;

    public SC_FPSController playerMove;
    public CameraRot cameraRot;
    private MouseCursor playerLook;

    private bool isPlayerNearby = false;

    private HintEntry[] hintEntries;


    void Start()
    {
        playerLook = FindObjectOfType<MouseCursor>();

        LoadHintData();
    }

    void LoadHintData()
    {
        if (hintJson == null)
        {
            Debug.LogError("Hint JSON 파일이 할당되지 않았습니다.");
            return;
        }

        // JSON은 배열만 있어서 Unity JsonUtility는 감싸주는 클래스가 필요함
        string wrappedJson = "{\"entries\":" + hintJson.text + "}";

        HintEntryArray data = JsonUtility.FromJson<HintEntryArray>(wrappedJson);
        hintEntries = data.entries;

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
            cameraRot.SetCanLook(false);
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
            cameraRot.SetCanLook(true);
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

    [System.Serializable]
    public class HintEntry
    {
        public string hintTitle;
        public string time;
        public string eventText;
        public int buttonIndex;
    }

    [System.Serializable]
    public class HintEntryArray
    {
        public HintEntry[] entries;
    }
}