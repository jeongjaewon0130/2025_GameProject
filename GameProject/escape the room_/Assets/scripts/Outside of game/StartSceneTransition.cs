using UnityEngine;

public class StartSceneTransition : MonoBehaviour
{
    public FadeManager fadeManager;

    public void OnClickStart()
    {
        fadeManager.FadeOutAndLoadScene();
    }
}
