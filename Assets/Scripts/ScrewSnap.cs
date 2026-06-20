using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ScrewSnap : MonoBehaviour
{
    public Transform snapPoint;
    private Transform _tool = null;
    private bool _isSnapped;
    public void OnTriggerEnter(Collider other)
    {
        var grab = other.GetComponent<XRGrabInteractable>();
            if (grab != null && grab.attachTransform != null)
            {
                _tool = grab.attachTransform;
                _isSnapped = true;
            }
        if (other.CompareTag("Tool"))
        {
            _tool = other.transform;
            _isSnapped = true;
        }
    }

    public void Update()
    {
        if (_isSnapped && _tool != null)
        {
            _tool.position = Vector3.Lerp(
                _tool.position,
                snapPoint.position,
                Time.deltaTime * 10
            );
            _tool.rotation = Quaternion.Lerp(
                _tool.rotation,
                snapPoint.rotation,
                Time.deltaTime * 10
            );
        }
    }
}
