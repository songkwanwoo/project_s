using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RightControllerTeleport : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public LineRenderer teleportLine; // ����ĳ��Ʈ ������ �׸��� ���� LineRenderer
    public LayerMask teleportMask; // �ڷ���Ʈ ������ ǥ���� �����ϱ� ���� ���̾� ����ũ
    public float maxTeleportDistance = 10f; // �ִ� �ڷ���Ʈ �Ÿ�

    private InputDevice rightController; // ������ ��Ʈ�ѷ�
    private Vector2 joystickInput; // ���̽�ƽ �Է� ��
    private bool isTeleporting = false; // �ڷ���Ʈ ��� Ȱ��ȭ ����
    private Vector3 teleportTarget; // �ڷ���Ʈ ��ǥ ����

    void Start()
    {
        // ������ ��Ʈ�ѷ� ã��
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

        // LineRenderer�� ��Ȱ��ȭ (ó������ �ڷ���Ʈ ��尡 ��������)
        teleportLine.enabled = false;
    }

    void Update()
    {
        if (rightController.isValid)
        {
            // ���̽�ƽ �Է� �ޱ�
            if (rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out joystickInput))
            {
                // ���̽�ƽ�� ������ �о��� �� ����ĳ��Ʈ�� ���� �ڷ���Ʈ ��ġ ǥ��
                if (joystickInput.y > 0.5f && !isTeleporting)
                {
                    isTeleporting = true;
                    teleportLine.enabled = true;
                }

                // ���̽�ƽ�� ������ �� �ڷ���Ʈ
                if (isTeleporting && joystickInput.y < 0.1f)
                {
                    TeleportPlayer();
                }
            }

            // ���̽�ƽ�� Ŭ������ �� �ڷ���Ʈ ���
            bool isClicking;
            if (rightController.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out isClicking) && isClicking)
            {
                CancelTeleport();
            }

            // �ڷ���Ʈ ������ ����ؼ� ������Ʈ
            if (isTeleporting)
            {
                UpdateTeleportLine();
            }
        }
    }

    // ����ĳ��Ʈ�� �ڷ���Ʈ ������ ��ġ ��� �� ���� ������ ������Ʈ
    void UpdateTeleportLine()
    {
        RaycastHit hit;
        Vector3 forwardDirection = transform.forward; // ������ ��Ʈ�ѷ��� �� ����
        Vector3 rayStart = transform.position;

        if (Physics.Raycast(rayStart, forwardDirection, out hit, maxTeleportDistance, teleportMask))
        {
            teleportTarget = hit.point; // �ڷ���Ʈ�� ��ġ ����
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

    // �÷��̾ �ڷ���Ʈ�� ��ġ�� �̵�
    void TeleportPlayer()
    {
        player.position = teleportTarget;
        isTeleporting = false;
        teleportLine.enabled = false;
    }

    // �ڷ���Ʈ ���
    void CancelTeleport()
    {
        isTeleporting = false;
        teleportLine.enabled = false;
    }
}
