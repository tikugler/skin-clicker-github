using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


public class DatabaseManager : MonoBehaviour
{

    private static string serverAddress = "https://skinclicker.000webhostapp.com";

    public static IEnumerator Register(string username, string password, string email)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", username);
        form.AddField("password", password);
        form.AddField("email", email);
        UnityWebRequest www = UnityWebRequest.Post(serverAddress + "/sqlconnect/register.php", form);
        yield return www.SendWebRequest();
        string result = www.downloadHandler.text;
        www.Dispose();
        yield return result;
    }



    public static IEnumerator LoginPlayer( string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", username);
        form.AddField("password", password);
        UnityWebRequest www = UnityWebRequest.Post(serverAddress + "/sqlconnect/login.php", form);
        yield return www.SendWebRequest();
        string result = www.downloadHandler.text;
        www.Dispose();
        yield return result;
    }


    public static IEnumerator SavePlayerData(string serverAddress, string username, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", username);
        form.AddField("score", score);
        UnityWebRequest www = UnityWebRequest.Post(serverAddress + "/sqlconnect/savedata.php", form);
        yield return www.SendWebRequest();
        string result = www.downloadHandler.text;
        www.Dispose();
        yield return result;

    }

}