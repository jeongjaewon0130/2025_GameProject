using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraRot : MonoBehaviour
{
    
    [SerializeField] private float mouseSpeed = 8f; //ȸ���ӵ�
    private float mouseX = 0f; //�¿� ȸ������ ���� ����
    private float mouseY = 0f; //���Ʒ� ȸ������ ���� ����

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * mouseSpeed;
        mouseY += Input.GetAxis("Mouse Y") * mouseSpeed;

        mouseX = Mathf.Clamp(mouseX, -50f, 30f);
        mouseY = Mathf.Clamp(mouseY, -50f, 30f);

        this.transform.localEulerAngles = new Vector3(-mouseY, mouseX, 0);
    }
}
