using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerRotation : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public InputDevice rightController; // ������ ��Ʈ�ѷ�
    public float rotationAngle = 90.0f; // �� �� ȸ���� �� ����
    private bool isRotating = false; // ȸ�� ������ Ȯ���ϴ� �÷���

    void Start()
    {
        // ������ ��Ʈ�ѷ��� ã��
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
        // ���̽�ƽ �¿� �Է� �ޱ�
        Vector2 joystickInput;
        if (rightController.isValid)
        {
            if (rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out joystickInput))
            {
                // ���̽�ƽ�� �¿�� ���������� Ȯ�� (����: -0.5 ����, ������: 0.5 �̻�)
                if (joystickInput.x > 0.5f && !isRotating) // ���������� ȸ��
                {
                    RotatePlayer(rotationAngle); // 90�� ȸ��
                    isRotating = true;
                }
                else if (joystickInput.x < -0.5f && !isRotating) // �������� ȸ��
                {
                    RotatePlayer(-rotationAngle); // -90�� ȸ��
                    isRotating = true;
                }

                // ���̽�ƽ�� �߸� ��ġ�� �����ϸ� �ٽ� ȸ�� ���� ���·�
                if (Mathf.Abs(joystickInput.x) < 0.1f)
                {
                    isRotating = false; // ȸ�� ���� ���·� ����
                }
            }
        }
        else
        {
            Debug.LogWarning("Right controller is not valid. Trying to reconnect...");
            ReconnectController(); // ��Ʈ�ѷ��� ��ȿ���� �ʴٸ� �ٽ� ���� �õ�
        }

    }

    // �÷��̾� ȸ�� �Լ�
    void RotatePlayer(float angle)
    {
        player.Rotate(Vector3.up, angle); // y�� �������� �÷��̾� ȸ��
    }

    void ReconnectController()
    {
        // ��Ʈ�ѷ��� �ٽ� ã�� ���� �ڵ�
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
