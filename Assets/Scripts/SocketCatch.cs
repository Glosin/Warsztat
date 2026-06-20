using System;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class SocketCatch : XRSocketInteractor
{
    public Screw screw;
    public GameObject parentObject;
    public GameObject plank;
    public GameObject socketObject; 
    public bool rotationDone = false;
    public Build build;
    public bool isCatchedPlank;
    public string sideName;
    public GameObject collidersBox;

    protected override void Start()
    {
        base.Start();
        build = FindAnyObjectByType<Build>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        screw = args.interactableObject.transform.GetComponent<Screw>();
        if (!screw.parentObject)
        {
            screw.parentObject = build.gameObject;
            build.AddToParent(screw.gameObject);
        }
        if (plank.transform.parent != build.transform)
            build.AddToParent(plank.gameObject);
        
        screw.catchPlank = this;
    }
    
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        var emptySockets = 0;
        foreach (Transform child in plank.transform)
        {
            SocketCatch socket = child.GetComponent<SocketCatch>();
            if (socket && (socket.firstInteractableSelected == null || !socket.isCatchedPlank))
                emptySockets++;
            
            if (emptySockets > 1)
                Destroy(child.gameObject);
        }
        
        screw.catchPlank = null;
        screw.tip.gameObject.SetActive(true);
        GetComponent<BoxCollider>().enabled = true;
    }

    // private void OnCollisionExit(Collision other)
    // {
    //     GetComponent<BoxCollider>().enabled = true;
    //     
    // }

    public void CreateNewSocket()
    {
        var obj = Instantiate(socketObject, plank.transform, false);
        obj.GetComponent<SocketCatch>().plank = plank;
        GetComponent<BoxCollider>().enabled = false;
    }
    
    protected new void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("ScrewTip"))
            return;
        
        other.GetComponent<ScrewTip>().gameObject.SetActive(false);
        if (other.GetComponent<ScrewTip>().screw.hasPlank)
        {
            build.AddToParent(plank.gameObject);
            Destroy(attachTransform.gameObject);
            Vector3 hitPoint = GetComponent<Collider>().ClosestPoint(other.transform.position);
            Vector3 hitNormal = (hitPoint - other.transform.position).normalized;
            Vector3 offset = hitPoint - transform.position;
            plank.transform.position = other.GetComponent<ScrewTip>().screw.anchor.transform.position - offset;
            // plank.gameObject.isStatic = true;
            isCatchedPlank = true;
            var screwGameObject = other.GetComponent<ScrewTip>().screw.gameObject;
            screwGameObject.GetComponent<Collider>().enabled = false;
            Destroy(screwGameObject.GetComponent<Screw>());
            Destroy(screwGameObject.GetComponent<XRGeneralGrabTransformer>());
            Destroy(screwGameObject.GetComponent<Rigidbody>());

            // Quaternion targetRotation = Quaternion.LookRotation(-hitNormal, Vector3.up);
            // plank.transform.rotation = targetRotation;
        }
        else
        {
            ChangePosition(other);
            UpdateAttachPoint();
        }
        // CreateNewSocket();
    }
    
    public void UpdateAttachPoint()
    {
        Vector3 localPos = attachTransform.localPosition;
        var minus = 0f;
        if (screw)
            minus = ((0.1f * screw.screwInPercent));

        if (sideName == "Back")
            localPos.y = -5.7f + minus;
        else
            localPos.y = 5.7f - minus;
            
            
        attachTransform.localPosition = localPos;
    }

    public void ChangeRotation()
    {
        attachTransform.localRotation = sideName switch
        {
            "Front" => Quaternion.Euler(90f, 0f, 0f),
            "Back" => Quaternion.Euler(-90f, 0f, 0f),
            _ => attachTransform.localRotation
        }; 
    }
    
    public void ChangePosition(Collider other)
    {
        GameObject emptyObject = new GameObject("TemporaryPositionFinder");
        emptyObject.transform.SetParent(other.transform);
        emptyObject.transform.localPosition = new Vector3(0, 0, 0);
        attachTransform.transform.position = emptyObject.transform.position;
        ChangeRotation();
    }
}
