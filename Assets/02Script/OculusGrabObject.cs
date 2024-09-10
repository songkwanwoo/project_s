using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class OculusGrabObject : MonoBehaviour
{
    public Transform rightControllerTransform;  // ������ ��Ʈ�ѷ��� Transform
    public Transform leftControllerTransform;   // ���� ��Ʈ�ѷ��� Transform

    private InputDevice rightController;
    private InputDevice leftController;

    private GameObject objectInHandRight;  // ���������� ���� ������Ʈ
    private GameObject objectInHandLeft;   // �޼����� ���� ������Ʈ

    private void Start()
    {
        InitializeControllers();
    }

    void InitializeControllers()
    {
        // ������ ��Ʈ�ѷ� �ʱ�ȭ
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightCharacteristics, devices);
        if (devices.Count > 0) rightController = devices[0];

        // �޼� ��Ʈ�ѷ� �ʱ�ȭ
        devices.Clear();
        InputDeviceCharacteristics leftCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftCharacteristics, devices);
        if (devices.Count > 0) leftController = devices[0];
    }

    void Update()
    {
        if (rightController.isValid)
        {
            HandleRightController();
        }
        if (leftController.isValid)
        {
            HandleLeftController();
        }
    }

    void HandleRightController()
    {
        // �׸� ��ư�� ����
        if (rightController.TryGetFeatureValue(CommonUsages.grip, out float gripValue) && gripValue > 0.5f)
        {
            if (objectInHandRight != null)
            {
                // �׸� ��ư�� ���� ���¶�� ������Ʈ�� ����
                objectInHandRight.transform.SetParent(rightControllerTransform);
            }
        }
        else
        {
            // �׸� ��ư�� ���� ������Ʈ ����
            if (objectInHandRight != null)
            {
                objectInHandRight.transform.SetParent(null);
                objectInHandRight = null;
            }
        }
    }

    void HandleLeftController()
    {
        // ���� �׸� ��ư ����
        if (leftController.TryGetFeatureValue(CommonUsages.grip, out float gripValue) && gripValue > 0.5f)
        {
            if (objectInHandLeft != null)
            {
                // �׸� ��ư�� ���� ���¶�� ������Ʈ�� ����
                objectInHandLeft.transform.SetParent(leftControllerTransform);
            }
        }
        else
        {
            // �׸� ��ư�� ���� ������Ʈ ����
            if (objectInHandLeft != null)
            {
                objectInHandLeft.transform.SetParent(null);
                objectInHandLeft = null;
            }
        }
    }

    // �浹 ������ ���� ���� �� �ִ� ������Ʈ ����
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Object"))
        {
            // ������ ��Ʈ�ѷ��� �浹
            if (other.gameObject != objectInHandRight)
            {
                objectInHandRight = other.gameObject;
            }

            // �޼� ��Ʈ�ѷ��� �浹
            if (other.gameObject != objectInHandLeft)
            {
                objectInHandLeft = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �տ��� ������Ʈ�� �������� ��
        if (other.CompareTag("Object"))
        {
            if (other.gameObject == objectInHandRight)
            {
                objectInHandRight = null;
            }
            if (other.gameObject == objectInHandLeft)
            {
                objectInHandLeft = null;
            }
        }
    }
}
