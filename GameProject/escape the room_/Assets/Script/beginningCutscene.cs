using UnityEngine;
using UnityEngine.Video;

public class beginningCutscene : MonoBehaviour
{
    public GameObject cutsceneCam, player;
    public VideoPlayer videoPlayer;

    void Start()
    {
        // 영상 끝났을 때 호출될 이벤트 등록
        videoPlayer.loopPointReached += OnCutsceneEnd;
    }

    void OnCutsceneEnd(VideoPlayer vp)
    {
        Debug.Log("컷신 영상 종료됨, 플레이어로 전환");

        player.SetActive(true);
        cutsceneCam.SetActive(false);
    }
}
