using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable : XRGrabInteractable
{
    public bool setVelocityTrackingParent = false;
    public Transform originalParent;
    public bool selectDisabled = false;
    //public List<XRBaseInteractor> interactorsToDisableFor;
    public int layerOnSelected = -1;
    public int layerOnExited = -1;
    //public List<XRBaseInteractor> interactorsToChangeLayer;
    [SerializeField] Animator disableAnimatorOnSelect;
    [SerializeField] bool setInteractorModelParent = false;
    private Transform interactorModel;
    Transform interactorModelOrgParent;
    Vector3 interactorModelOrgPosition;
    Quaternion interactorModelOrgRotation;
    public Transform secondGrabTransform;
    Vector3 attachOrgPos;
    Quaternion attachOrgRot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override bool IsHoverableBy(IXRHoverInteractor interactor)
    {
        return base.IsHoverableBy(interactor);
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        //bool interactorFound = false;
        //foreach (var itd in interactorsToDisableFor)
        //{
        //    if (interactor.Equals(itd))
        //    {
        //        //Debug.Log("Interactor found" + itd.ToString());
        //        interactorFound = true;
        //        break;
        //    }
        //}

        bool socketInteractorFound = false;
        if (interactor.transform.gameObject.layer == 10)
            socketInteractorFound = true;
        //Debug.Log("Socket!" + " " + interactor.transform.gameObject.layer + " " + interactor.transform.gameObject);

        //bool alreadySelecting = firstInteractorSelecting != null && !firstInteractorSelecting.Equals(interactor) && firstInteractorSelecting.transform.gameObject.layer != 10;

        //return base.IsSelectableBy(interactor) && !(selectDisabled && interactorFound);
        //return base.IsSelectableBy(interactor) && !(selectDisabled && !socketInteractorFound) && !alreadySelecting;
        return base.IsSelectableBy(interactor) && !(selectDisabled && !socketInteractorFound);
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        //Debug.Log("Entering: " + args.interactorObject);

        if (layerOnSelected >= 0)
            args.interactableObject.transform.gameObject.layer = layerOnSelected;

        base.OnSelectEntering(args);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        //Debug.Log("Entered: " + args.interactorObject);

        if (setVelocityTrackingParent && movementType == MovementType.VelocityTracking)
        {
            Transform velocityTrackingParent = args.interactorObject.transform;
            transform.SetParent(velocityTrackingParent);
        }

        if(disableAnimatorOnSelect != null)
        {
            disableAnimatorOnSelect.enabled = false;
        }

        if(setInteractorModelParent)
        {
            interactorModel = args.interactorObject.transform.Find("Model");

            if (interactorModel != null)
            {
                interactorModelOrgParent = interactorModel.transform.parent;
                interactorModelOrgPosition = interactorModel.transform.localPosition;
                interactorModelOrgRotation = interactorModel.transform.localRotation;

                interactorModel.transform.parent = transform;

                if (attachTransform != null)
                {
                    attachOrgPos = attachTransform.localPosition;
                    attachOrgRot = attachTransform.localRotation;

                    attachTransform.localPosition = interactorModel.localPosition;
                    attachTransform.localRotation = interactorModel.localRotation;
                }
            }
        }

        if (originalParent != null)
            transform.SetParent(originalParent);

        base.OnSelectEntered(args);
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        //Debug.Log("Exiting: " + args.interactorObject);

        base.OnSelectExiting(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        //Debug.Log("Exited: " + args.interactorObject);

        bool socketInteracting = false;
        foreach (var interactor in interactorsHovering)
        {
            if (interactor.transform.gameObject.layer == 10)
            {
                socketInteracting = true;
                Debug.Log("Socket found!");
            }
        }

        if (disableAnimatorOnSelect != null)
        {
            disableAnimatorOnSelect.enabled = true;
        }

        if (setInteractorModelParent)
        {
            if (interactorModel != null)
            {
                interactorModel.transform.parent = interactorModelOrgParent.transform;
                interactorModel.transform.localPosition = interactorModelOrgPosition;
                interactorModel.transform.localRotation = interactorModelOrgRotation;

                if (attachTransform != null)
                {
                    attachTransform.localPosition = attachOrgPos;
                    attachTransform.localRotation = attachOrgRot;
                }
            }
        }

        if (layerOnExited >= 0 && !socketInteracting)
            args.interactableObject.transform.gameObject.layer = layerOnExited;

        base.OnSelectExited(args);
    }

    public void DisableAttachmentInteractor()
    {
        //Debug.Log("Disable " + firstInteractableSelected.transform.name);

        selectDisabled = true;
    }

    public void EnableAttachmentInteractor()
    {
        //Debug.Log("Enable " + firstInteractableSelected.transform.name);

        selectDisabled = false;
    }
}
