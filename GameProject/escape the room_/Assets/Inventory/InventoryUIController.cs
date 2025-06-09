using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    private static InventoryUIController instance;

    public GameObject inventoryPrefab; // �츮�� ���� ������ �����
    private GameObject inventoryInstance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ������Ʈ�� ���� �ٲ� ����־��

            // �������� ȭ�鿡 �����ݴϴ�
            inventoryInstance = Instantiate(inventoryPrefab);
            DontDestroyOnLoad(inventoryInstance);
        }
        else
        {
            Destroy(gameObject); // �̹� ������ �� ������ �ʾƿ�
        }
    }
}
