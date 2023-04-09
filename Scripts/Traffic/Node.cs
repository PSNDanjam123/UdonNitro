
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonNitro.Traffic
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class Node : UdonSharpBehaviour
    {
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

    }

}