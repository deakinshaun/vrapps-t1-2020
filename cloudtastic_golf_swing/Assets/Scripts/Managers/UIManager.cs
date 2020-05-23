using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject playerPlayButton;
    public GameObject playerPauseButton;
    public GameObject playerStopButton;
    public GameObject playerRecButton;
    public GameObject playerSaveButton;
    public GameObject playerLoadButton;
    public GameObject expertPlayButton;
    public GameObject expertPauseButton;
    public GameObject expertStopButton;
    public GameObject expertRecButton;
    public GameObject expertSaveButton;
    public GameObject expertLoadButton;

    public GameObject rawImage;
    public Texture background;
    
    public TextMeshProUGUI outputText;
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
