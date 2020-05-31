using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordableObject : MonoBehaviour
{
    private bool isRecording;
    public List<RecordedTransform> record = new List<RecordedTransform>();
    private Rigidbody rb;
    private float time;
    private RecordedTransform start;
    public float speed = 80f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        start = new RecordedTransform(transform.position, transform.rotation, transform.localScale);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRecording)
        {
            record.Add(new RecordedTransform(transform.position, transform.rotation, transform.localScale));
        }
        else
        {
            rb.Sleep();
        }
    }

    public void Record()
    {
        start = new RecordedTransform(transform.position, transform.rotation, transform.localScale);
        if (record.Count > 0)
        {
            record.Clear();
        }

        isRecording = true;
        time = Time.realtimeSinceStartup;
        rb.isKinematic = false;
        rb.velocity = transform.forward * speed / 3.6f;
        rb.WakeUp();
    }

    public void Stop()
    {
        Debug.Log("Recorded " + record.Count + " frames");
        isRecording = false;
        rb.isKinematic = true;
    }

    public void SetTransform(int currentFrame)
    {
        transform.position = record[currentFrame].position;
        transform.rotation = record[currentFrame].rotation;
        transform.localScale = record[currentFrame].scale;
    }

    public void Reset()
    {
        transform.position = start.position;
        transform.rotation = start.rotation;
        transform.localScale = start.scale;
    }
}

public class RecordedTransform
{
    public float time;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public RecordedTransform(Vector3 _position, Quaternion _rotation, Vector3 _scale)
    {
        position = _position;
        rotation = _rotation;
        scale = _scale;
    }
}
