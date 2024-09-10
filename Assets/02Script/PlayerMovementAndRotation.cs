using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMovementAndRotation : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    private InputDevice leftController; // 왼쪽 컨트롤러
    private InputDevice rightController; // 오른쪽 컨트롤러
    private Vector2 leftJoystickInput; // 왼쪽 조이스틱 입력 값
    private Vector2 rightJoystickInput; // 오른쪽 조이스틱 입력 값
    private bool isRotating = false; // 회전 중인지 확인하는 플래그

    public float baseSpeed = 0.5f; // 기본 속도
    public float speedMultiplier = 2.0f; // 조이스틱 이동에 따른 속도 배율
    public float rotationAngle = 90.0f; // 한 번 회전할 때 각도

    void Start()
    {
        // 왼쪽 및 오른쪽 컨트롤러를 찾기
        var inputDevices = new List<InputDevice>();

        // 왼쪽 컨트롤러
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, inputDevices);
        if (inputDevices.Count > 0)
        {
            leftController = inputDevices[0];
            Debug.Log("Left controller found!");
        }
        else
        {
            Debug.LogError("Left controller not found!");
        }

        // 오른쪽 컨트롤러
        inputDevices.Clear(); // 리스트를 다시 사용하기 위해 초기화
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, inputDevices);
        if (inputDevices.Count > 0)
        {
            rightController = inputDevices[0];
            Debug.Log("Right controller found!");
        }
        else
        {
            Debug.LogError("Right controller not found!");
        }
    }

    void Update()
    {
        // 왼쪽 조이스틱으로 플레이어 이동
        if (leftController.isValid)
        {
            if (leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out leftJoystickInput))
            {
                // 조이스틱 입력에 따른 이동 방향 및 속도 계산
                Vector3 moveDirection = new Vector3(leftJoystickInput.x, 0, leftJoystickInput.y);
                float moveSpeed = baseSpeed + leftJoystickInput.magnitude * speedMultiplier;

                // 이동
                player.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
            }
        }
        else
        {
            Debug.LogWarning("Left controller is not valid. Trying to reconnect...");
            ReconnectController(ref leftController, XRNode.LeftHand);
        }

        // 오른쪽 조이스틱으로 플레이어 회전
        if (rightController.isValid)
        {
            if (rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out rightJoystickInput))
            {
                // 조이스틱을 좌우로 움직였는지 확인 (왼쪽: -0.5 이하, 오른쪽: 0.5 이상)
                if (rightJoystickInput.x > 0.5f && !isRotating) // 오른쪽으로 회전
                {
                    RotatePlayer(rotationAngle); // 90도 회전
                    isRotating = true;
                }
                else if (rightJoystickInput.x < -0.5f && !isRotating) // 왼쪽으로 회전
                {
                    RotatePlayer(-rotationAngle); // -90도 회전
                    isRotating = true;
                }

                // 조이스틱이 중립 위치에 근접하면 다시 회전 가능 상태로
                if (Mathf.Abs(rightJoystickInput.x) < 0.1f)
                {
                    isRotating = false; // 회전 가능 상태로 변경
                }
            }
        }
        else
        {
            Debug.LogWarning("Right controller is not valid. Trying to reconnect...");
            ReconnectController(ref rightController, XRNode.RightHand);
        }
    }

    // 플레이어 회전 함수
    void RotatePlayer(float angle)
    {
        player.Rotate(Vector3.up, angle); // y축 기준으로 플레이어 회전
    }

    // 컨트롤러 다시 찾기
    void ReconnectController(ref InputDevice controller, XRNode node)
    {
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(node, inputDevices);

        if (inputDevices.Count > 0)
        {
            controller = inputDevices[0];
            //Debug.Log($"{node} controller reconnected!");
        }
        else
        {
            //Debug.LogError($"{node} controller reconnect failed!");
        }
    }
}
