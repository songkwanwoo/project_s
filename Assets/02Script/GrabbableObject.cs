using UnityEngine;
using UnityEngine.UI;

public class GrabbableObject : MonoBehaviour
{
    private bool isInRange = false; // 컨트롤러가 오브젝트 가까이에 있는지 확인
    private Transform controller; // 컨트롤러의 위치

    // 아웃라인을 관리하는 컴포넌트 (필수)
    private Outline outline;

    void Start()
    {
        // 오브젝트에 추가한 Outline 컴포넌트를 가져옴
        outline = GetComponent<Outline>();
        outline.enabled = false; // 처음엔 비활성화
    }

    // 컨트롤러가 오브젝트 근처에 들어왔을 때
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller")) // 컨트롤러와 충돌했는지 확인
        {
            isInRange = true;
            controller = other.transform; // 컨트롤러 위치 저장
            outline.enabled = true; // 초록색 아웃라인 활성화
        }
    }

    // 컨트롤러가 오브젝트 근처를 벗어났을 때
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            isInRange = false;
            controller = null;
            outline.enabled = false; // 아웃라인 비활성화
        }
    }

    void Update()
    {
        /**if (isInRange && OVRInput.Get(OVRInput.Button.Grip)) // 그립 버튼을 누를 때
        {
            transform.position = controller.position; // 오브젝트를 컨트롤러에 부착
            transform.rotation = controller.rotation;
        }**/
    }
}
