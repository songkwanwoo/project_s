using UnityEngine;

public class OutlineController : MonoBehaviour
{
    private Outline outline;  // Quick Outline ������Ʈ�� ������ ����

    void Start()
    {
        // ť�� ������Ʈ�� �ִ� Outline ������Ʈ�� ������
        outline = GetComponent<Outline>();

        if (outline != null)
        {
            // ó������ Outline�� ��Ȱ��ȭ
            outline.enabled = false;
        }
    }

    // Controller�� ť�꿡 ����� �� Outline Ȱ��ȭ
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            if (outline != null)
            {
                outline.enabled = true;  // Outline Ȱ��ȭ
            }
        }
    }

    // Controller�� ť�꿡�� ����� �� Outline ��Ȱ��ȭ
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            if (outline != null)
            {
                outline.enabled = false;  // Outline ��Ȱ��ȭ
            }
        }
    }
}
