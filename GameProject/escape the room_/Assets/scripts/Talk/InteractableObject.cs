using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public GameObject uiPanel; // UI 패널 (이미지 + 버튼들)
    private bool isPlayerInRange = false;

    void Update()
    {
        // 플레이어가 상호작용 범위에 있을 때 'E' 키를 눌렀는지 확인
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E키 눌림! UI 토글 시작.");
            ToggleUI();
        }
    }

    void ToggleUI()
    {
        if (uiPanel != null)
        {
            bool isActive = uiPanel.activeSelf;
            uiPanel.SetActive(!isActive); // UI 패널 활성화/비활성화
            Debug.Log("UI 상태 토글됨: " + !isActive);
        }
        else
        {
            Debug.LogWarning("UI 패널이 연결되지 않았습니다.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 상호작용 범위에 들어옴.");
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 상호작용 범위에서 나감.");
            isPlayerInRange = false;
        }
    }
}