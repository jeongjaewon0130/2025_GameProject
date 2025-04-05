using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public PlayerMove playerMove;

    private string[] dialogueLines;
    private int currentLineIndex;

    public bool isDialogueActive { get; private set; } = false;

    private Coroutine typingCoroutine;
    public float typingSpeed = 0.05f;

    private bool isTyping = false;

    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    public void StartDialogue(string[] lines, string npcName)
    {
        dialogueLines = lines;
        currentLineIndex = 0;
        nameText.text = npcName;
        isDialogueActive = true;

        dialoguePanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        DisplayNextLine();

        if (playerMove != null)
            playerMove.SetCanMove(false);
    }

    private void OnNextButtonPressed()
    {
        if (isTyping)
            return;

        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (currentLineIndex < dialogueLines.Length)
        {
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            typingCoroutine = StartCoroutine(TypeSentence(dialogueLines[currentLineIndex]));
            currentLineIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        typingCoroutine = null;
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        isDialogueActive = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (playerMove != null)
            playerMove.SetCanMove(true);
    }

    public void OnDialoguePanelClicked()
    {
        Debug.Log("패널 클릭됨");

        if (!isDialogueActive)
            return;

        if (isTyping)
            return;

        DisplayNextLine();
    }
}