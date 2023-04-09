
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonNitro.Traffic
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class Navigator : UdonSharpBehaviour
    {
        [SerializeField]
        Path m_path; public Path Path
        {
            set => m_path = value;
            get => m_path;
        }

        [SerializeField]
        Node m_prevNode; public Node PrevNode
        {
            get => m_prevNode;
        }

        [SerializeField]
        Node m_nextNode; public Node NextNode
        {
            get => m_nextNode;
        }

        [SerializeField]
        Vector3 m_point;

        [SerializeField]
        Vector3 m_nextPoint;

        void FixedUpdate()
        {
            CalculatePoint();
            CalculateNextPoint();
        }

        void CalculatePoint()
        {
            var vector = transform.position - m_prevNode.transform.position;
            var normal = (m_nextNode.transform.position - m_prevNode.transform.position).normalized;
            var proj = Vector3.Project(vector, normal);
            m_point = m_prevNode.transform.position + proj;
        }

        [RecursiveMethod]
        void CalculateNextPoint()
        {
            var remaining = 3.0f;
            var pos = m_point;
            var nextNode = m_nextNode;
            var next = nextNode.transform.position;
            while (true)
            {
                var distance = Vector3.Distance(pos, next);
                var vector = (next - pos).normalized;
                if (distance > remaining)
                {
                    m_nextPoint = pos + (vector * remaining);
                    return;
                }
                else if (nextNode.Next == null)
                {
                    Debug.Log(nextNode.Previous);
                    m_nextPoint = next;
                    return;
                }
                remaining -= distance;
                pos = next;
                nextNode = nextNode.Next;
                next = nextNode.transform.position;
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(m_point, 0.2f);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(m_nextPoint, 0.2f);
        }

    }

}