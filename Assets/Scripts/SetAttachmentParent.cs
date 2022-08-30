using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SetAttachmentParent : MonoBehaviour
{
    public GameObject parent;
    public XRBaseInteractor socket;

    private Transform socketAttachment = null;
    private Transform socketAttachmentOriginalParent = null;

    public void SetParent()
    {
        if (socket.firstInteractableSelected.transform != null)
        {
            socketAttachment = socket.firstInteractableSelected.transform;

            //Debug.Log("Socket Attachment Set!");

            if (socketAttachment.parent != null)
            {
                socketAttachmentOriginalParent = socketAttachment.parent.transform;

                //Debug.Log("Original Parent Set!");
            }

            socketAttachment.parent = parent.transform;

            //Debug.Log("Socket Attachment Parent Set!");
        }
    }

    public void RemoveParent()
    {
        if (socketAttachmentOriginalParent != null)
        {
            socketAttachment.parent = socketAttachmentOriginalParent;

            //Debug.Log("Socket Attachment Original Parent Reverted!");
        }
        else
        {
            //Debug.Log("Socket Attachment Parent Set To Null!");

            socketAttachment.parent = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
