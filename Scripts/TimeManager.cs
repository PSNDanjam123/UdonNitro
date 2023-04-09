
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonNitro
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class TimeManager : UdonSharpBehaviour
    {
        [Header("Components")]
        [SerializeField]
        Light m_sunLight;
        [Header("Settings")]
        [SerializeField]
        float m_timeSpeed = 1.0f;
        [SerializeField]
        float m_startTime = 26000;
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
            // might be broken?
            //m_total = (int)(Networking.GetServerTimeInSeconds() * m_timeSpeed) + m_startTime;
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
            if (m_lastCheck < 0.01f)
            {
                m_lastCheck += Time.fixedDeltaTime;
                return;
            }
            updateTime();
            updateSun();
            m_lastCheck = 0;
        }

        void updateTime()
        {
            m_total += m_lastCheck * m_timeSpeed;
            var time = (int)Mathf.Floor(m_total);
            m_seconds = time % 60;
            m_minutes = (int)Mathf.Floor(time / 60) % 60;
            m_hours = (int)Mathf.Floor(time / 60 / 60) % 24;
            m_days = (int)Mathf.Floor(time / 60 / 60 / 24) % 60;
        }

        void updateSun()
        {
            var sunrise = 100;
            var totalMinutes = (m_hours * 60) + m_minutes;
            float angle = -180 + sunrise + (((float)totalMinutes / 1440) * 360);
            m_sunLight.transform.rotation = Quaternion.Euler(angle, 0, 0);

            // sunrise
            if (m_hours == 5)
            {
                m_sunLight.intensity = Mathf.Lerp(m_sunLight.intensity, 1, m_minutes / 60);
            }

            // sunset
            if (m_hours == 18)
            {
                m_sunLight.intensity = Mathf.Lerp(m_sunLight.intensity, 0, m_minutes / 60);
            }

            // day
            if (m_hours > 5 || m_hours < 18)
            {
                m_sunLight.intensity = 1;
            }

            // night
            if (m_hours < 5 || m_hours > 18)
            {
                m_sunLight.intensity = 0;
            }
        }
    }
}