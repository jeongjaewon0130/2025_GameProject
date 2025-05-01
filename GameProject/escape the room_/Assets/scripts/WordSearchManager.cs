using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WordSearchManager : MonoBehaviour
{
    [Header("UI 요소")]
    public List<Button> wordButtons;
    public TextMeshProUGUI timerText;
    public GameObject successPanel;
    public GameObject failPanel;

    [Header("정답 설정")]
    public List<string> correctWords = new List<string> { "산책", "사료", "반려견" };

    private HashSet<string> foundWords = new HashSet<string>();
    private float timeLimit = 60f;
    private bool puzzleActive = true;

    [Header("씬 설정")]
    public string nextSceneName = "MainScene"; // 퍼즐 성공 시 이동할 씬 이름

    void Start()
    {
        successPanel.SetActive(false);
        failPanel.SetActive(false);

        foreach (Button btn in wordButtons)
        {
            string word = btn.GetComponentInChildren<TextMeshProUGUI>().text;

            btn.onClick.AddListener(() =>
            {
                OnWordClicked(word, btn);
            });
        }

        StartCoroutine(StartTimer());
    }

    void OnWordClicked(string word, Button btn)
    {
        if (!puzzleActive || foundWords.Contains(word))
            return;

        if (correctWords.Contains(word))
        {
            foundWords.Add(word);
            btn.interactable = false;
            btn.GetComponent<Image>().color = Color.green;

            if (foundWords.Count == correctWords.Count)
            {
                PuzzleSuccess();
            }
        }
        else
        {
            PuzzleFail();
        }
    }

    IEnumerator StartTimer()
    {
        float timeLeft = timeLimit;

        while (timeLeft > 0 && puzzleActive)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = $"남은 시간: {Mathf.CeilToInt(timeLeft)}초";
            yield return null;
        }

        if (puzzleActive && foundWords.Count < correctWords.Count)
        {
            PuzzleFail();
        }
    }

    void PuzzleSuccess()
    {
        puzzleActive = false;
        successPanel.SetActive(true);
        Debug.Log("퍼즐 성공! 루미를 얻었습니다.");
        StartCoroutine(GoToNextSceneAfterDelay(2f)); // 2초 후 다음 씬으로 이동
    }

    void PuzzleFail()
    {
        puzzleActive = false;
        failPanel.SetActive(true);
        Debug.Log("퍼즐 실패! 다시 도전하세요.");
        // 실패 시에는 씬을 유지하고 UI에서 재시도 버튼만 활성화
    }

    IEnumerator GoToNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextSceneName);
    }

    public void RetryPuzzle()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 현재 씬 리셋
    }
}