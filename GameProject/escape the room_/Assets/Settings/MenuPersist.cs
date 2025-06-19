using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPersist : MonoBehaviour
{
    public GameObject panelMenu;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);  // �� �̵��ص� ����
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        bool isActive = panelMenu.activeSelf;
        panelMenu.SetActive(!isActive);
        Time.timeScale = isActive ? 1f : 0f;
    }
}

