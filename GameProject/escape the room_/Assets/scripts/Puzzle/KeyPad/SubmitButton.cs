using UnityEngine;

public class SubmitButton : MonoBehaviour
{
    public Animator Animator;
    public void OnPressed()
    {
        if (Animator != null)
        {
            Animator.SetTrigger("Press");
        }
        KeyPadManager.Instance.Submit();
    }
}
