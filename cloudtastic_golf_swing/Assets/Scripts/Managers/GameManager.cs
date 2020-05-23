using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Only ended up using this to switch scenes.
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

   

}
