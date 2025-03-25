using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //마우스 커서를 화면에서 중앙으로 고정
        Cursor.visible = false; // 마우스 커서를 숨김
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None; //마우스 잠금 헤제
            Cursor.visible = true; //마우스 커서 보이기
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Locked; //마우스 잠금 헤제
            Cursor.visible = false; //마우스 커서 보이기
        }
    }
}
