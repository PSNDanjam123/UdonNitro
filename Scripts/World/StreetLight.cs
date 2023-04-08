
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonNitro.World
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class StreetLight : UdonSharpBehaviour
    {
        WorldManager m_worldManager;
        TimeManager m_timeManager;

        VRCPlayerApi m_localPlayer;

        [Header("Settings")]
        [SerializeField, Range(0, 23)]
        int m_lightsOnFrom = 20;
        [SerializeField, Range(0, 23)]
        int m_lightsOnTo = 6;

        [SerializeField]
        Light m_light;
        [SerializeField]
        MeshRenderer m_lightMeshRenderer;

        [SerializeField]
        float m_refreshRate = 1.0f;

        float m_lastCheck = 0;

        void Start()
        {
            m_localPlayer = Networking.LocalPlayer;
            m_worldManager = GetComponentInParent<WorldManager>();
            m_timeManager = m_worldManager.TimeManager;
        }

        void FixedUpdate()
        {
            if (m_lastCheck < m_refreshRate)
            {
                m_lastCheck += Time.fixedDeltaTime;
            }
            m_lastCheck = 0;

            var enable = false;

            if (Vector3.Distance(m_localPlayer.GetPosition(), gameObject.transform.position) > 50f)
            {
                m_light.enabled = enable;
                return;
            }

            var hour = m_timeManager.Hour;
            enable = (hour >= m_lightsOnFrom || hour <= m_lightsOnTo);
            m_light.enabled = enable;
            m_lightMeshRenderer.materials[1].SetColor("_EmissionColor", enable ? Color.white : Color.black);
        }
    }

}