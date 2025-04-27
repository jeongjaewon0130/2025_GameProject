using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WordSearchManager : MonoBehaviour
{
    [Header("UI ���")]
    public List<Button> wordButtons; // ���� �ܾ� ��ư��
    public TextMeshProUGUI timerText;
    public GameObject successPanel;
    public GameObject failPanel;

    [Header("���� ����")]
    public List<string> correctWords = new List<string> { "��å", "���", "�ݷ���" };

    private HashSet<string> foundWords = new HashSet<string>();
    private float timeLimit = 60f;
    private bool puzzleActive = true;

    public string mainSceneName = "MainScene";

    void Start()
    {
        // �г��� ó���� �����
        successPanel.SetActive(false);
        failPanel.SetActive(false);

        // ��ư Ŭ�� �̺�Ʈ ����
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
            PuzzleFail(); // ���� ������ �ٷ� ���� ó��
        }

        if (foundWords.Count == correctWords.Count)
        {
            PuzzleSuccess(); // ���� ��� ã��
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
            timerText.text = $"���� �ð�: {Mathf.CeilToInt(timeLeft)}��";
            yield return null;
        }

        if (puzzleActive && foundWords.Count < correctWords.Count)
        {
            PuzzleFail(); // �ð� �ʰ�
        }
    }

    void PuzzleSuccess()
    {
        puzzleActive = false;
        successPanel.SetActive(true);
        Debug.Log("���� ����! ��̸� ������ϴ�.");
    }

    void PuzzleFail()
    {
        puzzleActive = false;
        failPanel.SetActive(true);
        Debug.Log("���� ����! �ٽ� �����ϼ���.");
    }

    public void RetryPuzzle()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
