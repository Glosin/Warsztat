using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class DistanceBasedSocket : XRSocketInteractor
{
    public GameObject simulatedTool;
    public SpawnTool spawnPoint;
    
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        var interactable = args.interactableObject.transform.gameObject;

        // Check tag
        if (interactable.CompareTag("Tool"))
        {
            simulatedTool.SetActive(true);
            simulatedTool.GetComponent<Tool>().isCopy = true;
            Destroy(interactable);
            spawnPoint.Spawn();
            socketActive = false;
        }
    }
}
