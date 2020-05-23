
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalMovement : MonoBehaviour
{
    // Start is called before the first frame update


    public Vector3 boundary;
    private Vector3 Origin;
    private Vector3 Destination;
    public GameObject target;
    public float speed = 0.0f;
    public float Rspeed = 0.0f;
    public float tolerance = 2;
    public bool home = false;
    public string CorrectHab;
    public float initialSpeed;
    public bool axisUp = false;
    private Vector3 newDirection;

    public AudioClip Sound_1;
    public AudioClip Sound_2;
    public AudioSource audioSource;

    public ParticleSystem particals;
    public int waitTime = 0;
    //public Text debugText;

    public bool hasTarget = false;
    private bool timer = false;

    public bool isMoving = true;
    void Start()
    {

        Origin = transform.position;


        Destination = RandomPointInBox(Origin, boundary);

        StartCoroutine(DestinationWait());

    }

    // Update is called once per frame
    void Update()
    {
      //  debugText.text = (" " + timer);


        if (hasTarget)
        {
            MoveTo(target);
        }
        else
        {
            if (timer == true)
            {
                if (Vector3.Distance(transform.position, Destination) < tolerance)
                {

                    Invoke("getNewDestination", waitTime);
                    PlaySounds();
                    timer = false;

                }
            }
            MoveTo(Destination);
        }



    }

    public void MoveTo(GameObject target)
    {
        float step = speed * Time.deltaTime; // calculate distance to move

        Vector3 targetDirection = target.transform.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = Rspeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step

        if (Vector3.Distance(transform.position, target.transform.position) < tolerance)
        {
            isMoving = false;
            hasTarget = false;
        }
        else
        {
            isMoving = true;
        }

        if (isMoving == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
            
            if (!axisUp)
            {
                newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
            else if(axisUp)
            {
                newDirection = Vector3.RotateTowards(-transform.up, targetDirection, singleStep, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
                transform.Rotate(-90, 0, 0);
            }
            
        }

    }
    public void MoveTo(Vector3 destination)
    {

        float step = speed * Time.deltaTime; // calculate distance to move

        Vector3 targetDirection = destination - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = Rspeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, Destination) > tolerance)
        {

            timer = true;
        }



        transform.position = Vector3.MoveTowards(transform.position, destination, step);
        if (!axisUp)
        {
            newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        else if (axisUp)
        {
            newDirection = Vector3.RotateTowards(-transform.up, targetDirection, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
            transform.Rotate(-90, 0, 0);
        }
        


    }
    public void setTarget(GameObject newTarget)
    {
        target = newTarget;
        hasTarget = true;
    }

    private static Vector3 RandomPointInBox(Vector3 center, Vector3 size)
    {

        return center + new Vector3(
           (Random.value - 0.5f) * size.x,
          0,
           (Random.value - 0.5f) * size.z
        );
    }

    public void getNewDestination()
    {
        Destination = RandomPointInBox(Origin, boundary);
    }

    IEnumerator DestinationWait()
    {


        //yield on a new YieldInstruction that waits for 5 seconds.
        while (Vector3.Distance(transform.position, target.transform.position) < tolerance)
        {
            yield return new WaitForSeconds(1);
            getNewDestination();
        }


    }
    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.tag == "Habitat")

        { 
           if(other.gameObject.name == CorrectHab)
               {
               initialSpeed = speed;
               speed = 0;
               home = true;
                particals.gameObject.SetActive(true);
        }
           else
            {
                initialSpeed = speed;
                speed = 0;
                Invoke("ResetSpeed", 3);
            }   

            
      }
    }

    public void ResetSpeed()
    {
        speed = initialSpeed;
    }

    public void PlaySounds()
     {
        float selectorFloat = Random.value;
        if (selectorFloat >= 0.5)
        {
            if ((Sound_1 != null) && (Sound_2 != null))
            {
                selectorFloat = Random.value;
                if (selectorFloat >= 0.5)
                {
                    AudioSource.PlayClipAtPoint(Sound_1, transform.position);
                }
                else
                {
                    AudioSource.PlayClipAtPoint(Sound_2, transform.position);
                }

            }
        }
            

    }
}
