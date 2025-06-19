using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartup : MonoBehaviour
{
    void Awake()
    {
        // 세팅 UI가 있는 씬을 Additive로 불러온다
        SceneManager.LoadScene("SettingUI", LoadSceneMode.Additive);
    }
}
