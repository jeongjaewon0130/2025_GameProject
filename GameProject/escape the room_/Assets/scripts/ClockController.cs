using UnityEngine;
using TMPro;  // TextMeshPro 네임스페이스
using UnityEngine.SceneManagement;  // 씬 전환하려면 필요해요

public class ClockController : MonoBehaviour
{
    public TextMeshProUGUI clockText;  // 텍스트 오브젝트를 연결할 변수
    private int hours = 5;  // 시작 시간 (5시)
    private int minutes = 0;  // 시작 분 (0분)
    public TextMeshProUGUI messageText;  // 메시지 텍스트를 연결할 변수

    void Start()
    {
        UpdateClockDisplay();  // 시계 업데이트
    }

    // 시계를 업데이트하는 함수
    void UpdateClockDisplay()
    {
        clockText.text = string.Format("{0:D2}:{1:D2}", hours, minutes);  // 시간을 "05:00" 형식으로 표시
        CheckGoalTime();  // 목표 시간 확인
    }

    // +1분 버튼 클릭 시 호출되는 함수
    public void AddMinute()
    {
        minutes++;
        if (minutes >= 60)  // 60분을 넘으면 1시간 추가
        {
            minutes = 0;
            hours++;
            if (hours >= 24)  // 24시간을 넘으면 0시로 돌아가기
            {
                hours = 0;
            }
        }
        UpdateClockDisplay();
    }

    // -1분 버튼 클릭 시 호출되는 함수
    public void SubtractMinute()
    {
        minutes--;
        if (minutes < 0)  // 0분보다 적으면 1시간 빼기
        {
            minutes = 59;
            hours--;
            if (hours < 0)  // 0시보다 적으면 23시로 돌아가기
            {
                hours = 23;
            }
        }
        UpdateClockDisplay();
    }

    // 목표 시간(5:24)에 도달했을 때 호출되는 함수
    void CheckGoalTime()
    {
        if (hours == 5 && minutes == 24)
        {
            messageText.text = "지하철을 탈 수 있습니다!";  // 성공 메시지 출력
        }
        else
        {
            messageText.text = "";  // 목표 시간에 도달하지 않았을 경우 메시지 제거
        }
    }

    public void TryBoardTrain()
    {
        if (hours == 5 && minutes == 24)
        {
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            messageText.text = "아직 지하철을 탈 수 없습니다!";
        }
    }
}
