using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Plank : XRSocketInteractor
{
    public GameObject plankGameObject;
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        var interactable = args.interactableObject.transform.gameObject;
        interactable.GetComponent<Screw>().plank = this;
    }
}
