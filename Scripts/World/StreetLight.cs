
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonNitro.World
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class StreetLight : UdonSharpBehaviour
    {
        VRCPlayerApi m_localPlayer;

        [SerializeField]
        Light m_light;
        [SerializeField]
        MeshRenderer m_lightMeshRenderer;

        void Start()
        {
            m_localPlayer = Networking.LocalPlayer;
        }

        void FixedUpdate()
        {
            if (Vector3.Distance(m_localPlayer.GetPosition(), gameObject.transform.position) > 50f)
            {
                m_light.enabled = false;
            }
            else
            {
                m_light.enabled = true;
            }
        }
    }

}