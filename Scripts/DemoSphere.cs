
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class DemoSphere : UdonSharpBehaviour
{
    UdonNitro.Traffic.Navigator m_navigator;
    Rigidbody m_rigidBody;

    void Start()
    {
        m_navigator = GetComponentInChildren<UdonNitro.Traffic.Navigator>();
        m_rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        var vector = (m_navigator.NextPoint - transform.position).normalized;
        m_rigidBody.AddForce(-m_rigidBody.velocity);
        m_rigidBody.AddForce(vector * 5.0f);
    }
}
