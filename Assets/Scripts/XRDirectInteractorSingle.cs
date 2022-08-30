using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class XRDirectInteractorSingle : XRDirectInteractor
{
    public List<XRBaseInteractor> otherInteractors;

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        bool otherInteractorSelecting = false;
        foreach (var interactor in otherInteractors)
        {
            if (interactable.interactorsSelecting.Contains(interactor))
            {
                otherInteractorSelecting = true;
                break;
            }
        }
          
        return !otherInteractorSelecting && base.CanSelect(interactable);
        //return base.CanSelect(interactable);
    }
}
