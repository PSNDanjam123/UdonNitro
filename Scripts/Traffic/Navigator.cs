
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
        Vector3 m_nextPoint; public Vector3 NextPoint
        {
            get => m_nextPoint;
        }

        void FixedUpdate()
        {
            CalculatePoint();
            CalculateNextPoint();
            CalculateNextNode();
        }

        void CalculateNextNode()
        {
            var distance = Vector3.Distance(m_point, m_nextNode.transform.position);
            if (distance > 3.0f)
            {
                return;
            }
            if (m_nextNode.Next != null && Random.Range(0, 2) == 1)
            {
                m_prevNode = m_nextNode;
                m_nextNode = m_nextNode.Next;
                return;
            }
            if (m_nextNode.Branches.Length == 0)
            {
                return; // nothing to do?
            }
            var branches = m_nextNode.Branches;
            var index = Random.Range(0, m_nextNode.Branches.Length);
            m_prevNode = m_nextNode;
            m_nextNode = branches[index];
            m_path = NextNode.Path;
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
            var remaining = 5.0f;
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
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(m_nextPoint, 0.2f);
        }

    }

}