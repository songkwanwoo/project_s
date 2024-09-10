using UnityEngine;

public class OutlineController : MonoBehaviour
{
    private Outline outline;  // Quick Outline 컴포넌트를 저장할 변수

    void Start()
    {
        // 큐브 오브젝트에 있는 Outline 컴포넌트를 가져옴
        outline = GetComponent<Outline>();

        if (outline != null)
        {
            // 처음에는 Outline을 비활성화
            outline.enabled = false;
        }
    }

    // Controller가 큐브에 닿았을 때 Outline 활성화
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            if (outline != null)
            {
                outline.enabled = true;  // Outline 활성화
            }
        }
    }

    // Controller가 큐브에서 벗어났을 때 Outline 비활성화
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            if (outline != null)
            {
                outline.enabled = false;  // Outline 비활성화
            }
        }
    }
}
