using UnityEngine;

public class SpawnTool : MonoBehaviour
{
    public GameObject spawnPrefab;

    public void Start()
    {
        Spawn();
    }
    public void Spawn()
    {
        Instantiate(spawnPrefab, transform.position, Quaternion.identity);
    }
}
