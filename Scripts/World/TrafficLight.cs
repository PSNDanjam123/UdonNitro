
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonNitro.World
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class TrafficLight : UdonSharpBehaviour
    {
        [SerializeField, UdonSynced]
        bool m_green = false;

        Animator m_animator;

        void Start()
        {
            m_animator = GetComponent<Animator>();
            UpdateAnimator();
        }

        public bool IsGreen()
        {
            return m_green;
        }

        public void SetGreen()
        {
            SetOwner();
            m_green = true;
            RequestSerialization();
            UpdateAnimator();
        }

        public void SetRed()
        {
            SetOwner();
            m_green = false;
            RequestSerialization();
            UpdateAnimator();
        }

        public override void OnDeserialization()
        {
            UpdateAnimator();
        }

        void UpdateAnimator()
        {
            m_animator.SetBool("Green", m_green);
        }

        bool IsOwner()
        {
            return Networking.IsOwner(Networking.LocalPlayer, gameObject);
        }

        void SetOwner()
        {
            if (IsOwner())
            {
                return;
            }
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
        }

        void OnBecameInvisible()
        {
            Debug.Log("Invisible!");
        }

    }

}