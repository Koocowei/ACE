mergeInto(LibraryManager.library, {

    // 獲取 reCAPTCHA Token
    JS_GetRecaptchaToken: function(action) {
        const actionStr = UTF8ToString(action);

        grecaptcha.ready(function() {
            const siteKey = '6LdZ8BcqAAAAAF81_Z_JpfbmuvlW5B0lS2hnyzkr';
            grecaptcha.execute(siteKey, { action: actionStr }).then(function(token) {
                window.unityInstance.SendMessage('FirebaseManager', 'ReceiveRecaptchaToken', token);
            }).catch(function(error) {
                console.error("Error getting reCAPTCHA token:", error);
            });
        });
    },

    // 發送 OTP
    JS_SendOTPCode: function(phone) {
        var phoneNumber = UTF8ToString(phone);

        // 清除先前创建的 reCAPTCHA 验证器
        if (window.recaptchaVerifier) {
            window.recaptchaVerifier.clear();
        }

        // 创建新的 reCAPTCHA 验证器
        window.recaptchaVerifier = new firebase.auth.RecaptchaVerifier('recaptcha-container', {
            size: 'invisible', // 或 'normal' 根据你的需求
            callback: (response) => {
                // reCAPTCHA 验证成功的回调
                console.log('reCAPTCHA resolved');
            }
        });

        // 渲染 reCAPTCHA 验证器
        window.recaptchaVerifier.render().then((widgetId) => {
            window.recaptchaWidgetId = widgetId;

            // 记录令牌生成时间
            const tokenGenerationTime = Date.now();
            console.log('Token generation time:', tokenGenerationTime);

            // 使用 Firebase SDK 发送 OTP
            window.recaptchaVerifier.verify().then((token) => {
                console.log('reCAPTCHA token:', token);

                auth.signInWithPhoneNumber(phoneNumber, window.recaptchaVerifier)
                    .then((confirmationResult) => {
                        // 保存 confirmationResult 以便稍后验证 OTP
                        window.confirmationResult = confirmationResult;
                        console.log('OTP sent successfully.' + window.confirmationResult);
                    })
                    .catch((error) => {
                        console.error("Error during sign-in:", error);
                    });
            }).catch((error) => {
                console.error("Error verifying reCAPTCHA token:", error);
            });
        });
    },

    // 驗證 OTP Code
    JS_VerifyOTPCode: function(code, typeStr) {
        var OTPCode = UTF8ToString(code);
        var type = UTF8ToString(typeStr);

        if (window.confirmationResult) {
            window.confirmationResult.confirm(OTPCode)
                .then((result) => {
                    //驗證成功
                    console.log("Verification successful:", result.user);
                    switch(type)
                    {
                        //錢包手機登入
                        case "WalletLogin":
                            window.unityInstance.SendMessage('LoginView', 'OnWalletLoginSuccess');
                            break;

                        //手機註冊
                        case "Register":
                            window.unityInstance.SendMessage('LoginView', 'OnRegisterSuccess');
                            break;
                    }
                })
                .catch((error) => {
                    console.log("Verification Error:", error);
                    OnVerifyError();
                });
        } else {
            OnVerifyError();
        }

        //驗證失敗
        function OnVerifyError(){
            console.log("OTP Code Verification Fail:");
            switch(type)
            {
                //錢包手機登入
                case "WalletLogin":
                    window.unityInstance.SendMessage('LoginView', 'OnWalletLoginOTPCodeError');
                    break;

                //手機註冊
                case "Register":
                    window.unityInstance.SendMessage('LoginView', 'OnRegisterOTPCodeError');
                    break;
            }
        }
    },
});
