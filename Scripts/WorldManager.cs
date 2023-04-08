
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonNitro
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]

    public class WorldManager : UdonSharpBehaviour
    {
        [SerializeField]
        TimeManager m_timeManager;

        public TimeManager TimeManager
        {
            get => m_timeManager;
        }

        void Start()
        {
            if (!m_timeManager)
            {
                m_timeManager = GetComponentInChildren<TimeManager>();
            }
        }
    }

}