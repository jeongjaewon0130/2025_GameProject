using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void StartNewGame()
    {
        Debug.Log("�� ���� ����!");
    }

    public void LoadGame()
    {
        Debug.Log("�ҷ����� Ŭ����");
    }

    public void OpenSettings()
    {
        Debug.Log("���� Ŭ����");
    }

    public void QuitGame()
    {
        Debug.Log("���� Ŭ����");
        Application.Quit();
    }
}
