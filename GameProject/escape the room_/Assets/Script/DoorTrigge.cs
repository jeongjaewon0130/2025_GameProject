using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Animator doorAnimator; // 문에 붙은 애니메이터

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 지나갈 때만 작동
        {
            doorAnimator.SetTrigger("Open"); // Animator의 "Open" 트리거 실행
        }
    }
}
