using UnityEngine;

public class PlayerMovem : MonoBehaviour
{
     // 플레이어의 CharacterController
    public float speed = 5f;  // 이동 속도
    public float gravity = -9.8f;  // 중력
    public float jumpHeight = 3f;  // 점프 높이
    private float ySpeed = 0f;  // Y축 속도 (중력에 의해 지속적으로 변화)

    void Update()
    {
        // 수평 이동 입력 (W, A, S, D)
        float x = Input.GetAxis("Horizontal");  // A/D (좌우)
        float z = Input.GetAxis("Vertical");  // W/S (앞뒤)

        // 이동 방향
        Vector3 move = transform.right * x + transform.forward * z;
        transform.position += move * Time.deltaTime;

        // 점프 처리
        

        // 중력 처리
        ySpeed += gravity * Time.deltaTime;  // 지속적으로 중력 적용

        // 이동 벡터에 y축 속도 반영
        move.y = ySpeed;

        // 이동 처리
       
    }
}
