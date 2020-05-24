using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
public class DeepLink : MonoBehaviour
{


    void Start()
    {
        ImaginationOverflow.UniversalDeepLinking.DeepLinkManager.Instance.LinkActivated += Instance_LinkActivated;
    }

    private void Instance_LinkActivated(ImaginationOverflow.UniversalDeepLinking.LinkActivation linkActivation)
    {
        StartCoroutine(GetData(linkActivation.QueryString[GameManager.instance.VideoIDAddress]));

    }
    void OnDestroy()
    {
        ImaginationOverflow.UniversalDeepLinking.DeepLinkManager.Instance.LinkActivated -= Instance_LinkActivated;
    }
    #region ByGeoff
    //By Geoff Newman SID 215291967

    IEnumerator GetData(string id)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(GameManager.instance.webHost + "?"+ GameManager.instance.VideoIDAddress + "=" + id))
        {
            yield return www.SendWebRequest();
            Debug.Log(www.url);

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
              GameManager.instance.sharedSwing(www.downloadHandler.text);
               
            }
        }
    }
    #endregion

}

