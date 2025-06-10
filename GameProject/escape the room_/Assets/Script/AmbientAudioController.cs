using UnityEngine;
using UnityEngine.SceneManagement;

public class AmbientAudioController : MonoBehaviour
{
    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene != "Scene1")
        {
            // 이 오브젝트는 Scene1이 아니면 제거
            Destroy(gameObject);
        }
    }
}