using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Puzzle7 : MonoBehaviour
{
    public string puzzle_1;
    public GameObject interactUI;
    private bool isPlayerNear = false;

    public GameObject door;
    public Animator doorAnimator;

    void Start()
    {
        if (interactUI != null)
            interactUI.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Puzzle1Scene", LoadSceneMode.Additive);

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (interactUI != null)
                interactUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (interactUI != null)
                interactUI.SetActive(false);
        }
    }

    public void OpenDoor()
    {
        if (door != null)
        {
            door.SetActive(true);
        }

        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open");
        }
    }
}
