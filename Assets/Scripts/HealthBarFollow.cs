using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 1.5f, 0); // Position above character

    void LateUpdate()
    {
        if (target)
            transform.position = target.position + offset;
    }
}