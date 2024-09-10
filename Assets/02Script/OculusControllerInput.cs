using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class OculusControllerInput : MonoBehaviour
{
    private InputDevice rightController;
    private InputDevice leftController;

    // 오른쪽 컨트롤러 입력값과 이전 값
    private float rightGripValue;
    private float previousRightGripValue;

    private float rightTriggerValue;
    private float previousRightTriggerValue;

    private Vector2 rightJoystickValue;
    private Vector2 previousRightJoystickValue;

    // 왼쪽 컨트롤러 입력값과 이전 값
    private float leftGripValue;
    private float previousLeftGripValue;

    private float leftTriggerValue;
    private float previousLeftTriggerValue;

    private Vector2 leftJoystickValue;
    private Vector2 previousLeftJoystickValue;

    // 버튼 상태 저장
    private bool previousRightAButton;
    private bool previousRightBButton;
    private bool previousLeftXButton;
    private bool previousLeftYButton;

    void Start()
    {
        InitializeControllers();
    }

    // 오큘러스 컨트롤러 초기화
    void InitializeControllers()
    {
        List<InputDevice> devices = new List<InputDevice>();

        // 오른손 컨트롤러
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            rightController = devices[0];
        }

        // 왼손 컨트롤러
        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            leftController = devices[0];
        }
    }

    void Update()
    {
        // 오른손 컨트롤러 입력 처리
        if (!rightController.isValid)
        {
            InitializeControllers(); // 장치가 유효하지 않으면 다시 초기화
        }
        else
        {
            HandleRightControllerInput();
        }

        // 왼손 컨트롤러 입력 처리
        if (!leftController.isValid)
        {
            InitializeControllers();
        }
        else
        {
            HandleLeftControllerInput();
        }
    }

    void HandleRightControllerInput()
    {
        // Grip 입력 처리 (변화가 있을 때만 로그 출력)
        if (rightController.TryGetFeatureValue(CommonUsages.grip, out rightGripValue))
        {
            if (rightGripValue != previousRightGripValue)
            {
                if (rightGripValue == 1)
                {
                    Debug.Log("오른손 그립 버튼을 누름");
                }
                previousRightGripValue = rightGripValue;
            }
        }

        // Trigger 입력 처리
        if (rightController.TryGetFeatureValue(CommonUsages.trigger, out rightTriggerValue))
        {
            if (rightTriggerValue != previousRightTriggerValue)
            {
                if (rightTriggerValue == 1)
                {
                    Debug.Log("오른손 트리거 버튼을 누름");
                }
                previousRightTriggerValue = rightTriggerValue;
            }
        }

        // Joystick 입력 처리
        if (rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out rightJoystickValue))
        {
            if (rightJoystickValue != previousRightJoystickValue)
            {
                if (rightJoystickValue != Vector2.zero)
                {
                    Debug.Log("오른손 조이스틱을 움직임");
                }
                previousRightJoystickValue = rightJoystickValue;
            }
        }

        // A 버튼 입력 처리
        if (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool rightAButton))
        {
            if (rightAButton != previousRightAButton)
            {
                if (rightAButton)
                {
                    Debug.Log("오른손 A 버튼을 눌렀음");
                }
                else
                {
                    Debug.Log("오른손 A 버튼을 뗌");
                }
                previousRightAButton = rightAButton;
            }
        }

        // B 버튼 입력 처리
        if (rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool rightBButton))
        {
            if (rightBButton != previousRightBButton)
            {
                if (rightBButton)
                {
                    Debug.Log("오른손 B 버튼을 눌렀음");
                }
                else
                {
                    Debug.Log("오른손 B 버튼을 뗌");
                }
                previousRightBButton = rightBButton;
            }
        }
    }

    void HandleLeftControllerInput()
    {
        // Grip 입력 처리 (변화가 있을 때만 로그 출력)
        if (leftController.TryGetFeatureValue(CommonUsages.grip, out leftGripValue))
        {
            if (leftGripValue != previousLeftGripValue)
            {
                if (leftGripValue == 1)
                {
                    Debug.Log("왼손 그립 버튼을 누름");
                }
                previousLeftGripValue = leftGripValue;
            }
        }

        // Trigger 입력 처리
        if (leftController.TryGetFeatureValue(CommonUsages.trigger, out leftTriggerValue))
        {
            if (leftTriggerValue != previousLeftTriggerValue)
            {
                if (leftTriggerValue == 1)
                {
                    Debug.Log("왼손 트리거 버튼을 누름");
                }
                previousLeftTriggerValue = leftTriggerValue;
            }
        }

        // Joystick 입력 처리
        if (leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out leftJoystickValue))
        {
            if (leftJoystickValue != previousLeftJoystickValue)
            {
                if (leftJoystickValue != Vector2.zero)
                {
                    Debug.Log("왼손 조이스틱을 움직임");
                }
                previousLeftJoystickValue = leftJoystickValue;
            }
        }

        // X 버튼 입력 처리
        if (leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool leftXButton))
        {
            if (leftXButton != previousLeftXButton)
            {
                if (leftXButton)
                {
                    Debug.Log("왼손 X 버튼을 눌렀음");
                }
                else
                {
                    Debug.Log("왼손 X 버튼을 뗌");
                }
                previousLeftXButton = leftXButton;
            }
        }

        // Y 버튼 입력 처리
        if (leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool leftYButton))
        {
            if (leftYButton != previousLeftYButton)
            {
                if (leftYButton)
                {
                    Debug.Log("왼손 Y 버튼을 눌렀음");
                }
                else
                {
                    Debug.Log("왼손 Y 버튼을 뗌");
                }
                previousLeftYButton = leftYButton;
            }
        }
    }
}
