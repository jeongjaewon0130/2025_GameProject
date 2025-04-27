using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    public InputField answerInput;
    public TextMeshProUGUI resultText;
    public GameObject resultPanel; // 새로 추가: 패널 오브젝트 연결
    public string mainSceneName = "MainScene"; // 메인 씬 이름

    private void Start()
    {
        if (resultPanel != null)
            resultPanel.SetActive(false); // 시작할 때 패널 숨기기
    }

    public void CheckAnswer()
    {
        string userInput = answerInput.text.Trim();

        if (resultPanel != null)
            resultPanel.SetActive(true); // 정답/오답 상관없이 패널 보이게

        if (userInput == "10")
        {
            resultText.text = "O! 정답입니다!\n타임머신이 작동합니다!";
        }
        else
        {
            resultText.text = "X! 틀렸습니다.\n 다시 시도하세요.";
        }

        StartCoroutine(ReturnToMainAfterDelay(2f)); // 정답/오답 상관없이 2초 후 이동
    }

    IEnumerator ReturnToMainAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(mainSceneName);
    }
}
