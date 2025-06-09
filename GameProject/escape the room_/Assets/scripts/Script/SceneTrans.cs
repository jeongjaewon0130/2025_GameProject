using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using System.Collections;

public class SceneTrans : MonoBehaviour
{
    public string cutsceneSceneName = "CutScene";
    public string nextSceneName = "Scene 1-3";

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(LoadCutsceneAndPlayImmediately());
        }
    }

    IEnumerator LoadCutsceneAndPlayImmediately()
    {
        // �ƾ� �� �񵿱� �ε� (�̱� ���)
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(cutsceneSceneName, LoadSceneMode.Single);
        yield return new WaitUntil(() => loadOp.isDone);

        // �� �ε� �Ϸ� �� PlayableDirector �ٷ� ����
        PlayableDirector director = GameObject.FindObjectOfType<PlayableDirector>();

        if (director != null)
        {
            director.time = 0f;
            director.Evaluate();  // ��� �غ�
            director.Play();      // ��� ���

            // �ƾ� ���̸�ŭ ��� �� ���� �� �̵�
            yield return new WaitForSeconds((float)director.duration);
        }
        else
        {
            Debug.LogWarning("PlayableDirector not found.");
            yield return new WaitForSeconds(1f); // ���� ��Ȳ ���
        }

        SceneManager.LoadScene(nextSceneName);
    }
}