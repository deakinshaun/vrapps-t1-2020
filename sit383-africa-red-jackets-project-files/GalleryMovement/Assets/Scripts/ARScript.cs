using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ARScript : MonoBehaviour
{
    private float originalLatitude;
    private float originalLongitude;
    private float currentLongitude;
    private float currentLatitude;

    public Text distanceText;
    public double distance;

    private bool setOriginalValues = true;

    private Vector3 targetPosition;
    private Vector3 originalPosition;

    private float speed = 0.1f;

    public Material fadeInMaterial;

    public void GetCoordinates()
    {
        // check if user has location service enabled
        if(!Input.location.isEnabledByUser)
        {
            distanceText.text = "Location services not enabled";
        }

        // Start service before querying location
        Input.location.Start(1f, 0.1f);

        if(Input.location.status != LocationServiceStatus.Running)
        {
            //Wait until service initializes
            while (Input.location.status == LocationServiceStatus.Initializing)
            {
                distanceText.text = "Waiting for service to initialise";
            }

            //Restart service if stopped during initialising
            if (Input.location.status == LocationServiceStatus.Stopped)
            {
                distanceText.text = "Location services stopped; restarting";
                Input.location.Start();
            }

            //Connection has failed
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                distanceText.text = "Unable to determine device location";
            }
        }
        else
        {
            //Save player coordinates if none exist
            if(setOriginalValues)
            {
                originalLatitude = Input.location.lastData.latitude;
                originalLongitude = Input.location.lastData.longitude;
                setOriginalValues = false;
            }

            //Update current latitude and longitude every loop
            currentLatitude = Input.location.lastData.latitude;
            currentLongitude = Input.location.lastData.longitude;

            //Calculate distance between current and last coordinate of the player to determine their location in AR space now
            Calc(originalLatitude, originalLongitude, currentLatitude, currentLongitude);

        }
        Input.location.Stop();
    }

    //calculates distance between two sets of coordinates, taking into account the curvature of the earth.
    //Function to convert GPS from matthewh8, found at https://www.instructables.com/id/How-to-Markerless-GPS-Based-Augmented-Reality/
    public void Calc(float lat1, float lon1, float lat2, float lon2)
    {
        var R = 6378.137; // Radius of earth in KM
        var dLat = lat2 * Mathf.PI / 180 - lat1 * Mathf.PI / 180;
        var dLon = lon2 * Mathf.PI / 180 - lon1 * Mathf.PI / 180;
        float a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) + Mathf.Cos(lat1 * Mathf.PI / 180) * Mathf.Cos(lat2 * Mathf.PI / 180) * Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2);
        var c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        distance = R * c;
        distance = distance * 1000f; // meters

        //set the distance text on the canvas
        distanceText.text = "Distance: " + distance;

        //convert distance from double to float
        float distanceFloat = (float)distance;

        //set the target position of the targetObject, this is where we lerp to in the update function
        targetPosition = originalPosition - new Vector3(0, 0, distanceFloat * 12); //
    }

    /*IEnumerator fadeInGallery()
    {
        //get every object under the parent object, that we want to fade in OR SET THE MATERIAL TO ALL OBJECTS WITHIN THE GALLERY WALLS, THEN SET THE OPAACITY TO 0 AND SLOWLY RAISE IT
        //Set them all to invisible at first
        //While a number, the time in seconds the fade goes on for, is greater than 0, the objects continue fading in bit by bit
        //Once it hits 0, the loop ends and the coroutine cuts off
        var delay = 1.0f;
        var fadeSpeed = 1.0f;

        var material = fadeInMaterial;
 
        //GetComponent<Renderer>().material.color.a = 0.0f;
 
        //yield new WaitForSeconds(initialDelay);
 
        while (GetComponent<Renderer>().material.color.a < 1.0)
        {
            yield return new WaitForSeconds(delay);

            Color color = GetComponent<Renderer>().material.color;
            color.a = 1.0f;
            GetComponent<Renderer>().material.color = Color.Lerp(GetComponent<Renderer>().material.color, color, fadeSpeed * Time.deltaTime);
   
            //yield break;
        }
    }*/

    void Start()
    {
        //start fadeInGallery() coroutine
        //StartCoroutine("fadeInGallery");

        //initialize target and original position
        targetPosition = transform.position;
        originalPosition = transform.position;

    }

    void Update()
    {
        //Update coordinates 
        GetCoordinates();

        //Transform targetObject's position to reflect user's movement in AR space
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed);
    }
}
