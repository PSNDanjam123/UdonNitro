
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonNitro
{

    public class WorldManager : UdonSharpBehaviour
    {
        TimeManager m_timeManager;

        public TimeManager TimeManager
        {
            get => m_timeManager;
        }

        void Start()
        {
            m_timeManager = GetComponentInChildren<TimeManager>();
        }
    }

}