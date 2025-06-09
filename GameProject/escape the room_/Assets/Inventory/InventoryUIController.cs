using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    private static InventoryUIController instance;

    public GameObject inventoryPrefab; // 우리가 만든 프리팹 연결용
    private GameObject inventoryInstance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 이 오브젝트는 씬이 바뀌어도 살아있어요

            // 프리팹을 화면에 보여줍니다
            inventoryInstance = Instantiate(inventoryPrefab);
            DontDestroyOnLoad(inventoryInstance);
        }
        else
        {
            Destroy(gameObject); // 이미 있으면 또 만들지 않아요
        }
    }
}
