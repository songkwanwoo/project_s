using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class OculusGrabObject : MonoBehaviour
{
    public Transform rightControllerTransform;  // 오른쪽 컨트롤러의 Transform
    public Transform leftControllerTransform;   // 왼쪽 컨트롤러의 Transform

    private InputDevice rightController;
    private InputDevice leftController;

    private GameObject objectInHandRight;  // 오른손으로 잡은 오브젝트
    private GameObject objectInHandLeft;   // 왼손으로 잡은 오브젝트

    private void Start()
    {
        InitializeControllers();
    }

    void InitializeControllers()
    {
        // 오른손 컨트롤러 초기화
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightCharacteristics, devices);
        if (devices.Count > 0) rightController = devices[0];

        // 왼손 컨트롤러 초기화
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
        // 그립 버튼을 감지
        if (rightController.TryGetFeatureValue(CommonUsages.grip, out float gripValue) && gripValue > 0.5f)
        {
            if (objectInHandRight != null)
            {
                // 그립 버튼을 누른 상태라면 오브젝트를 붙임
                objectInHandRight.transform.SetParent(rightControllerTransform);
            }
        }
        else
        {
            // 그립 버튼을 떼면 오브젝트 놓기
            if (objectInHandRight != null)
            {
                objectInHandRight.transform.SetParent(null);
                objectInHandRight = null;
            }
        }
    }

    void HandleLeftController()
    {
        // 왼쪽 그립 버튼 감지
        if (leftController.TryGetFeatureValue(CommonUsages.grip, out float gripValue) && gripValue > 0.5f)
        {
            if (objectInHandLeft != null)
            {
                // 그립 버튼을 누른 상태라면 오브젝트를 붙임
                objectInHandLeft.transform.SetParent(leftControllerTransform);
            }
        }
        else
        {
            // 그립 버튼을 떼면 오브젝트 놓기
            if (objectInHandLeft != null)
            {
                objectInHandLeft.transform.SetParent(null);
                objectInHandLeft = null;
            }
        }
    }

    // 충돌 감지를 통해 잡을 수 있는 오브젝트 설정
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Object"))
        {
            // 오른손 컨트롤러와 충돌
            if (other.gameObject != objectInHandRight)
            {
                objectInHandRight = other.gameObject;
            }

            // 왼손 컨트롤러와 충돌
            if (other.gameObject != objectInHandLeft)
            {
                objectInHandLeft = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 손에서 오브젝트가 떨어졌을 때
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
