
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class DemoCube : UdonSharpBehaviour
{
    Vector3 m_targetPosition;
    bool reverse = false;

    void Start()
    {
        m_targetPosition = transform.position;
    }

    void FixedUpdate()
    {
        var dist = Vector3.Distance(transform.position, m_targetPosition);
        if (dist < 0.1f)
        {
            if (reverse)
            {
                m_targetPosition += Vector3.forward * -10;
            }
            else
            {
                m_targetPosition += Vector3.forward * 10;
            }
            reverse = !reverse;
            return;
        }
        transform.position = Vector3.Lerp(transform.position, m_targetPosition, Time.fixedDeltaTime);
    }
}
