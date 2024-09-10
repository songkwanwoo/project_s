using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerRotation : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public InputDevice rightController; // 오른쪽 컨트롤러
    public float rotationAngle = 90.0f; // 한 번 회전할 때 각도
    private bool isRotating = false; // 회전 중인지 확인하는 플래그

    void Start()
    {
        // 오른쪽 컨트롤러를 찾기
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
    }

    void Update()
    {
        // 조이스틱 좌우 입력 받기
        Vector2 joystickInput;
        if (rightController.isValid)
        {
            if (rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out joystickInput))
            {
                // 조이스틱을 좌우로 움직였는지 확인 (왼쪽: -0.5 이하, 오른쪽: 0.5 이상)
                if (joystickInput.x > 0.5f && !isRotating) // 오른쪽으로 회전
                {
                    RotatePlayer(rotationAngle); // 90도 회전
                    isRotating = true;
                }
                else if (joystickInput.x < -0.5f && !isRotating) // 왼쪽으로 회전
                {
                    RotatePlayer(-rotationAngle); // -90도 회전
                    isRotating = true;
                }

                // 조이스틱이 중립 위치에 근접하면 다시 회전 가능 상태로
                if (Mathf.Abs(joystickInput.x) < 0.1f)
                {
                    isRotating = false; // 회전 가능 상태로 변경
                }
            }
        }
        else
        {
            Debug.LogWarning("Right controller is not valid. Trying to reconnect...");
            ReconnectController(); // 컨트롤러가 유효하지 않다면 다시 연결 시도
        }

    }

    // 플레이어 회전 함수
    void RotatePlayer(float angle)
    {
        player.Rotate(Vector3.up, angle); // y축 기준으로 플레이어 회전
    }

    void ReconnectController()
    {
        // 컨트롤러를 다시 찾기 위한 코드
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, inputDevices);

        if (inputDevices.Count > 0)
        {
            rightController = inputDevices[0];
            Debug.Log("right controller reconnected!");
        }
        else
        {
            //Debug.LogError("Left controller reconnect failed!");
        }
    }
}
