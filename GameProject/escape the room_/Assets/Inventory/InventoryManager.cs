using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryCanvasPrefab;
    private static bool isCreated = false;
    private GameObject canvasInstance;

    // �ƾ� �� �̸��� (���⿡ �ƾ� �� �̸��� �߰��ϼ���)
    private HashSet<string> cutsceneSceneNames = new HashSet<string> {"CutScene"};

    void Awake()
    {
        if (!isCreated)
        {
            isCreated = true;
            canvasInstance = Instantiate(inventoryCanvasPrefab);
            DontDestroyOnLoad(canvasInstance);
            DontDestroyOnLoad(this.gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded; // �� �ε� �̺�Ʈ ���
        }
        else
        {
            Destroy(this.gameObject); // �ߺ� ����
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (canvasInstance != null)
        {
            if (cutsceneSceneNames.Contains(scene.name))
            {
                canvasInstance.SetActive(false); // �ƾ��̸� ��Ȱ��ȭ
            }
            else
            {
                canvasInstance.SetActive(true);  // �ƴϸ� �ٽ� Ȱ��ȭ
            }
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // �̺�Ʈ ����
    }
}