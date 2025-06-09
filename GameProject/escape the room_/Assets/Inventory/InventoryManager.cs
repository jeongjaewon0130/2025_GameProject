using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryCanvasPrefab;
    private static bool isCreated = false;

    void Awake()
    {
        if (!isCreated)
        {
            isCreated = true;
            GameObject canvas = Instantiate(inventoryCanvasPrefab);
            DontDestroyOnLoad(canvas);
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject); // 중복 제거
        }
    }
}
