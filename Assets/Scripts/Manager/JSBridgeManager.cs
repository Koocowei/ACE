using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class JSBridgeManager : UnitySingleton<JSBridgeManager>
{
    public override void Awake()
    {
        base.Awake();
    }

    #region Firebase

    [DllImport("__Internal")]
    private static extern bool JS_GetRecaptchaToken(string action);
    /// <summary>
    /// 獲取Recaptcha Token
    /// </summary>
    /// <returns></returns>
    public void GetRecaptchaToken()
    {
        JS_GetRecaptchaToken("send_otp");
    }

    [DllImport("__Internal")]
    private static extern bool JS_SendOTPCode(string phoneNumber);
    /// <summary>
    /// 發送OTP
    /// </summary>
    /// <returns></returns>
    public void SendOTPCode(string phoneNumber)
    {
        JS_SendOTPCode(phoneNumber);
    }

    [DllImport("__Internal")]
    private static extern bool JS_VerifyOTPCode(string code, string typeStr);
    /// <summary>
    /// 驗證OTP Code
    /// </summary>
    /// <param name="code">OTP Code</param>
    /// <param name="typeStr">驗證類型判斷</param>
    public void VerifyOTPCode(string code, string typeStr)
    {
        JS_VerifyOTPCode(code, typeStr);
    }

    #endregion

    #region 錢包

    [DllImport("__Internal")]
    private static extern bool JS_WindowCheckWallet(string walletName);
    /// <summary>
    /// Window檢查錢包擴充是否安裝
    /// </summary>
    /// <param name="wallet"></param>
    /// <returns></returns>
    public bool WindowCheckWallet(WalletEnum wallet)
    {
        return JS_WindowCheckWallet(wallet.ToString());
    }

    [DllImport("__Internal")]
    private static extern bool JS_OpenDownloadWallet(string walletName);
    /// <summary>
    /// 開啟下載錢包分頁
    /// </summary>
    /// <param name="wallet"></param>
    /// <returns></returns>
    public bool OpenDownloadWallet(WalletEnum wallet)
    {
        return JS_OpenDownloadWallet(wallet.ToString());
    }

    #endregion

    #region 工具

    [DllImport("__Internal")]
    private static extern bool JS_Reload();
    /// <summary>
    /// 瀏覽器重新整理
    /// </summary>
    public void Reload()
    {
        JS_Reload();
    }

    [DllImport("__Internal")]
    private static extern string JS_GetBrowserInfo();
    /// <summary>
    /// 獲取瀏覽器訊息
    /// </summary>
    public void GetBrowserInfo()
    {
        JS_GetBrowserInfo();
    }

    [DllImport("__Internal")]
    private static extern void JS_LocationHref(string url);
    /// <summary>
    /// 本地頁面跳轉
    /// </summary>
    /// <param name="url"></param>
    public void LocationHref(string url)
    {
        JS_LocationHref(url);
    }

    [DllImport("__Internal")]
    private static extern void JS_WindowClose();
    /// <summary>
    /// 關閉頁面
    /// </summary>
    public void WindowClose()
    {
        JS_WindowClose();
    }

    [DllImport("__Internal")]
    private static extern void JS_OpenNewBrowser(string mail, string igIdAndName);
    /// <summary>
    /// 開啟新瀏覽器
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="igIdAndName"></param>
    public void OpenNewBrowser(string mail, string igIdAndName)
    {
        JS_OpenNewBrowser(mail, igIdAndName);
    }

    [DllImport("__Internal")]
    private static extern void JS_CopyString(string copyStr);
    /// <summary>
    /// Webgl複製文字
    /// </summary>
    /// <param name="copyStr"></param>
    public void CopyString(string copyStr)
    {
        JS_CopyString(copyStr);
    }

    #endregion
}
