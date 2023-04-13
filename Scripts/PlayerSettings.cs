
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PlayerSettings : UdonSharpBehaviour
{
    VRCPlayerApi m_player;

    [SerializeField]
    float m_jumpImpulse = 3;

    void Start()
    {
        m_player = Networking.LocalPlayer;
        m_player.SetJumpImpulse(m_jumpImpulse);
    }
}
