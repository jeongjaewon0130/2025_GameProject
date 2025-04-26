using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class interact : MonoBehaviour
{
    public bool interactable;
    public bool toggle;
    public GameObject inttext;
    public GameObject dialogue;
    public string dialogueString;
    public TMPro.TextMeshProUGUI dialogueText;
    public float dialogueTime;
    public AudioSource pickup;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (!toggle)
            {
                inttext.SetActive(true);
                interactable = true;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            inttext.SetActive(false);
            interactable = false;
        }
    }
    void Update()
    {
        if (interactable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogueText.text = dialogueString;
                dialogue.SetActive(true);
                inttext.SetActive(false);
                StartCoroutine(DisableDialogue());
                toggle = true;
                interactable = false;
            }
        }
    }
    IEnumerator DisableDialogue()
    {
        yield return new WaitForSeconds(dialogueTime);
        dialogue.SetActive(false);
    }
}