using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaryEventTrigger : MonoBehaviour
{
    public GameObject scare;
    public AudioSource scareSound;
    public Collider collision;

    void Start()
    {
        // 게임 시작 시 scare 오브젝트 비활성화
        if (scare != null)
        {
            scare.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // scare 오브젝트 활성화
            if (scare != null)
            {
                scare.SetActive(true);
            }

            // 사운드 재생 (주석 해제 시)
            if (scareSound != null)
            {
                scareSound.Play();
            }

            // 트리거 한 번만 작동하도록 콜라이더 비활성화
            if (collision != null)
            {
                collision.enabled = false;
            }
        }
    }
}
