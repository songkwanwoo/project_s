using UnityEngine;

public class ObjectResetter : MonoBehaviour
{
    public Transform designatedPosition; // 지정된 위치
    private float resetDelay = 4.0f; // 4초 타이머
    private float allowedDistance = 0.1f; // 허용되는 범위

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool isOutOfBounds = false;

    void Start()
    {
        if (designatedPosition != null)
        {
            initialPosition = designatedPosition.position;
            initialRotation = designatedPosition.rotation;
        }
        else
        {
            Debug.LogError("DesignatedPosition is not assigned!");
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, initialPosition);

        if (distance > allowedDistance)
        {
            if (!isOutOfBounds)
            {
                isOutOfBounds = true;
                Invoke("ResetPosition", resetDelay);
                //Debug.Log("Invoking ResetPosition in " + resetDelay + " seconds");
            }
        }
        else
        {
            if (isOutOfBounds)
            {
                isOutOfBounds = false;
                CancelInvoke("ResetPosition");
            }
        }
    }

    void ResetPosition()
    {
        if (isOutOfBounds)
        {
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            //Debug.Log("ResetPosition executed");
            isOutOfBounds = false;
        }
    }
}
