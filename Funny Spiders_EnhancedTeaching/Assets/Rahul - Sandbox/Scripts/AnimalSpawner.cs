using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject[] Animals;
    public GameObject spawnedObject;
    private Vector3 randomCoordinate;
    private AnimalMovement animalMovementScript;
    public float offset;
    private int animalIndex = 0;
    private Vector3 origin;
    public bool timer = true;
    private Vector3 range;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        range = transform.localScale / 2.0f;
        SpawnAnimal(animalIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (animalMovementScript.home)
        {
           
                SpawnAnimal(animalIndex);
                  
        }
       
    }


    public void SpawnAnimal(int index)
    {
        Vector3 randomRange = new Vector3(Random.Range(-range.x, range.x), origin.y, Random.Range(-range.z, range.z));
        randomCoordinate = origin + new Vector3(randomRange.x / 2, origin.y + offset, randomRange.z / 2);
        spawnedObject = Instantiate(Animals[index]) as GameObject;
        spawnedObject.transform.position = randomCoordinate;
        animalMovementScript = spawnedObject.GetComponent<AnimalMovement>();
        animalIndex += 1;

        
    }
}
