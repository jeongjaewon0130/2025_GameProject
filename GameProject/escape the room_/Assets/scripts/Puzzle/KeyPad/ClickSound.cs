using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public AudioClip clickSound; // �ν����Ϳ��� �Ҵ�
    private AudioSource audioSource;

    void Start()
    {
        // AudioSource ������Ʈ �������� (������ �ڵ� �߰�)
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false; // ���� �� �ڵ� ��� ����
    }

    void OnMouseDown()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
