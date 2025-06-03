using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    public string nextSceneName = "NextScene"; // 바꿀 씬 이름 적기
    public float delay = 10f; // 10초 뒤 전환

    void Start()
    {
        Invoke("ChangeScene", delay);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
