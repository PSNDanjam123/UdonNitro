
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonNitro
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class TimeManager : UdonSharpBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        float m_timeSpeed = 1.0f;
        [Header("Current")]
        [SerializeField]
        float m_total = 0;
        [SerializeField]
        int m_days = 0;

        [SerializeField]
        int m_hours = 0;

        [SerializeField]
        int m_minutes = 0;

        [SerializeField]
        int m_seconds = 0;

        float m_lastCheck = 0;

        void Start()
        {
            m_total = (int)(Networking.GetServerTimeInSeconds() * m_timeSpeed);
        }

        public int Day
        {
            get => m_days;
        }

        public int Hour
        {
            get => m_hours;
        }

        public int Minute
        {
            get => m_minutes;
        }

        public int Second
        {
            get => m_seconds;
        }

        void FixedUpdate()
        {
            updateTime();
        }

        void updateTime()
        {
            if (m_lastCheck < 0.5f)
            {
                m_lastCheck += Time.fixedDeltaTime;
                return;
            }
            m_total += m_lastCheck * m_timeSpeed;
            m_lastCheck = 0;
            var time = (int)Mathf.Floor(m_total);
            m_seconds = time % 60;
            m_minutes = (int)Mathf.Floor(time / 60) % 60;
            m_hours = (int)Mathf.Floor(time / 60 / 60) % 24;
            m_days = (int)Mathf.Floor(time / 60 / 60 / 24) % 60;
        }
    }
}