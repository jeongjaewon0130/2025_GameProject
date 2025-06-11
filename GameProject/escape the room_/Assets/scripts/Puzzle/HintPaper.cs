using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class HintPaper : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject hintUI;
    public GameObject interactText;
    public TMP_Text hintTitleText;
    public TMP_Text hintBodyText;

    [Header("Player Control")]
    public SC_FPSController playerMove;
    public CameraRot cameraRot;
    private MouseCursor playerLook;

    private bool isPlayerNearby = false;

    private float savedRotationX = 0f;

    [Header("Hint JSON File")]
    public string jsonFileName = "HintData.json";  // Resources 폴더에 넣고 경로는 파일명만

    private HintJsonData hintData;

    void Start()
    {
        playerLook = FindObjectOfType<MouseCursor>();
        LoadHintFromJSON();
    }

    void LoadHintFromJSON()
    {
        // Resources 폴더에서 텍스트 에셋으로 JSON 읽기
        TextAsset jsonText = Resources.Load<TextAsset>(Path.GetFileNameWithoutExtension(jsonFileName));
        if (jsonText == null)
        {
            Debug.LogError($"JSON 파일을 Resources에서 찾을 수 없습니다: {jsonFileName}");
            return;
        }

        hintData = JsonUtility.FromJson<HintJsonData>(jsonText.text);
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
            Debug.Log("새 힌트 오브젝트: 플레이어가 근처에 들어옴");
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
            if (hintUI.activeSelf)
            {
                CloseUI();
            }
        }
    }

    void ShowHintText()
    {
        if (hintData == null || hintData.hintLines == null || hintData.hintLines.Count == 0)
        {
            Debug.LogWarning("힌트 데이터가 없거나 비어 있습니다. 기존 텍스트 유지.");
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
