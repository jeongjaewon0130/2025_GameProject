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
        // 컷씬 씬 비동기 로드 (싱글 모드)
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(cutsceneSceneName, LoadSceneMode.Single);
        yield return new WaitUntil(() => loadOp.isDone);

        // 씬 로드 완료 → PlayableDirector 바로 실행
        PlayableDirector director = GameObject.FindObjectOfType<PlayableDirector>();

        if (director != null)
        {
            director.time = 0f;
            director.Evaluate();  // 즉시 준비
            director.Play();      // 즉시 재생

            // 컷씬 길이만큼 대기 후 다음 씬 이동
            yield return new WaitForSeconds((float)director.duration);
        }
        else
        {
            Debug.LogWarning("PlayableDirector not found.");
            yield return new WaitForSeconds(1f); // 예외 상황 대비
        }

        SceneManager.LoadScene(nextSceneName);
    }
}