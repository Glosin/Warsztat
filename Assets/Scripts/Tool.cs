using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Tool : XRGrabInteractable
{
    public Screw screw;
    public bool isCopy;
    private float _startRotation;
    public bool debug = false;
    private bool _isEnded;

    private void Update()
    {
        if (debug)
        {
            debug = false;
            UpdatePlankAttachPoint();
        }
    }
    
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        _startRotation = transform.rotation.z;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        
        if (!isCopy || !screw.catchPlank)
            return;
        screw.UpdateScrew(Mathf.Abs((transform.rotation.z - _startRotation) * 100f));
        if (screw.catchPlank.sideName == "Front")
        {
            var percent = 5.7f - screw.screwInPercent;
            if (percent <= 1.13f)
            {
                percent = 1.13f;
                _isEnded = true;

            }
            screw.catchPlank.attachTransform.localPosition = new Vector3(screw.catchPlank.attachTransform.localPosition.x, percent, screw.catchPlank.attachTransform.localPosition.z);
        }
        else
        {
            var percent = -5.7f + screw.screwInPercent;
            if (percent >= -1.13f)
            {
                percent = -1.13f;
                _isEnded = true;

            }
            screw.catchPlank.attachTransform.localPosition = new Vector3(screw.catchPlank.attachTransform.localPosition.x, percent, screw.catchPlank.attachTransform.localPosition.z);
        }
            
        
        if (screw.screwInPercent >= 3.7f)
        {
            screw.hasPlank = true;
            screw.tip.SetActive(true);
        }
        
        if (_isEnded)
            Destroy(gameObject);

    }

    void UpdatePlankAttachPoint()
    {
        screw.UpdateScrew(0);
        var percent = 2.73f - (4f * screw.screwInPercent);
        
        screw.plank.attachTransform.localPosition = new Vector3(0f, percent, 0f);

        if (screw.screwInPercent >= 0.99f)
            Destroy(gameObject);
    }
}
