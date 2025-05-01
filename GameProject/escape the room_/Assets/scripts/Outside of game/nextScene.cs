using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextScene : MonoBehaviour
{
    [SerializeField] private float delayBeforeStart = 3f; // 대기 시간

    void Start()
    {
        StartCoroutine(LoadAfterDelay());
    }

    IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeStart);
        SceneManager.LoadScene("MainScene"); // 대기 후 MainScene 바로 로드
    }
}

