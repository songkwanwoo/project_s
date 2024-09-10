using UnityEngine;
using UnityEngine.UI;

public class GrabbableObject : MonoBehaviour
{
    private bool isInRange = false; // ��Ʈ�ѷ��� ������Ʈ �����̿� �ִ��� Ȯ��
    private Transform controller; // ��Ʈ�ѷ��� ��ġ

    // �ƿ������� �����ϴ� ������Ʈ (�ʼ�)
    private Outline outline;

    void Start()
    {
        // ������Ʈ�� �߰��� Outline ������Ʈ�� ������
        outline = GetComponent<Outline>();
        outline.enabled = false; // ó���� ��Ȱ��ȭ
    }

    // ��Ʈ�ѷ��� ������Ʈ ��ó�� ������ ��
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller")) // ��Ʈ�ѷ��� �浹�ߴ��� Ȯ��
        {
            isInRange = true;
            controller = other.transform; // ��Ʈ�ѷ� ��ġ ����
            outline.enabled = true; // �ʷϻ� �ƿ����� Ȱ��ȭ
        }
    }

    // ��Ʈ�ѷ��� ������Ʈ ��ó�� ����� ��
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            isInRange = false;
            controller = null;
            outline.enabled = false; // �ƿ����� ��Ȱ��ȭ
        }
    }

    void Update()
    {
        /**if (isInRange && OVRInput.Get(OVRInput.Button.Grip)) // �׸� ��ư�� ���� ��
        {
            transform.position = controller.position; // ������Ʈ�� ��Ʈ�ѷ��� ����
            transform.rotation = controller.rotation;
        }**/
    }
}
