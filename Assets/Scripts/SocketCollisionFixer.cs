using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketCollisionFixer : MonoBehaviour
{
    private XRSocketInteractor socket;

    void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
        socket.selectEntered.AddListener(OnSocketed);
        socket.selectExited.AddListener(OnUnsocketed);
    }

    void OnDestroy()
    {
        socket.selectEntered.RemoveListener(OnSocketed);
        socket.selectExited.RemoveListener(OnUnsocketed);
    }

    void OnSocketed(SelectEnterEventArgs args)
    {
        var screw = args.interactableObject.transform;
        var screwdriver = socket.transform;

        Collider[] screwdriverColliders = screwdriver.GetComponentsInParent<Collider>();
        Collider[] screwColliders = screw.GetComponentsInChildren<Collider>();

        foreach (var a in screwdriverColliders)
            foreach (var b in screwColliders)
                Physics.IgnoreCollision(a, b, true);

        // Optional: make screw kinematic while socketed
        var rb = screw.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;
    }

    void OnUnsocketed(SelectExitEventArgs args)
    {
        var screw = args.interactableObject.transform;
        var screwdriver = socket.transform;

        Collider[] screwdriverColliders = screwdriver.GetComponentsInParent<Collider>();
        Collider[] screwColliders = screw.GetComponentsInChildren<Collider>();

        foreach (var a in screwdriverColliders)
            foreach (var b in screwColliders)
                Physics.IgnoreCollision(a, b, false);

        // Optional: restore dynamics
        var rb = screw.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = false;
    }
}
