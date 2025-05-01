using System.Collections;
using TMPro;
using UnityEngine;
public class BlinkText : MonoBehaviour
{
    public TMP_Text pressText;
    public float fadeDuration = 1f; // 한 쪽 페이드 시간

    private void Start()
    {
        StartCoroutine(FadeLoop());
    }

    IEnumerator FadeLoop()
    {
        while (true)
        {
            // Fade Out (1 → 0)
            yield return StartCoroutine(FadeTextAlpha(1f, 0f));
            // Fade In (0 → 1)
            yield return StartCoroutine(FadeTextAlpha(0f, 1f));
        }
    }

    IEnumerator FadeTextAlpha(float from, float to)
    {
        float elapsed = 0f;
        Color c = pressText.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            pressText.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        pressText.color = new Color(c.r, c.g, c.b, to);
    }
}

