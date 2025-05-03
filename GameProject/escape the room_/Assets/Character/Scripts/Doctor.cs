using UnityEngine;

public class Doctor : MonoBehaviour
{
    public Animator animator;             // Animator 연결
    public float walkDuration = 4f;       // Walk 애니메이션 2번 길이 (예: 2초짜리 Walk * 2 = 4초)

    private float walkTimer = 0f;
    private bool hasSwitchedToIdle = false;

    void Start()
    {
        animator.Play("Walk");
    }

    void Update()
    {
        if (hasSwitchedToIdle) return;

        walkTimer += Time.deltaTime;

        if (walkTimer >= walkDuration)
        {
            animator.CrossFade("Idle", 1.0f); // 부드러운 전환 (0.3초 동안 전이)
            hasSwitchedToIdle = true;
        }
    }
}