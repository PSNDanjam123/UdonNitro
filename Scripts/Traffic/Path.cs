
using UdonSharp;
using UnityEngine;
using VRC.Udon;

namespace UdonNitro.Traffic
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class Path : UdonSharpBehaviour
    {
        [SerializeField]
        Node[] m_nodes;

        [SerializeField]
        Node m_firstNode; public Node FirstNode
        {
            set => m_firstNode = value;
            get => m_firstNode;
        }

        [SerializeField]
        Node m_lastNode; public Node LastNode
        {
            set => m_lastNode = value;
            get => m_lastNode;
        }

        public Node[] Nodes
        {
            set => m_nodes = value;
            get => m_nodes;
        }
    }
}