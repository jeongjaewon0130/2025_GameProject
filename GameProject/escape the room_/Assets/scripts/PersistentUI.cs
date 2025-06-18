using UnityEngine;

public class PersistentUI : MonoBehaviour
{
    private static PersistentUI instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // 중복 제거
        }
    }
}
