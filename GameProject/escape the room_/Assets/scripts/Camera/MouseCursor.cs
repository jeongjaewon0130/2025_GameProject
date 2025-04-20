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
        
    }
}
