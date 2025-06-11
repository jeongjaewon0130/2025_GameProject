using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void StartNewGame()
    {
        Debug.Log("새 게임 시작!");
    }

    public void LoadGame()
    {
        Debug.Log("불러오기 클릭됨");
    }

    public void OpenSettings()
    {
        Debug.Log("설정 클릭됨");
    }

    public void QuitGame()
    {
        Debug.Log("종료 클릭됨");
        Application.Quit();
    }
}
