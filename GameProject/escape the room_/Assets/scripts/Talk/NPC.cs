using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public string[] dialogueLines;
    public string npcName = "NPC 이름";

    public GameObject interactionText;

    private bool isPlayerInRange;

    void Start()
    {
        if (interactionText != null)
            interactionText.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            dialogueManager.StartDialogue(dialogueLines, npcName);
            if (interactionText != null)
                interactionText.SetActive(false);

            if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("F 키 입력 확인!");
                dialogueManager.StartDialogue(dialogueLines, npcName);
                if (interactionText != null)
                    interactionText.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (interactionText != null)
                interactionText.SetActive(true);

            if (other.CompareTag("Player"))
            {
                Debug.Log("플레이어가 범위에 들어옴");
                isPlayerInRange = true;
                if (interactionText != null)
                    interactionText.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (interactionText != null)
                interactionText.SetActive(false);
        }
    }
}