using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 3f;
    public GameObject crosshair;
    public GameObject interactPrompt;

    private PuzzleButton currentButton = null;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.red, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        {
            PuzzleButton button = hit.collider.GetComponent<PuzzleButton>();
            if (button != null)
            {
                interactPrompt.SetActive(true);
                currentButton = button;

                if (Input.GetKeyDown(KeyCode.F))
                {
                    currentButton.OnButtonPressed();
                }

                return;
            }
        }

        interactPrompt.SetActive(false);
        currentButton = null;
    }
}
