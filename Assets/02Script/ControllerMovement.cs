using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerMovement : MonoBehaviour
{
    private float baseSpeed = 0.5f; //기본 속도
    private float speedMultiplier = 2.0f; // 조이스틱 이동에 따른 속도 배율

    private InputDevice leftController; // 왼쪽 컨트롤러
    private Vector2 joystickInput; // 조이스틱 입력 값

    void Start()
    {
        // 왼쪽 컨트롤러를 찾기
        var inputDevices = new List<InputDevice>(); // System.Collections.Generic가 필요
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
    }

    void Update()
    {
        if (leftController.isValid)
        {
            // 조이스틱 입력 값 읽어오기
            if (leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out joystickInput))
            {
                // 조이스틱 입력에 따른 이동 방향 및 속도 계산
                Vector3 moveDirection = new Vector3(joystickInput.x, 0, joystickInput.y);
                float moveSpeed = baseSpeed + joystickInput.magnitude * speedMultiplier;

                // 이동
                transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
            }
        }
        else
        {
            Debug.LogWarning("Left controller is not valid. Trying to reconnect...");
            ReconnectController(); // 컨트롤러가 유효하지 않다면 다시 연결 시도
        }
    }

    void ReconnectController()
    {
        // 컨트롤러를 다시 찾기 위한 코드
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, inputDevices);

        if (inputDevices.Count > 0)
        {
            leftController = inputDevices[0];
            //Debug.Log("Left controller reconnected!");
        }
        else
        {
            //Debug.LogError("Left controller reconnect failed!");
        }
    }
}
