using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float jumpHeight = 2f; // ���� ����
    public float gravity = 10f; // �߷� ��
    public float speed = 5f; // �⺻ �̵� �ӵ�
    public float RunSpeed = 2f; // �޸��� �ӵ� ���
    public float SitSpeed = 2.5f; // ���� ���� �̵� �ӵ�
    public float mouseSpeed = 8f; // ���콺 ȸ�� �ӵ�
    public float SitHeight = 1f; // �ɾ��� �� ����
    private float originalHeight; // ���� ���� ����

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float mouseX;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height; // �ʱ� ���� ����
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // �ٴڿ� �پ� �ֵ��� ����
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // ���콺 ȸ�� ó��
        mouseX += Input.GetAxis("Mouse X") * mouseSpeed;
        transform.localEulerAngles = new Vector3(0, mouseX, 0);

        // �̵� ó�� (�޸��� ����)
        float moveSpeed = speed;

        if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
        {
            moveSpeed *= RunSpeed; // �޸��� �ӵ� ����
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            moveSpeed = SitSpeed; // �ɾ��� �� �ӵ� ����
            controller.height = SitHeight; // ���� ����
        }
        else
        {
            controller.height = originalHeight; // ���� ���̷� ����
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // �̵� ���� ��� �� y �� �� ����
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move.y = 0; // y �� �� ���� (���ʿ��� �̵� ����)

        controller.Move(move.normalized * moveSpeed * Time.deltaTime); // ����ȭ�Ͽ� ���� ����

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}
