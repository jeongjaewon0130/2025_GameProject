using UnityEngine;
using UnityEngine.Video;

public class CutsceneTrigger : MonoBehaviour
{
    public GameObject cutsceneCam;
    public GameObject player;
    public VideoPlayer videoPlayer;

    private bool hasPlayed = false;

    void OnTriggerEnter(Collider other)
    {
        if (hasPlayed) return;

        if (other.CompareTag("Player"))
        {
            hasPlayed = true;
            Debug.Log("🎬 트리거 감지 - 컷신 시작");

            player.SetActive(false);
            cutsceneCam.SetActive(true);

            videoPlayer.Play();
            videoPlayer.loopPointReached += OnCutsceneEnd;
        }
    }

    void OnCutsceneEnd(VideoPlayer vp)
    {
        Debug.Log("✅ 컷신 종료 - 플레이어 재활성화");

        cutsceneCam.SetActive(false);
        player.SetActive(true);
    }
}
