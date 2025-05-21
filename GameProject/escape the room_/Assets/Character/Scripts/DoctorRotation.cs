using UnityEngine;

public class DoctorRotation : MonoBehaviour
{
    [Header("회전 설정")]
    public float delayBeforeRotation = 2f;     // 회전 시작 전 대기 시간
    public float rotationDuration = 2f;        // 회전 소요 시간
    public float rotationAngleY = 90f;         // Y축으로 회전할 각도

    private float delayTimer = 0f;
    private float rotationTimer = 0f;
    private bool startRotating = false;
    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private bool hasSetTarget = false;

    void Update()
    {
        if (!startRotating)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer >= delayBeforeRotation)
            {
                startRotating = true;
                rotationTimer = 0f;

                // 현재 회전 기준으로 목표 회전값 설정
                initialRotation = transform.rotation;
                targetRotation = Quaternion.Euler(0f, rotationAngleY, 0f) * initialRotation;
            }
        }
        else if (rotationTimer < rotationDuration)
        {
            rotationTimer += Time.deltaTime;
            float t = Mathf.Clamp01(rotationTimer / rotationDuration);
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);
        }
        // 회전 완료 후는 아무 동작 없음 (애니메이션은 계속 진행됨)
    }
}