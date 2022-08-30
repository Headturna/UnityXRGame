using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabManager : MonoBehaviour
{
    public List<XRBaseInteractor> acceptedInteractors;
    public List<XRBaseInteractable> interactables;
    private XRGrabInteractable component;

    public void DisableInteractables()
    {
        bool accepted = false;
        foreach (var interactor in acceptedInteractors)
        {
            if((Object)component.firstInteractorSelecting == interactor)
            {
                accepted = true;
                break;
            }
        }

        if (accepted)
        {
            foreach (var interactable in interactables)
            {
                interactable.enabled = false;
                Debug.Log("Disabled");
            }
        }
    }

    public void EnableInteractables()
    {
        bool accepted = false;
        foreach (var interactor in acceptedInteractors)
        {
            if ((Object)component.firstInteractorSelecting == interactor)
            {
                accepted = true;
                break;
            }
        }

        if (accepted)
        {
            foreach (var interactable in interactables)
            {
                interactable.enabled = true;
                Debug.Log("Enabled");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        component = GetComponent<XRGrabInteractable>();

        DisableInteractables();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
