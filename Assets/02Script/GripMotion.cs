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
        // ��ü�� ������ �� ����Ǵ� �ڵ�
        // ��: ��ü�� ��Ʈ�ѷ��� Ư�� ��ġ�� �̵�
        Transform attachTransform = args.interactorObject.GetAttachTransform(grabInteractable);
        transform.position = attachTransform.position;
        transform.rotation = attachTransform.rotation;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // ��ü�� ������ �� ����Ǵ� �ڵ�
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    

    

    
}
