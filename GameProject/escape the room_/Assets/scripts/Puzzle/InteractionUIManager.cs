using UnityEngine;
using UnityEngine.UI;

public class InteractionUIManager : MonoBehaviour
{
    public float interactDistance = 3f;  // 상호작용 가능한 거리
    public KeyCode interactKey = KeyCode.E;  // 상호작용 키 (E키)
    public GameObject interactionUI;  // UI의 전체 패널 (Image와 Button을 포함)
    public GameObject interactionPanel;  // 상호작용 시 나타날 Panel (UI 패널)
    public Image interactionImage;  // 상호작용 시 나타낼 이미지
    public Text interactText;  // "E를 누르세요" 텍스트
    public Button[] interactionButtons;  // 9개의 버튼 배열

    // 상태 추적 변수
    private bool isButton1Pressed = false;  // 1번 버튼이 눌렸는지 여부 추적

    private void Start()
    {
        // UI를 처음에는 비활성화 상태로 설정
        interactionUI.SetActive(false);
        interactionPanel.SetActive(false);  // interactionPanel을 비활성화 상태로 설정
        interactText.gameObject.SetActive(false);  // 텍스트도 비활성화

        // 각 버튼에 클릭 이벤트 연결
        for (int i = 0; i < interactionButtons.Length; i++)
        {
            int buttonIndex = i; // 로컬 변수로 인덱스를 캡처 (Button 클릭 시 필요)
            interactionButtons[buttonIndex].onClick.AddListener(() => OnButtonClicked(buttonIndex));
        }

        // 처음에는 이미지도 비활성화 (UI가 비활성화 되면 이미지는 보이지 않도록)
        if (interactionImage != null)
        {
            interactionImage.gameObject.SetActive(false);  // 이미지 비활성화
        }
    }

    private void Update()
    {
        // 플레이어가 이 오브젝트와 상호작용할 수 있는 거리에 있는지 확인
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        if (distance <= interactDistance)
        {
            // E키가 눌렸을 때 상호작용
            interactText.gameObject.SetActive(true);  // 텍스트 활성화
            interactText.text = "E를 눌러 상호작용";  // 텍스트 변경

            // E키가 눌렸을 때 상호작용
            if (Input.GetKeyDown(interactKey))
            {
                Interact();  // 패널을 띄우는 함수 호출
            }
        }
        else
        {
            // 상호작용 범위를 벗어나면 텍스트 비활성화
            interactText.gameObject.SetActive(false);
        }

        // ESC키를 눌렀을 때 UI를 비활성화하거나 게임을 종료
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseUI();
        }

        // 키 1, 5를 눌렀을 때 콘솔에 "통과" 메시지 출력
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isButton1Pressed = true;  // 버튼 1 눌림 상태 추적
            Debug.Log("버튼 1 눌림");
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (isButton1Pressed)  // 버튼 1이 먼저 눌렸을 때만 통과
            {
                Debug.Log("통과");
                isButton1Pressed = false;  // 상태 초기화
            }
            else
            {
                Debug.Log("버튼 5만 눌림");
            }
        }
    }

    // 상호작용 함수 (E를 눌렀을 때 패널을 띄움)
    private void Interact()
    {
        // UI 패널을 활성화 (interactionPanel)
        if (interactionPanel != null)
        {
            interactionPanel.SetActive(true);  // interactionPanel을 활성화
        }

        // 이미지가 존재하면 활성화
        if (interactionImage != null)
        {
            interactionImage.gameObject.SetActive(true);  // 이미지 활성화
            interactionImage.color = Color.white;  // 이미지 색상을 흰색으로 설정 (보이도록)
        }

        // 버튼들 활성화
        foreach (Button button in interactionButtons)
        {
            button.gameObject.SetActive(true);
        }

        // 상호작용 후 텍스트를 비활성화
        interactText.gameObject.SetActive(false);
    }

    // 버튼 클릭 시 실행될 함수
    private void OnButtonClicked(int buttonIndex)
    {
        // Button1 (index 0)과 Button5 (index 4)를 클릭했을 때 "통과" 메시지 출력
        if (buttonIndex == 0 && !isButton1Pressed)  // 버튼 1을 클릭했을 때
        {
            isButton1Pressed = true;
            Debug.Log("버튼 1 클릭됨");
        }
        else if (buttonIndex == 4 && isButton1Pressed)  // 버튼 5를 클릭했을 때
        {
            Debug.Log("통과");
            isButton1Pressed = false;  // 상태 초기화
        }
        else
        {
            Debug.Log("버튼 " + (buttonIndex + 1) + " 클릭됨");
        }

        // UI 비활성화 (상호작용이 끝났을 때)
        interactionPanel.SetActive(false);  // interactionPanel 비활성화
    }

    // ESC키를 눌렀을 때 UI를 닫거나 게임을 종료하는 함수
    private void CloseUI()
    {
        // UI가 활성화되어 있으면 비활성화
        if (interactionPanel.activeSelf)
        {
            interactionPanel.SetActive(false);
        }
        else
        {
            // 게임을 종료하려면 이 부분을 사용
            // Application.Quit();  // 게임 종료 (실제 빌드에서만 작동)

            // 에디터에서는 종료가 안되므로, 에디터에서 종료할 때도 사용할 수 있도록 아래 코드를 추가
            Debug.Log("ESC 키 눌림: 게임 종료 또는 UI 닫기");
        }
    }
}