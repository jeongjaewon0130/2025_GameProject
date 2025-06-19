using UnityEngine;
using UnityEngine.Video;

public class beginningCutscene : MonoBehaviour
{
    public GameObject cutsceneCam, player;
    public VideoPlayer videoPlayer;

    void Start()
    {
        // ���� ������ �� ȣ��� �̺�Ʈ ���
        videoPlayer.loopPointReached += OnCutsceneEnd;
    }

    void OnCutsceneEnd(VideoPlayer vp)
    {
        Debug.Log("�ƽ� ���� �����, �÷��̾�� ��ȯ");

        player.SetActive(true);
        cutsceneCam.SetActive(false);
    }
}
