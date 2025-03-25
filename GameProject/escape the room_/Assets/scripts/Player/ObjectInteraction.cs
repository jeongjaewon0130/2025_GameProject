using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public float interactRange = 3f; // 상호작용 가능한 거리

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) //E키 누르면 상호작용
        {
            InteractWithObject();
        }
    }

    void InteractWithObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange))
        {
            if (hit.collider.CompareTag("Object"))
            {
                Debug.Log("Object와 상호작용함: " + hit.collider.name);
                Debug.Log("안녕하세요"); // 상호작용 시 "안녕하세요" 출력
                
            }
        }
    }
}