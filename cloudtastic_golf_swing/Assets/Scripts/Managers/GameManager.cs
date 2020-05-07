using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private IEnumerator sceneSwitcher;

    public static GameManager instance;

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

    /*
     * possible game states/control flags
     * basic            - load mocap data (own or target), back button
     * mocapLoaded1      - edit,play,record,back button, load mocap
     * mocapLoaded2      - edit,play,record,compare,back button, load mocap
     * cameraReady      - back button, load mocap
     * trackingReady    - record, back button, load mocap
     * tracking         - none
     * playing          - pause, playback speed, back button
     * editing          - trimStart, trimEnd, done 
     * 
     * */
}
