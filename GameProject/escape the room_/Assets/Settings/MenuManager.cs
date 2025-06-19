using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;           // 메뉴 전체 패널
    public GameObject settingsPanel;  // 설정 슬라이더 패널
    public KeyCode toggleKey = KeyCode.Escape;


    void Start()
    {
        // MenuManager도 중복 방지
        if (FindObjectsOfType<MenuManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        bool isActive = !menu.activeSelf;
        menu.SetActive(isActive);

        // 시간 정지/재개
        if (isActive)
        {
            Time.timeScale = 0f; // 게임 일시정지
        }
        else
        {
            Time.timeScale = 1f; // 게임 재개
            settingsPanel.SetActive(false); // 설정 패널 닫기
        }
    }

    public void OnSettingsButton()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void OnQuitButton()
    {
        Application.Quit();
        Debug.Log("게임 종료");
    }
}
