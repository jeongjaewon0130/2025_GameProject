using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WordSearchManager : MonoBehaviour
{
    [Header("UI 요소")]
    public List<Button> wordButtons; // 퍼즐 단어 버튼들
    public TextMeshProUGUI timerText;
    public GameObject successPanel;
    public GameObject failPanel;

    [Header("정답 설정")]
    public List<string> correctWords = new List<string> { "산책", "사료", "반려견" };

    private HashSet<string> foundWords = new HashSet<string>();
    private float timeLimit = 60f;
    private bool puzzleActive = true;

    public string mainSceneName = "MainScene";

    void Start()
    {
        // 패널은 처음에 숨기기
        successPanel.SetActive(false);
        failPanel.SetActive(false);

        // 버튼 클릭 이벤트 연결
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
        }
        else
        {
            PuzzleFail(); // 오답 누르면 바로 실패 처리
        }

        if (foundWords.Count == correctWords.Count)
        {
            PuzzleSuccess(); // 정답 모두 찾음
        }

        StartCoroutine(ReturnToMainAfterDelay(2f));
    }

    IEnumerator ReturnToMainAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(mainSceneName);
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
            PuzzleFail(); // 시간 초과
        }
    }

    void PuzzleSuccess()
    {
        puzzleActive = false;
        successPanel.SetActive(true);
        Debug.Log("퍼즐 성공! 루미를 얻었습니다.");
    }

    void PuzzleFail()
    {
        puzzleActive = false;
        failPanel.SetActive(true);
        Debug.Log("퍼즐 실패! 다시 도전하세요.");
    }

    public void RetryPuzzle()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
