using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ScrewStickToBoard : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private bool isStuck = false;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnRelease(SelectExitEventArgs args)
{
    if (isStuck) return;

    if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 0.1f))
    {
        if (hit.collider.CompareTag("Woodplank"))
        {
            transform.position = hit.point - hit.normal * 0.01f;
            transform.rotation = Quaternion.LookRotation(hit.normal);

            transform.SetParent(hit.collider.transform);

            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;

            grabInteractable.enabled = false;

            Physics.IgnoreCollision(GetComponent<Collider>(), hit.collider);

            isStuck = true;
        }
    }
}


}
