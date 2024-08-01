using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/*[System.Serializable]
public class RecaptchaResponse
{
    public bool success;
    public float score;
    public string action;
    public string challenge_ts;
    public string hostname;
}*/

public class FirebaseManager : UnitySingleton<FirebaseManager>
{
    const string API_VerifyreCaptchaUrl = "https://test-project-ovfp.onrender.com/verify-recaptcha";            //reCAPTCHA 驗證API
    const string API_SEND_OTP_URL = "https://test-project-ovfp.onrender.com/send-otp";                          //發送OTP API
    const string API_VERIFY_OTP_URL = "https://test-project-ovfp.onrender.com/verify-otp";                      //驗證OTP API

    public string CurrPhoneNumber { get; set; }                                                                 //當前驗證手機號

    /// <summary>
    /// 發送 OTP
    /// </summary>
    /// <param name="phoneNumber"></param>
    public void SendOTP(string phoneNumber)
    {
        if (!string.IsNullOrEmpty(phoneNumber))
        {
            CurrPhoneNumber = phoneNumber;
            JSBridgeManager.Instance.GetRecaptchaToken();
        }
        else
        {
            Debug.LogError("Phone Number Is Empty!!!");
        }
    }

    /// <summary>
    /// 接收 reCAPTCHA Token
    /// </summary>
    /// <param name="token"></param>
    public void ReceiveRecaptchaToken(string token)
    {        
        if (!string.IsNullOrEmpty(token))
        {
            Debug.Log("Get reCAPTCHA Token Successful.");
            StartCoroutine(ISendVerificationCodeCoroutine(token));
        }
    }

    #region 驗證 reCAPTCHA 驗證碼

    [System.Serializable]
    public class VerifyRecaptchaRequest
    {
        public string token;
    }
    [System.Serializable]
    public class VeriRecaptchaResponse
    {
        public bool success;
        public string token;
        public string[] errors;
    }
    /// <summary>
    /// 驗證 reCAPTCHA 驗證碼
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public IEnumerator IVerifyRecaptchaToken(string token)
    {
        VerifyRecaptchaRequest requestBody = new VerifyRecaptchaRequest
        {
            token = token
        };
        string jsonRequest = JsonUtility.ToJson(requestBody);
        Debug.Log("IVerify Recaptcha Token: " + jsonRequest);

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(API_VerifyreCaptchaUrl, jsonRequest))
        {
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonRequest));
            www.uploadHandler.contentType = "application/json";
            www.downloadHandler = new DownloadHandlerBuffer();

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
                Debug.LogError("Response: " + www.downloadHandler.text);
            }
            else
            {
                string jsonResponse = www.downloadHandler.text;
                VeriRecaptchaResponse response = JsonUtility.FromJson<VeriRecaptchaResponse>(jsonResponse);

                if (response.success)
                {
                    Debug.Log("reCAPTCHA Verification Successful.");
                    JSBridgeManager.Instance.SendOTPCode(CurrPhoneNumber);
                    //yield return ISendVerificationCodeCoroutine(token);
                }
                else
                {
                    Debug.LogError("reCAPTCHA verification failed: " + response.token);
                    if (response.errors != null)
                    {
                        foreach (var error in response.errors)
                        {
                            Debug.LogError("Error: " + error);
                        }
                    }
                }
            }
        }
    }

    #endregion

    #region 發送 OTP

    [System.Serializable]
    public class OTPRequest
    {
        public string phoneNumber;
        public string recaptchaToken;
    }

    [System.Serializable]
    public class OTPResponse
    {
        public bool success;
        public string message;
        public string error;
    }
    /// <summary>
    /// 發送 OTP
    /// </summary>
    /// <param name="token">reCAPTCHA Token</param>
    /// <returns></returns>
    private IEnumerator ISendVerificationCodeCoroutine(string token)
    {
        OTPRequest requestBody = new OTPRequest
        {
            phoneNumber = CurrPhoneNumber,
            recaptchaToken = token
        };
        string jsonRequest = JsonUtility.ToJson(requestBody);
        Debug.Log("Send OTP Code Request: " + jsonRequest);

        using (UnityWebRequest www = new UnityWebRequest(API_SEND_OTP_URL, "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonRequest));
            www.uploadHandler.contentType = "application/json";
            www.downloadHandler = new DownloadHandlerBuffer();

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
                Debug.LogError("Response: " + www.downloadHandler.text);
            }
            else
            {
                string jsonResponse = www.downloadHandler.text;
                OTPResponse response = JsonUtility.FromJson<OTPResponse>(jsonResponse);

                if (response.success)
                {
                    Debug.Log("OTP sent successfully.");
                }
                else
                {
                    Debug.LogError("OTP sending failed: " + response.message);
                    if (response.error != null)
                    {
                        Debug.LogError("Error: " + response.error);
                    }
                }
            }
        }
    }

    #endregion

    /// <summary>
    /// 驗證OTP
    /// </summary>
    /// <param name="code"></param>
    public void VerifyOTP(string code)
    {
        StartCoroutine(IVerifyOTP(code));
    }

    [System.Serializable]
    public class VerifyOTPCodeRequest
    {
        public string otpCode;
    }
    [System.Serializable]
    public class VerifyOTPCodeResponse
    {
        public bool success;
        public string message;
        public string error;
    }
    private IEnumerator IVerifyOTP(string code)
    {
        VerifyOTPCodeRequest requestBody = new VerifyOTPCodeRequest()
        {
            otpCode = code
        };
        string jsonRequest = JsonUtility.ToJson(requestBody);
        Debug.Log("Verify OTP Request: " + jsonRequest);

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(API_VERIFY_OTP_URL, jsonRequest))
        {
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonRequest));
            www.uploadHandler.contentType = "application/json";
            www.downloadHandler = new DownloadHandlerBuffer();

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
                Debug.LogError("Response: " + www.downloadHandler.text);
            }
            else
            {
                string jsonResponse = www.downloadHandler.text;
                VerifyOTPCodeResponse response = JsonUtility.FromJson<VerifyOTPCodeResponse>(jsonResponse);

                if (response.success)
                {
                    Debug.Log("Verify OTP Code Successful.");
                }
                else
                {
                    Debug.LogError("Verify OTP Code failed: " + response.error);
                    if (response.error != null)
                    {
                        foreach (var error in response.error)
                        {
                            Debug.LogError("Error: " + error);
                        }
                    }
                }
            }
        }
    }
}
