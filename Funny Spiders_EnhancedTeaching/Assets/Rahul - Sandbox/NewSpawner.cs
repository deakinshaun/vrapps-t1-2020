using System.Collections;
using System.Collections.Generic;
using Vuforia;
using UnityEngine;

public class NewSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Corner1Object;
    public GameObject Corner2Object;
    public GameObject Corner3Object;
    public GameObject Corner4Object;
    public GameObject Animal;
    public bool active;
    private Vector3 randomCoordinate;
    public Vector3 scalemult;
    Vector3 corner1;
    Vector3 corner2;
    Vector3 corner3;
    Vector3 corner4;
    void Start()
    {
        Vector3 origin = transform.position;
        Vector3 range = transform.localScale / 2.0f;
        Vector3 randomRange = new Vector3(Random.Range(-range.x, range.x), origin.y,Random.Range(-range.z, range.z));
        randomCoordinate = origin + new Vector3(randomRange.x/2,-range.y, randomRange.z / 2);
        corner1 = new Vector3(origin.x-range.x, origin.y, origin.z-range.z);
        corner2 = new Vector3(origin.x + range.x, origin.y, origin.z - range.z);
        corner3 = new Vector3(origin.x - range.x, origin.y, origin.z + range.z);
        corner4 = new Vector3(origin.x + range.x, origin.y, origin.z + range.z);
        SpawnObjects();
       

    }

    // Update is called once per frame
    void Update()
    {
    }

    public Color GizmosColor = new Color(0.5f, 0.5f, 0.5f, 0.2f);

    void OnDrawGizmos()
    {
        Gizmos.color = GizmosColor;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }

    private void SpawnObjects()
    {
       // scalemult = new Vector3(0.05f, 0.05f, 0.05f);
        HandlePlacement(Corner1Object,corner1,scalemult);
        HandlePlacement(Corner2Object,corner2, scalemult);
        HandlePlacement(Corner3Object,corner3, scalemult);
        HandlePlacement(Corner4Object,corner4, scalemult);
        HandlePlacement(Animal,randomCoordinate, scalemult);
    }

    private void HandlePlacement(GameObject objecttoSpawn,Vector3 place,Vector3 scale)
    {
        GameObject spawnedObject;
        spawnedObject = Instantiate(objecttoSpawn) as GameObject;
        spawnedObject.transform.localScale = scale;
        spawnedObject.transform.position = place;
        spawnedObject.transform.parent = transform;
    }
}