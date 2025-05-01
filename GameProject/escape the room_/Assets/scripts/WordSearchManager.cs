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

        // 퍼즐 종료 후 마우스 커서 숨기기
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // 이동 비활성화
        FindObjectOfType<SC_FPSController>().DisableMovement();

        // 문 열기
        FindObjectOfType<Puzzle7>().OpenDoor();

        StartCoroutine(ExitPuzzleAfterDelay(2f));
    }

    void PuzzleFail()
    {
        puzzleActive = false;
        failPanel.SetActive(true);
        Debug.Log("퍼즐 실패! 다시 도전하세요.");
        // 실패 시에는 씬을 유지하고 UI에서 재시도 버튼만 활성화
        FindObjectOfType<SC_FPSController>().DisableMovement();
    }

    IEnumerator ExitPuzzleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 원래 씬 활성화
        Scene originalScene = SceneManager.GetSceneByName("Scene 1-1");
        if (originalScene.IsValid() && originalScene.isLoaded)
        {
            SceneManager.SetActiveScene(originalScene);
        }

        // 퍼즐 씬 언로드
        SceneManager.UnloadSceneAsync("Puzzle1Scene");

        // 씬 전환 후 마우스 커서 및 화면 잠금 복구
        Cursor.visible = false;  // 마우스 커서 숨기기
        Cursor.lockState = CursorLockMode.Locked;  // 마우스 커서 잠금

        // 이동 활성화
        FindObjectOfType<SC_FPSController>().EnableMovement();
    }

    public void RetryPuzzle()
    {
        // 씬 리셋 제거
        foundWords.Clear();
        puzzleActive = true;
        timerText.text = $"남은 시간: {Mathf.CeilToInt(timeLimit)}초";

        successPanel.SetActive(false);
        failPanel.SetActive(false);

        foreach (Button btn in wordButtons)
        {
            btn.interactable = true;
            btn.GetComponent<Image>().color = Color.white;
        }

        StartCoroutine(StartTimer());
    }
}