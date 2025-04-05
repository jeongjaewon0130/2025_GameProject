using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Effect : MonoBehaviour
{
    public Text dialogueText;
    public float typingSpeed = 0.05f;

    private Coroutine typingCoroutine;

    public void StartTyping(string sentence)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    public void ShowFullTextImmediately(string sentence)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        dialogueText.text = sentence;
    }
}