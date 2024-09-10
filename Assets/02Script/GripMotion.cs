using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GripMotion : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }
    private void OnGrab(SelectEnterEventArgs args)
    {
        // 물체가 잡혔을 때 실행되는 코드
        // 예: 물체를 컨트롤러의 특정 위치로 이동
        Transform attachTransform = args.interactorObject.GetAttachTransform(grabInteractable);
        transform.position = attachTransform.position;
        transform.rotation = attachTransform.rotation;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // 물체가 놓였을 때 실행되는 코드
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    

    

    
}
