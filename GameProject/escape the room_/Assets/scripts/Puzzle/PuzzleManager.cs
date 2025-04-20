using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    public InputField answerInput;
    public TextMeshProUGUI resultText;
    public string mainSceneName = "MainScene"; // ���� �� �̸�

    public void CheckAnswer()
    {
        string userInput = answerInput.text.Trim();

        if (userInput == "10")
        {
            resultText.text = "O! �����Դϴ�! Ÿ�Ӹӽ��� �۵��մϴ�!";
        }
        else
        {
            resultText.text = "X! Ʋ�Ƚ��ϴ�. �ٽ� �õ��ϼ���.";
        }

        StartCoroutine(ReturnToMainAfterDelay(2f)); // ����/���� ������� 2�� �� �̵�
    }

    IEnumerator ReturnToMainAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(mainSceneName);
    }
}
