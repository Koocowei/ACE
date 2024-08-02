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
    public T OnFirebaseDataRead<T>(string jsonData) where T : class
    {
        var data = JsonConvert.DeserializeObject<T>(jsonData);

        if (data == null)
        {
            Debug.LogError("Firebase read error or data is null.");
            return default;
        }
        else
        {
            Debug.Log("Firebase data read: " + JsonUtility.ToJson(data, true));
            return data;
        }
    }
}

/// <summary>
/// 登入資料
/// </summary>
public class LoginData
{
    public string PhoneNumber;
    public string Password;
}
