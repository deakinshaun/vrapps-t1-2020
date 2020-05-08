using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Corner1Object;
    public GameObject Corner2Object;
    public GameObject Corner3Object;
    public GameObject Corner4Object;
    public GameObject Animal;

    public Vector3 scale;
    private Vector3 randomCoordinate;
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
        // for (int i = 0; i < 3; i++)
        //{
        GameObject spawnedObject;
        spawnedObject= Instantiate(Corner1Object) as GameObject;
        spawnedObject.transform.localScale = scale;
        spawnedObject.transform.position = corner1;
        
        spawnedObject = Instantiate(Corner2Object) as GameObject;
        spawnedObject.transform.localScale = scale;
        spawnedObject.transform.position = corner2;
        spawnedObject = Instantiate(Corner3Object) as GameObject;
        spawnedObject.transform.localScale = scale;
        spawnedObject.transform.position = corner3;
        spawnedObject = Instantiate(Corner4Object) as GameObject;
        spawnedObject.transform.localScale = scale;
        spawnedObject.transform.position = corner4;
        spawnedObject = Instantiate(Animal) as GameObject;
        spawnedObject.transform.localScale = scale;
        spawnedObject.transform.position = randomCoordinate;
        // CurrentAnimal = spawnedObject;
        // }
    }
}