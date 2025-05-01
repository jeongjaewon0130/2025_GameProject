using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private FadeManager fadeManager;

    [SerializeField] private float fakeLoadingTime = 5f; // 5초 동안 진행

    void Start()
    {
        StartCoroutine(FakeLoadingRoutine());
    }

    IEnumerator FakeLoadingRoutine()
    {
        float elapsed = 0f;

        // 로딩바 초기화
        if (progressBar != null)
            progressBar.fillAmount = 0f;

        // 5초 동안 로딩바 천천히 증가
        while (elapsed < fakeLoadingTime)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / fakeLoadingTime);

            if (progressBar != null)
                progressBar.fillAmount = progress;

            yield return null;
        }

        // 로딩바 100% 채우기
        if (progressBar != null)
            progressBar.fillAmount = 1f;

        yield return new WaitForSeconds(0.5f); // 약간의 딜레이

        // 페이드 아웃 → 다음 씬
        if (fadeManager != null)
            yield return StartCoroutine(fadeManager.FadeOutAndLoad("Scene 1-1"));
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene("Scene 1-1");
    }
}
