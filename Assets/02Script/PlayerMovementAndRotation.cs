using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMovementAndRotation : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    private InputDevice leftController; // ���� ��Ʈ�ѷ�
    private InputDevice rightController; // ������ ��Ʈ�ѷ�
    private Vector2 leftJoystickInput; // ���� ���̽�ƽ �Է� ��
    private Vector2 rightJoystickInput; // ������ ���̽�ƽ �Է� ��
    private bool isRotating = false; // ȸ�� ������ Ȯ���ϴ� �÷���

    public float baseSpeed = 0.5f; // �⺻ �ӵ�
    public float speedMultiplier = 2.0f; // ���̽�ƽ �̵��� ���� �ӵ� ����
    public float rotationAngle = 90.0f; // �� �� ȸ���� �� ����

    void Start()
    {
        // ���� �� ������ ��Ʈ�ѷ��� ã��
        var inputDevices = new List<InputDevice>();

        // ���� ��Ʈ�ѷ�
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

        // ������ ��Ʈ�ѷ�
        inputDevices.Clear(); // ����Ʈ�� �ٽ� ����ϱ� ���� �ʱ�ȭ
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
        // ���� ���̽�ƽ���� �÷��̾� �̵�
        if (leftController.isValid)
        {
            if (leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out leftJoystickInput))
            {
                // ���̽�ƽ �Է¿� ���� �̵� ���� �� �ӵ� ���
                Vector3 moveDirection = new Vector3(leftJoystickInput.x, 0, leftJoystickInput.y);
                float moveSpeed = baseSpeed + leftJoystickInput.magnitude * speedMultiplier;

                // �̵�
                player.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
            }
        }
        else
        {
            Debug.LogWarning("Left controller is not valid. Trying to reconnect...");
            ReconnectController(ref leftController, XRNode.LeftHand);
        }

        // ������ ���̽�ƽ���� �÷��̾� ȸ��
        if (rightController.isValid)
        {
            if (rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out rightJoystickInput))
            {
                // ���̽�ƽ�� �¿�� ���������� Ȯ�� (����: -0.5 ����, ������: 0.5 �̻�)
                if (rightJoystickInput.x > 0.5f && !isRotating) // ���������� ȸ��
                {
                    RotatePlayer(rotationAngle); // 90�� ȸ��
                    isRotating = true;
                }
                else if (rightJoystickInput.x < -0.5f && !isRotating) // �������� ȸ��
                {
                    RotatePlayer(-rotationAngle); // -90�� ȸ��
                    isRotating = true;
                }

                // ���̽�ƽ�� �߸� ��ġ�� �����ϸ� �ٽ� ȸ�� ���� ���·�
                if (Mathf.Abs(rightJoystickInput.x) < 0.1f)
                {
                    isRotating = false; // ȸ�� ���� ���·� ����
                }
            }
        }
        else
        {
            Debug.LogWarning("Right controller is not valid. Trying to reconnect...");
            ReconnectController(ref rightController, XRNode.RightHand);
        }
    }

    // �÷��̾� ȸ�� �Լ�
    void RotatePlayer(float angle)
    {
        player.Rotate(Vector3.up, angle); // y�� �������� �÷��̾� ȸ��
    }

    // ��Ʈ�ѷ� �ٽ� ã��
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
