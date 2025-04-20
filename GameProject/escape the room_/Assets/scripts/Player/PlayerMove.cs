using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
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

    private bool canMove = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (!canMove)
        {
            velocity = Vector3.zero;
            return;
        }

        // 마우스 회전
        mouseX += Input.GetAxis("Mouse X") * mouseSpeed;
        transform.localEulerAngles = new Vector3(0, mouseX, 0);

        // 이동 속도
        float moveSpeed = speed;

        if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
        {
            moveSpeed *= RunSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            moveSpeed = SitSpeed;
            controller.height = SitHeight;
        }
        else
        {
            controller.height = originalHeight;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move.y = 0;

        controller.Move(move.normalized * moveSpeed * Time.deltaTime);
        controller.Move(new Vector3(0, velocity.y, 0) * Time.deltaTime);
    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }

}