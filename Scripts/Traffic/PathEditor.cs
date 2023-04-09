using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdonNitro.Traffic
{
    [ExecuteInEditMode]
    public class PathEditor : MonoBehaviour
    {
        [SerializeField]
        Path m_road;

        [Header("Settings")]
        [SerializeField]
        Color m_nodeColor = Color.gray;
        [SerializeField]
        Color m_nodeConnectionColor = Color.gray;

        void Update()
        {
            if (!Application.isEditor)
            {
                return;
            }
            m_road = GetComponent<Path>();
            m_road.Nodes = GetComponentsInChildren<Node>();

            for (var i = 0; i < m_road.Nodes.Length; i++)
            {
                var node = m_road.Nodes[i];
                var first = (i == 0);
                var last = (i == m_road.Nodes.Length - 1);

                node.Previous = first ? null : m_road.Nodes[i - 1];
                node.Next = last ? null : m_road.Nodes[i + 1];

                if (first)
                {
                    m_road.FirstNode = node;
                }
                if (last)
                {
                    m_road.LastNode = node;
                }
            }


        }

        void OnDrawGizmos()
        {
            foreach (var node in m_road.Nodes)
            {
                drawNode(node);
                if (node.Next == null)
                {
                    continue;
                }
                drawConnection(node, node.Next);
            }
        }

        private void drawNode(Node node)
        {
            Gizmos.color = m_nodeColor;
            Gizmos.DrawSphere(node.transform.position, 0.02f);
        }

        private void drawConnection(Node prevNode, Node nextNode)
        {
            var prev = prevNode.transform.position;
            var next = nextNode.transform.position;

            Gizmos.color = m_nodeConnectionColor;

            var forward = (next - prev).normalized;
            var middle = Vector3.Lerp(prev, next, 0.5f);

            // Line
            Gizmos.DrawLine(prev, next);

            // Direction Arrow
            var size = 0.1f;
            var right = Vector3.Cross(forward, Vector3.up).normalized;
            var back = middle - forward * size;
            Gizmos.DrawLine(middle, back + right * size);
            Gizmos.DrawLine(middle, back - right * size);

        }
    }
}