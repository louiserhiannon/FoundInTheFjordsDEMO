using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using TMPro;

public class SendEmail : MonoBehaviour
{
    private string uri = "https://api.sendgrid.com/v3/mail/send";
    public static SendEmail SE;
    public string templateID;
    //public CanvasGroup aPIPanel;
    //public TMP_Text key;

    //public ChangeScene changeScene;

    private void Awake()
    {
        SE = this;
        //aPIPanel.alpha = 0;
        //aPIPanel.blocksRaycasts = false;
    }


    public IEnumerator PostData_Coroutine(string emailAddress, string templateID) 
    {
        string jsonStr = "{\"personalizations\": [{\"to\": [{\"email\": \"" + emailAddress + "\"}]}],\"from\": {\"email\": \"hello@foundinthefjords.org\",\"name\":\"Found in the Fjords\"},\"template_id\":\"" + templateID + "\"}";
        // Transform string into byte array
        byte[] data = System.Text.Encoding.UTF8.GetBytes(jsonStr);
        Debug.Log(templateID);
        Debug.Log(emailAddress);
        Debug.Log(jsonStr);
        // Load API key from credebtials file, which is not tracked by git. Contact Aya for a copy.
        string sgk = Resources.Load<TextAsset>("key").ToString();
        //DisplayAPIKey(sgk);
        //string sgk = System.IO.File.ReadLines(Application.dataPath + "/key.txt").First(); 
        //Debug.Log(sgk);
        // Using Post method for UnityWebRequest doesn't work well with sending JSON. Use this way instead.
        var www = new UnityWebRequest(uri, "POST");
        www.SetRequestHeader("Content-Type", "application/json");
        // Authorize with API key
        www.SetRequestHeader("Authorization", $"Bearer {sgk}");
        www.uploadHandler = (UploadHandler) new UploadHandlerRaw(data);
        yield return www.SendWebRequest();
        string error = null;
        if ((www.result != UnityWebRequest.Result.Success))
        {
            error = www.error;
            Debug.Log("Oh no! " + error);
        }
        if ((www.result == UnityWebRequest.Result.Success))
        {
            error = www.error;
            Debug.Log("Success!");
        }
        
    }

    //private void DisplayAPIKey(string text)
    //{
    //    key.text = text;
    //    aPIPanel.alpha = 1;
    //}
}
