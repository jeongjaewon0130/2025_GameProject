using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintPaper : MonoBehaviour
{
    public GameObject hintUI;
    public GameObject interactText;
    private bool isPlayerNearby = false;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            hintUI.SetActive(true);
            interactText.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            hintUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger µé¾î¿È: " + other.name);
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            interactText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            hintUI.SetActive(false);
            interactText.SetActive(false);
        }
    }
}
