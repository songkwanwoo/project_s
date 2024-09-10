using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RightControllerTeleport : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public LineRenderer teleportLine; // 레이캐스트 라인을 그리기 위한 LineRenderer
    public LayerMask teleportMask; // 텔레포트 가능한 표면을 정의하기 위한 레이어 마스크
    public float maxTeleportDistance = 10f; // 최대 텔레포트 거리

    private InputDevice rightController; // 오른쪽 컨트롤러
    private Vector2 joystickInput; // 조이스틱 입력 값
    private bool isTeleporting = false; // 텔레포트 모드 활성화 여부
    private Vector3 teleportTarget; // 텔레포트 목표 지점

    void Start()
    {
        // 오른쪽 컨트롤러 찾기
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, inputDevices);

        if (inputDevices.Count > 0)
        {
            rightController = inputDevices[0];
        }
        else
        {
            Debug.LogError("Right controller not found!");
        }

        // LineRenderer를 비활성화 (처음에는 텔레포트 모드가 꺼져있음)
        teleportLine.enabled = false;
    }

    void Update()
    {
        if (rightController.isValid)
        {
            // 조이스틱 입력 받기
            if (rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out joystickInput))
            {
                // 조이스틱을 앞으로 밀었을 때 레이캐스트를 통한 텔레포트 위치 표시
                if (joystickInput.y > 0.5f && !isTeleporting)
                {
                    isTeleporting = true;
                    teleportLine.enabled = true;
                }

                // 조이스틱을 놓았을 때 텔레포트
                if (isTeleporting && joystickInput.y < 0.1f)
                {
                    TeleportPlayer();
                }
            }

            // 조이스틱을 클릭했을 때 텔레포트 취소
            bool isClicking;
            if (rightController.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out isClicking) && isClicking)
            {
                CancelTeleport();
            }

            // 텔레포트 라인을 계속해서 업데이트
            if (isTeleporting)
            {
                UpdateTeleportLine();
            }
        }
    }

    // 레이캐스트로 텔레포트 가능한 위치 계산 및 라인 렌더링 업데이트
    void UpdateTeleportLine()
    {
        RaycastHit hit;
        Vector3 forwardDirection = transform.forward; // 오른쪽 컨트롤러의 앞 방향
        Vector3 rayStart = transform.position;

        if (Physics.Raycast(rayStart, forwardDirection, out hit, maxTeleportDistance, teleportMask))
        {
            teleportTarget = hit.point; // 텔레포트할 위치 설정
            teleportLine.SetPosition(0, rayStart);
            teleportLine.SetPosition(1, hit.point);
        }
        else
        {
            teleportTarget = rayStart + forwardDirection * maxTeleportDistance;
            teleportLine.SetPosition(0, rayStart);
            teleportLine.SetPosition(1, teleportTarget);
        }
    }

    // 플레이어를 텔레포트할 위치로 이동
    void TeleportPlayer()
    {
        player.position = teleportTarget;
        isTeleporting = false;
        teleportLine.enabled = false;
    }

    // 텔레포트 취소
    void CancelTeleport()
    {
        isTeleporting = false;
        teleportLine.enabled = false;
    }
}
