using UnityEngine;
using UnityEngine.SceneManagement;

public class AmbientAudioController : MonoBehaviour
{
    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene != "Scene1")
        {
            // �� ������Ʈ�� Scene1�� �ƴϸ� ����
            Destroy(gameObject);
        }
    }
}