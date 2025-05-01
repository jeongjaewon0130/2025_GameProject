using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzle1Mnger : MonoBehaviour
{
    public static Puzle1Mnger Instance;
    public bool wordPuzzleCompleted = false;

    void Awake()
    {
        Debug.Log("GameManager 생성됨");

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("중복 GameManager 삭제됨");
            Destroy(gameObject);
        }
    }
}