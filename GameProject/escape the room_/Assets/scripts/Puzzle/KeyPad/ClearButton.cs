using UnityEngine;

public class ClearButton : MonoBehaviour
{
    public Animator Animator;
    public void OnPressed()
    {
        if (Animator != null)
        {
            Animator.SetTrigger("Press");
        }
        KeyPadManager.Instance.ClearInput();
    }
}
