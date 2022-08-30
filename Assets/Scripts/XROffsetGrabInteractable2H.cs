using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable2H : XROffsetGrabInteractable
{
    [SerializeField] bool snapToSecondInteractor = true;
    [SerializeField] List<XRSimpleInteractable> secondGrabPoints = new List<XRSimpleInteractable>();
    [SerializeField] IXRSelectInteractor secondInteractor;
    [SerializeField] IXRSelectInteractable secondInteractable;
    [SerializeField] bool attachSecondInteractor = false;

    private Quaternion interactorOrgRotation;
    private Quaternion initialRotationOffset;
    private XRSimpleInteractableSecondGrabPoint secondGrabPointComponent;

    private Transform secondHandModel = null;
    private Transform secondHandOrgParent;
    private Vector3 secondHandOrgLocalPos;
    private Quaternion secondHandOrgLocalRot;

    private Quaternion GetTwoHandRotation()
    {
        Vector3 offset = new Vector3(0f, 0f, 0f);
        
        if (secondGrabPointComponent != null)
        {
            offset = secondGrabPointComponent.offset;
        }

        Vector3 forwardVec;
        if(snapToSecondInteractor)
            forwardVec = secondInteractor.transform.position + offset - firstInteractorSelecting.transform.position;
        else
            forwardVec = secondInteractor.transform.position - firstInteractorSelecting.transform.position;

        //float vecLength = Vector3.Distance(secondInteractable.transform.position, firstInteractorSelecting.transform.position);

        return Quaternion.LookRotation(forwardVec, firstInteractorSelecting.transform.up);
    }

    public void OnSecondHandEntered(SelectEnterEventArgs args)
    {
        Debug.Log("Second hand entered!");
        secondInteractor = args.interactorObject;
        secondInteractable = args.interactableObject;
        secondGrabPointComponent = secondInteractable.transform.gameObject.GetComponent<XRSimpleInteractableSecondGrabPoint>();

        initialRotationOffset = Quaternion.Inverse(GetTwoHandRotation()) * firstInteractorSelecting.transform.rotation;

        //Save second hand original valiues if model is to attach to point
        if(attachSecondInteractor)
        {
            secondHandModel = secondInteractor.transform.Find("Model");

            secondHandOrgParent = secondHandModel.transform.parent;
            secondHandOrgLocalPos = secondHandModel.transform.localPosition;
            secondHandOrgLocalRot = secondHandModel.transform.localRotation;

            secondHandModel.transform.parent = secondInteractable.transform;
            //secondHandModel.transform.localPosition = Vector3.zero;
        }
    }

    public void OnSecondHandExited(SelectExitEventArgs args)
    {
        Debug.Log("Second hand exited!");
        secondInteractor = null;
        secondInteractable = null;

        if (secondHandModel != null)
        {
            secondHandModel.transform.parent = secondHandOrgParent.transform;
            secondHandModel.transform.localPosition = secondHandOrgLocalPos;
            secondHandModel.transform.localRotation = secondHandOrgLocalRot;
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        foreach (var sgp in secondGrabPoints)
        {
            sgp.enabled = true;
        }

        //Save original rotation of first selector
        interactorOrgRotation = args.interactorObject.transform.localRotation;

        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        foreach (var sgp in secondGrabPoints)
        {
            sgp.enabled = false;
        }

        secondInteractor = null;
        //Reset  interactor position
        args.interactorObject.transform.localRotation = interactorOrgRotation;

        base.OnSelectExited(args);
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if(secondInteractor!= null && firstInteractorSelecting != null)
        {
            //Debug.Log("Interactor: " + secondInteractor.transform.position + " " + firstInteractorSelecting.transform.position);
            //Debug.Log("Interactable: " + secondInteractable.transform.position + " " + firstInteractorSelecting.transform.position);
            //Vector3 interactorPosition = new Vector3(secondInteractor.transform.position.x, secondInteractor.transform.position.y, secondInteractor.transform.position.z);

            if(snapToSecondInteractor)
                firstInteractorSelecting.transform.rotation = GetTwoHandRotation();
            else
                firstInteractorSelecting.transform.rotation = GetTwoHandRotation() * initialRotationOffset;

            float letGoDistance = 0f;
            if (secondGrabPointComponent != null)
            {
                letGoDistance = secondGrabPointComponent.activeDistanceMax;
            }

            if (letGoDistance > 0 && Vector3.Distance(secondInteractor.transform.position, secondInteractable.transform.position) > letGoDistance)
            {
                interactionManager.CancelInteractableSelection(secondInteractable);
            }

            //Vector3 target = secondInteractor.transform.position - firstInteractorSelecting.transform.position;
            //Quaternion lookRotation = Quaternion.LookRotation(target);
            //
            //Vector3 gripRotation = Vector3.zero;
            //gripRotation.z = firstInteractorSelecting.transform.eulerAngles.z;
            //
            //lookRotation *= Quaternion.Euler(gripRotation);
            //firstInteractorSelecting.transform.rotation = lookRotation;
        }

        base.ProcessInteractable(updatePhase);
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var sgp in secondGrabPoints)
        {
            sgp.selectEntered.AddListener(OnSecondHandEntered);
            sgp.selectExited.AddListener(OnSecondHandExited);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
