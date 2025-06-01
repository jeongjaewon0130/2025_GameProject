using UnityEngine;

public class KeyPadButton : MonoBehaviour
{
    public string digit;
    public Animator buttonAnimator;

    public void OnPressed()
    {
        Debug.Log("버튼 누름: " + digit);

        if (buttonAnimator != null)
        {
            buttonAnimator.SetTrigger("Press");
        }

        KeyPadManager.Instance.AddDigit(digit);
    }
}
