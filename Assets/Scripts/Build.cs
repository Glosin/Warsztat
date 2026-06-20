using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class Build : XRGrabInteractable
{
    public int plankObjects;

    public void AddToParent(GameObject obj)
    {
        obj.transform.SetParent(gameObject.transform);
        
        if (obj.layer == LayerMask.NameToLayer("Plank"))
        {
            plankObjects += 1;
            if (plankObjects >= 2)
            {
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                foreach (Transform child in transform)
                {
                    if (child.gameObject.layer != LayerMask.NameToLayer("Plank"))
                        continue;
                    
                    Destroy(child.GetComponent<XRGrabInteractable>());
                    Destroy(child.GetComponent<XRGeneralGrabTransformer>());
                    Destroy(child.GetComponent<Rigidbody>());
                    if (!colliders.Contains(child.GetComponent<Collider>()))
                        colliders.Add(child.GetComponent<Collider>());
                    enabled = false;
                    enabled = true;
                }
            }
        }
        
    }
}
