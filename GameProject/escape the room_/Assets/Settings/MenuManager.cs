using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;           // �޴� ��ü �г�
    public GameObject settingsPanel;  // ���� �����̴� �г�
    public KeyCode toggleKey = KeyCode.Escape;


    void Start()
    {
        // MenuManager�� �ߺ� ����
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

        // �ð� ����/�簳
        if (isActive)
        {
            Time.timeScale = 0f; // ���� �Ͻ�����
        }
        else
        {
            Time.timeScale = 1f; // ���� �簳
            settingsPanel.SetActive(false); // ���� �г� �ݱ�
        }
    }

    public void OnSettingsButton()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void OnQuitButton()
    {
        Application.Quit();
        Debug.Log("���� ����");
    }
}
