using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class OculusControllerInput : MonoBehaviour
{
    private InputDevice rightController;
    private InputDevice leftController;

    // ������ ��Ʈ�ѷ� �Է°��� ���� ��
    private float rightGripValue;
    private float previousRightGripValue;

    private float rightTriggerValue;
    private float previousRightTriggerValue;

    private Vector2 rightJoystickValue;
    private Vector2 previousRightJoystickValue;

    // ���� ��Ʈ�ѷ� �Է°��� ���� ��
    private float leftGripValue;
    private float previousLeftGripValue;

    private float leftTriggerValue;
    private float previousLeftTriggerValue;

    private Vector2 leftJoystickValue;
    private Vector2 previousLeftJoystickValue;

    // ��ư ���� ����
    private bool previousRightAButton;
    private bool previousRightBButton;
    private bool previousLeftXButton;
    private bool previousLeftYButton;

    void Start()
    {
        InitializeControllers();
    }

    // ��ŧ���� ��Ʈ�ѷ� �ʱ�ȭ
    void InitializeControllers()
    {
        List<InputDevice> devices = new List<InputDevice>();

        // ������ ��Ʈ�ѷ�
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            rightController = devices[0];
        }

        // �޼� ��Ʈ�ѷ�
        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            leftController = devices[0];
        }
    }

    void Update()
    {
        // ������ ��Ʈ�ѷ� �Է� ó��
        if (!rightController.isValid)
        {
            InitializeControllers(); // ��ġ�� ��ȿ���� ������ �ٽ� �ʱ�ȭ
        }
        else
        {
            HandleRightControllerInput();
        }

        // �޼� ��Ʈ�ѷ� �Է� ó��
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
        // Grip �Է� ó�� (��ȭ�� ���� ���� �α� ���)
        if (rightController.TryGetFeatureValue(CommonUsages.grip, out rightGripValue))
        {
            if (rightGripValue != previousRightGripValue)
            {
                if (rightGripValue == 1)
                {
                    Debug.Log("������ �׸� ��ư�� ����");
                }
                previousRightGripValue = rightGripValue;
            }
        }

        // Trigger �Է� ó��
        if (rightController.TryGetFeatureValue(CommonUsages.trigger, out rightTriggerValue))
        {
            if (rightTriggerValue != previousRightTriggerValue)
            {
                if (rightTriggerValue == 1)
                {
                    Debug.Log("������ Ʈ���� ��ư�� ����");
                }
                previousRightTriggerValue = rightTriggerValue;
            }
        }

        // Joystick �Է� ó��
        if (rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out rightJoystickValue))
        {
            if (rightJoystickValue != previousRightJoystickValue)
            {
                if (rightJoystickValue != Vector2.zero)
                {
                    Debug.Log("������ ���̽�ƽ�� ������");
                }
                previousRightJoystickValue = rightJoystickValue;
            }
        }

        // A ��ư �Է� ó��
        if (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool rightAButton))
        {
            if (rightAButton != previousRightAButton)
            {
                if (rightAButton)
                {
                    Debug.Log("������ A ��ư�� ������");
                }
                else
                {
                    Debug.Log("������ A ��ư�� ��");
                }
                previousRightAButton = rightAButton;
            }
        }

        // B ��ư �Է� ó��
        if (rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool rightBButton))
        {
            if (rightBButton != previousRightBButton)
            {
                if (rightBButton)
                {
                    Debug.Log("������ B ��ư�� ������");
                }
                else
                {
                    Debug.Log("������ B ��ư�� ��");
                }
                previousRightBButton = rightBButton;
            }
        }
    }

    void HandleLeftControllerInput()
    {
        // Grip �Է� ó�� (��ȭ�� ���� ���� �α� ���)
        if (leftController.TryGetFeatureValue(CommonUsages.grip, out leftGripValue))
        {
            if (leftGripValue != previousLeftGripValue)
            {
                if (leftGripValue == 1)
                {
                    Debug.Log("�޼� �׸� ��ư�� ����");
                }
                previousLeftGripValue = leftGripValue;
            }
        }

        // Trigger �Է� ó��
        if (leftController.TryGetFeatureValue(CommonUsages.trigger, out leftTriggerValue))
        {
            if (leftTriggerValue != previousLeftTriggerValue)
            {
                if (leftTriggerValue == 1)
                {
                    Debug.Log("�޼� Ʈ���� ��ư�� ����");
                }
                previousLeftTriggerValue = leftTriggerValue;
            }
        }

        // Joystick �Է� ó��
        if (leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out leftJoystickValue))
        {
            if (leftJoystickValue != previousLeftJoystickValue)
            {
                if (leftJoystickValue != Vector2.zero)
                {
                    Debug.Log("�޼� ���̽�ƽ�� ������");
                }
                previousLeftJoystickValue = leftJoystickValue;
            }
        }

        // X ��ư �Է� ó��
        if (leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool leftXButton))
        {
            if (leftXButton != previousLeftXButton)
            {
                if (leftXButton)
                {
                    Debug.Log("�޼� X ��ư�� ������");
                }
                else
                {
                    Debug.Log("�޼� X ��ư�� ��");
                }
                previousLeftXButton = leftXButton;
            }
        }

        // Y ��ư �Է� ó��
        if (leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool leftYButton))
        {
            if (leftYButton != previousLeftYButton)
            {
                if (leftYButton)
                {
                    Debug.Log("�޼� Y ��ư�� ������");
                }
                else
                {
                    Debug.Log("�޼� Y ��ư�� ��");
                }
                previousLeftYButton = leftYButton;
            }
        }
    }
}
