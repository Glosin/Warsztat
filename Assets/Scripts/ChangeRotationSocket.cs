using UnityEngine;

public class ChangeRotationSocket : MonoBehaviour
{
    public string sideName;
    public SocketCatch socketCatch;

    public void OnTriggerEnter(Collider other)
    {
        socketCatch.sideName = sideName;
    }
}
