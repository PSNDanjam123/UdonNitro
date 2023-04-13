
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonNitro
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class CarController : UdonSharpBehaviour
    {
        [SerializeField] Rigidbody m_rigidBody;
        [SerializeField] WheelCollider[] m_wheelColliders;
        [SerializeField] Transform[] m_wheelMeshes;

        [SerializeField] Traffic.Navigator m_navigator;

        [SerializeField] Vector3 m_com;
        [SerializeField] LayerMask m_obstacleLayerMask;

        void Start()
        {
            m_rigidBody.centerOfMass = m_com;
        }

        void FixedUpdate()
        {
            animateWheels();
            moveToPoint();
            obstacleCheck();
        }

        private void obstacleCheck()
        {
            var vector = transform.forward; // (m_navigator.NextPoint - transform.position).normalized;
            var hits = Physics.BoxCastAll(transform.position, new Vector3(0.5f, 1, 1), vector, m_rigidBody.rotation, 5f, m_obstacleLayerMask);
            var brakeTorque = 0.0f;
            foreach (var hit in hits)
            {
                if (hit.rigidbody && hit.rigidbody.GetInstanceID() == m_rigidBody.GetInstanceID())
                {
                    continue;
                }
                brakeTorque = 1000f;
            }

            foreach (var wheel in m_wheelColliders)
            {
                wheel.brakeTorque = brakeTorque;
            }
        }

        private void moveToPoint()
        {
            var position = transform.position;
            var forward = transform.forward;
            var point = m_navigator.NextPoint;
            var vector = (point - position).normalized;
            var angle = Vector3.SignedAngle(forward, vector, transform.up);

            Debug.DrawLine(position, position + forward, Color.magenta);
            Debug.DrawLine(position, position + vector, Color.green);

            var fl = m_wheelColliders[2];
            var fr = m_wheelColliders[3];

            if (angle > 60)
            {
                angle = 60;
            }
            else if (angle < -60)
            {
                angle = -60;
            }

            fl.steerAngle = angle;
            fr.steerAngle = angle;

            foreach (var wheel in m_wheelColliders)
            {
                if (m_rigidBody.velocity.magnitude > 4)
                {
                    wheel.motorTorque = 0f;
                }
                else
                {
                    wheel.motorTorque = 100f;
                }
            }
        }

        private void animateWheels()
        {
            for (var i = 0; i < m_wheelColliders.Length; i++)
            {
                var collider = m_wheelColliders[i];
                var mesh = m_wheelMeshes[i];
                var pos = mesh.position;
                var rot = mesh.rotation;
                collider.GetWorldPose(out pos, out rot);
                mesh.transform.position = pos;
                mesh.transform.rotation = rot;
            }
        }

    }

}