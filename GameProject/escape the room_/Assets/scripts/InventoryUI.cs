using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;
    public int slotCount = 8;

    void Start()
    {
        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, transform);
        }
    }
}
