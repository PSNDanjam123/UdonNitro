
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class DemoButton : UdonSharpBehaviour
{
    [SerializeField]
    UdonNitro.World.TrafficLight trafficLight;

    public override void Interact()
    {
        if (trafficLight.IsGreen())
        {
            trafficLight.SetRed();
        }
        else
        {
            trafficLight.SetGreen();
        }
    }
}
