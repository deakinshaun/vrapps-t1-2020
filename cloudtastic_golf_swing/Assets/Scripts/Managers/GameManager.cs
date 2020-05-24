using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Only ended up using this to switch scenes.
public class GameManager : MonoBehaviour
{
    private IEnumerator sceneSwitcher;
    public static GameManager instance;
    #region ByGeoff
    //By Geoff Newman SID 215291967

    public string webHost = "http://127.0.0.1/sit383/";
    public string webFunctions = "functions.php";
    public string shareLinkAddress = "DATA";
    public string VideoIDAddress = "VID";

    [HideInInspector]
    public string swingData { get; private set; }
    [HideInInspector]
    public bool isUsingSharedData = false;
    #endregion
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        if(instance != null)
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
        sceneSwitcher = sceneSwitch();       
    }

    public void StartChildCoroutine(IEnumerator coroutineMethod)
    {
        StartCoroutine(coroutineMethod);
    }
    public void OnStartClicked()
    {       
        StartCoroutine(sceneSwitcher);
    }
    IEnumerator sceneSwitch()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive);
        yield return load;
        SceneManager.UnloadSceneAsync("MainMenu");
    }
    #region ByGeoff
    //By Geoff Newman SID 215291967

    public void sharedSwing(string data)
    {
        swingData = data;
        StartCoroutine(sceneSwitcher);
        isUsingSharedData = true;
    }
    private void LateUpdate()
    {
        if (SceneManager.GetActiveScene().name == "MainScene" && isUsingSharedData)
        {
            GameObject obj = GameObject.Find("PlayerPoseVisualizer");
            obj.GetComponent<PoseSkeleton>().loadSharedClip(PoseClip.Classification.Player);
            isUsingSharedData = false;
        }
    }
    #endregion

}
