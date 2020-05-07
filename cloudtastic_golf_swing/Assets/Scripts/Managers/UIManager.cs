using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject pauseButton;
    public GameObject stopButton;
    public GameObject recButton;
    public GameObject rawImage;
    public Texture background;
    
    public Text outputText;
    //public CameraManager cameraManager;

    public static UIManager instance;

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
