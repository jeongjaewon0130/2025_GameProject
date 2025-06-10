using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryCanvasPrefab;
    private static bool isCreated = false;
    private GameObject canvasInstance;

    // 컷씬 씬 이름들 (여기에 컷씬 씬 이름을 추가하세요)
    private HashSet<string> cutsceneSceneNames = new HashSet<string> {"CutScene"};

    void Awake()
    {
        if (!isCreated)
        {
            isCreated = true;
            canvasInstance = Instantiate(inventoryCanvasPrefab);
            DontDestroyOnLoad(canvasInstance);
            DontDestroyOnLoad(this.gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드 이벤트 등록
        }
        else
        {
            Destroy(this.gameObject); // 중복 제거
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (canvasInstance != null)
        {
            if (cutsceneSceneNames.Contains(scene.name))
            {
                canvasInstance.SetActive(false); // 컷씬이면 비활성화
            }
            else
            {
                canvasInstance.SetActive(true);  // 아니면 다시 활성화
            }
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 해제
    }
}