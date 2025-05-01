using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    public GameObject gameManagerPrefab;

    void Awake()
    {
        if (Puzle1Mnger.Instance == null)
        {
            Instantiate(gameManagerPrefab);
            Debug.Log("GameManager 프리팹 인스턴스 생성됨");
        }
    }
}
