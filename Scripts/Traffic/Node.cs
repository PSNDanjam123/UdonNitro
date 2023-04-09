
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonNitro.Traffic
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class Node : UdonSharpBehaviour
    {
        [Header("Node")]
        [SerializeField]
        Path m_path; public Path Path
        {
            set => m_path = value;
            get => m_path;
        }
        [SerializeField]
        Node m_previous; public Node Previous
        {
            set => m_previous = value;
            get => m_previous;
        }

        [SerializeField]
        Node m_next; public Node Next
        {
            set => m_next = value;
            get => m_next;
        }

        [SerializeField]
        Node[] m_branches; public Node[] Branches
        {
            set => m_branches = value;
            get => m_branches;
        }

    }

}