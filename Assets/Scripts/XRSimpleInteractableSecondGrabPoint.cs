using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSimpleInteractableSecondGrabPoint : XRSimpleInteractable
{
    public Vector3 offset = new Vector3();
    public float activeDistanceMax = 0f;
}
