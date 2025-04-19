using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    public InputField answerInput;
    public TextMeshProUGUI resultText;
    public string mainSceneName = "MainScene"; // 메인 씬 이름

    public void CheckAnswer()
    {
        string userInput = answerInput.text.Trim();

        if (userInput == "10")
        {
            resultText.text = "O! 정답입니다! 타임머신이 작동합니다!";
        }
        else
        {
            resultText.text = "X! 틀렸습니다. 다시 시도하세요.";
        }

        StartCoroutine(ReturnToMainAfterDelay(2f)); // 정답/오답 상관없이 2초 후 이동
    }

    IEnumerator ReturnToMainAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(mainSceneName);
    }
}
