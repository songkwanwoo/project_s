using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerInputDebug : MonoBehaviour
{
    private InputDevice leftController;
    private InputDevice rightController;

    void Start()
    {
        // XRNode의 장치를 가져옴
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
            // A 버튼 (Right Controller)
            if (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
            {
                Debug.Log("A 버튼이 눌렸습니다.");
            }

            // B 버튼 (Right Controller)
            if (rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonValue) && secondaryButtonValue)
            {
                Debug.Log("B 버튼이 눌렸습니다.");
            }

            // 그립 버튼
            if (rightController.TryGetFeatureValue(CommonUsages.gripButton, out bool gripButtonValue) && gripButtonValue)
            {
                Debug.Log("그립 버튼이 눌렸습니다.");
            }

            // 트리거 버튼
            if (rightController.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerButtonValue) && triggerButtonValue)
            {
                Debug.Log("트리거 버튼이 눌렸습니다.");
            }

            // 터치패드 / 엄지스틱 (Right Controller)
            if (rightController.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool touchpadValue) && touchpadValue)
            {
                Debug.Log("우측 터치패드가 눌렸습니다.");
            }
        }

        if (leftController.isValid)
        {
            // X 버튼 (Left Controller)
            if (leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
            {
                Debug.Log("X 버튼이 눌렸습니다.");
            }

            // Y 버튼 (Left Controller)
            if (leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonValue) && secondaryButtonValue)
            {
                Debug.Log("Y 버튼이 눌렸습니다.");
            }

            // 터치패드 / 엄지스틱 (Left Controller)
            if (leftController.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool touchpadValue) && touchpadValue)
            {
                Debug.Log("좌측 터치패드가 눌렸습니다.");
            }
        }
    }
}
