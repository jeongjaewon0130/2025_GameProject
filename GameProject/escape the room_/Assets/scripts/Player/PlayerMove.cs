using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float jumpHeight = 2f; // 점프 높이
    public float gravity = 10f; // 중력 값
    public float speed = 5f; // 기본 이동 속도
    public float RunSpeed = 2f; // 달리기 속도 배수
    public float SitSpeed = 2.5f; // 앉은 상태 이동 속도
    public float mouseSpeed = 8f; // 마우스 회전 속도
    public float SitHeight = 1f; // 앉았을 때 높이
    private float originalHeight; // 원래 높이 저장

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float mouseX;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height; // 초기 높이 저장
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // 바닥에 붙어 있도록 설정
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // 마우스 회전 처리
        mouseX += Input.GetAxis("Mouse X") * mouseSpeed;
        transform.localEulerAngles = new Vector3(0, mouseX, 0);

        // 이동 처리 (달리기 포함)
        float moveSpeed = speed;

        if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
        {
            moveSpeed *= RunSpeed; // 달리기 속도 증가
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            moveSpeed = SitSpeed; // 앉았을 때 속도 감소
            controller.height = SitHeight; // 높이 변경
        }
        else
        {
            controller.height = originalHeight; // 원래 높이로 복구
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // 이동 벡터 계산 후 y 축 값 제거
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move.y = 0; // y 축 값 제거 (불필요한 이동 방지)

        controller.Move(move.normalized * moveSpeed * Time.deltaTime); // 정규화하여 방향 유지

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}
