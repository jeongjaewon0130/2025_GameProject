using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartup : MonoBehaviour
{
    void Awake()
    {
        // ���� UI�� �ִ� ���� Additive�� �ҷ��´�
        SceneManager.LoadScene("SettingUI", LoadSceneMode.Additive);
    }
}
