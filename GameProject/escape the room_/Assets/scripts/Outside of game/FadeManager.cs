using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;
    [SerializeField] private string sceneToLoad = "next1";

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // 씬 넘어가도 유지
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (fadeImage != null)
            StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        float time = 0f;
        Color color = fadeImage.color;
        color.a = 1f;

        while (time < fadeDuration)
        {
            color.a = 1f - (time / fadeDuration);
            fadeImage.color = color;
            time += Time.deltaTime;
            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;
    }

    public IEnumerator FadeOutAndLoad(string sceneName)
    {
        float time = 0f;
        Color color = fadeImage.color;
        color.a = 0f;

        while (time < fadeDuration)
        {
            color.a = time / fadeDuration;
            fadeImage.color = color;
            time += Time.deltaTime;
            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;

        SceneManager.LoadScene(sceneName);
    }

    public void FadeOutAndLoadScene()
    {
        StartCoroutine(FadeOutAndLoad(sceneToLoad));
    }
}
