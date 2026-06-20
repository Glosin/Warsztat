using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Screw : XRGrabInteractable
{
    public float screwInPercent;
    public Plank plank;
    public SocketCatch catchPlank;
    public DistanceBasedSocket toolSocket;
    public GameObject parentObject;
    public GameObject anchor;
    public GameObject tip;
    public bool hasPlank;
    public Build build;

    public void Update()
    {
        if (toolSocket == null || catchPlank == null || toolSocket.simulatedTool == null) return;
        if (toolSocket.simulatedTool.gameObject.activeInHierarchy)
            interactionLayers = InteractionLayerMask.GetMask("BlockedScrew"); 
    }


    public void UpdateScrew(float percent)
    {
        
        screwInPercent += percent;
        catchPlank.collidersBox.SetActive(false);


        // if (screwInPercent >= 1f)
        // screwInPercent = 1f;
        // if (catchPlank.catchedObject != null)
        // catchPlank.UpdateAttachPoint();
    }
}
