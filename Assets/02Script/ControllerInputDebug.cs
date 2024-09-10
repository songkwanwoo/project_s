using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerInputDebug : MonoBehaviour
{
    private InputDevice leftController;
    private InputDevice rightController;

    void Start()
    {
        // XRNode�� ��ġ�� ������
        var leftHand = new List<InputDevice>();
        var rightHand = new List<InputDevice>();

        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHand);
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHand);

        if (leftHand.Count > 0) leftController = leftHand[0];
        if (rightHand.Count > 0) rightController = rightHand[0];
    }

    void Update()
    {
        if (rightController.isValid)
        {
            // A ��ư (Right Controller)
            if (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
            {
                Debug.Log("A ��ư�� ���Ƚ��ϴ�.");
            }

            // B ��ư (Right Controller)
            if (rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonValue) && secondaryButtonValue)
            {
                Debug.Log("B ��ư�� ���Ƚ��ϴ�.");
            }

            // �׸� ��ư
            if (rightController.TryGetFeatureValue(CommonUsages.gripButton, out bool gripButtonValue) && gripButtonValue)
            {
                Debug.Log("�׸� ��ư�� ���Ƚ��ϴ�.");
            }

            // Ʈ���� ��ư
            if (rightController.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerButtonValue) && triggerButtonValue)
            {
                Debug.Log("Ʈ���� ��ư�� ���Ƚ��ϴ�.");
            }

            // ��ġ�е� / ������ƽ (Right Controller)
            if (rightController.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool touchpadValue) && touchpadValue)
            {
                Debug.Log("���� ��ġ�е尡 ���Ƚ��ϴ�.");
            }
        }

        if (leftController.isValid)
        {
            // X ��ư (Left Controller)
            if (leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
            {
                Debug.Log("X ��ư�� ���Ƚ��ϴ�.");
            }

            // Y ��ư (Left Controller)
            if (leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonValue) && secondaryButtonValue)
            {
                Debug.Log("Y ��ư�� ���Ƚ��ϴ�.");
            }

            // ��ġ�е� / ������ƽ (Left Controller)
            if (leftController.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool touchpadValue) && touchpadValue)
            {
                Debug.Log("���� ��ġ�е尡 ���Ƚ��ϴ�.");
            }
        }
    }
}
