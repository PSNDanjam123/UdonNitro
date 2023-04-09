using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdonNitro.Traffic
{
#if(UNITY_EDITOR)
    [ExecuteInEditMode]
    public class PathEditor : MonoBehaviour
    {
        [SerializeField]
        Path m_path;

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
            var pathSO = new UnityEditor.SerializedObject(m_path);

            m_path = GetComponent<Path>();
            m_path.Nodes = GetComponentsInChildren<Node>();

            var prop = pathSO.FindProperty("m_nodes");

            prop.arraySize = m_path.Nodes.Length;
            for (var i = 0; i < m_path.Nodes.Length; i++)
            {
                prop.GetArrayElementAtIndex(i).objectReferenceValue = m_path.Nodes[i];
            }

            for (var i = 0; i < m_path.Nodes.Length; i++)
            {
                var node = m_path.Nodes[i];
                var first = (i == 0);
                var last = (i == m_path.Nodes.Length - 1);

                var nodeSO = new UnityEditor.SerializedObject(node);

                nodeSO.FindProperty("m_path").objectReferenceValue = m_path;
                nodeSO.FindProperty("m_previous").objectReferenceValue = first ? null : m_path.Nodes[i - 1];
                nodeSO.FindProperty("m_next").objectReferenceValue = last ? null : m_path.Nodes[i + 1];
                nodeSO.ApplyModifiedProperties();

                if (first)
                {
                    m_path.FirstNode = node;
                    pathSO.FindProperty("m_firstNode").objectReferenceValue = node;
                }
                if (last)
                {
                    m_path.LastNode = node;
                    pathSO.FindProperty("m_lastNode").objectReferenceValue = node;
                }
            }

            pathSO.ApplyModifiedProperties();

        }

        void OnDrawGizmos()
        {
            foreach (var node in m_path.Nodes)
            {
                drawNode(node);
                if (node.Next == null)
                {
                    continue;
                }
                drawConnection(node, node.Next, m_nodeConnectionColor);
            }
        }

        private void drawNode(Node node)
        {
            Gizmos.color = m_nodeColor;
            Gizmos.DrawSphere(node.transform.position, 0.02f);
            foreach (var branch in node.Branches)
            {
                drawConnection(node, branch, Color.red);
            }
        }

        private void drawConnection(Node prevNode, Node nextNode, Color color)
        {
            var prev = prevNode.transform.position;
            var next = nextNode.transform.position;

            Gizmos.color = color;

            var forward = (next - prev).normalized;
            var middle = Vector3.Lerp(prev, next, 0.5f);

            // Line
            Gizmos.DrawLine(prev, next);

            // Direction Arrow
            var size = 0.3f;
            var right = Vector3.Cross(forward, Vector3.up).normalized;
            var back = middle - forward * size;
            Gizmos.DrawLine(middle, back + right * size);
            Gizmos.DrawLine(middle, back - right * size);

        }
    }
#endif
}