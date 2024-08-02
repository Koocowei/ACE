using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class FirebaseManager : UnitySingleton<FirebaseManager>
{
    public override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 讀取資料回傳
    /// </summary>
    /// <param name="jsonData"></param>
    public void OnFirebaseDataRead(string jsonData)
    {
        var data = JObject.Parse(jsonData);

        if (data["error"] != null)
        {
            Debug.LogError("Firebase read error: " + data["error"].ToString());
        }
        else
        {
            Debug.Log("Firebase data read: " + data.ToString());
        }
    }
}
