using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerMovement : MonoBehaviour
{
    private float baseSpeed = 0.5f; //�⺻ �ӵ�
    private float speedMultiplier = 2.0f; // ���̽�ƽ �̵��� ���� �ӵ� ����

    private InputDevice leftController; // ���� ��Ʈ�ѷ�
    private Vector2 joystickInput; // ���̽�ƽ �Է� ��

    void Start()
    {
        // ���� ��Ʈ�ѷ��� ã��
        var inputDevices = new List<InputDevice>(); // System.Collections.Generic�� �ʿ�
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
            // ���̽�ƽ �Է� �� �о����
            if (leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out joystickInput))
            {
                // ���̽�ƽ �Է¿� ���� �̵� ���� �� �ӵ� ���
                Vector3 moveDirection = new Vector3(joystickInput.x, 0, joystickInput.y);
                float moveSpeed = baseSpeed + joystickInput.magnitude * speedMultiplier;

                // �̵�
                transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
            }
        }
        else
        {
            Debug.LogWarning("Left controller is not valid. Trying to reconnect...");
            ReconnectController(); // ��Ʈ�ѷ��� ��ȿ���� �ʴٸ� �ٽ� ���� �õ�
        }
    }

    void ReconnectController()
    {
        // ��Ʈ�ѷ��� �ٽ� ã�� ���� �ڵ�
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
