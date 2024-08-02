mergeInto(LibraryManager.library, {

    //驗證OTP 
    //code = OTP code
    //type = 當前驗證類型
    JS_FirebaseVerifyCode: function(code, type) {
        window.confirmationResult.confirm(code).then((result) => {
        console.log("User signed in successfully!!!");
        const user = result.user;

        switch(type)
        {
            //錢包登入
            case "Wallet":
                window.unityInstance.SendMessage("LoginView", "OnWalletLoginSuccess");
                break;

            //手機註冊
            case "Register":
                window.unityInstance.SendMessage("LoginView", "OnRegisterSuccess");
                break;
            
            //忘記密碼
            case "LostPsw":
                window.unityInstance.SendMessage("LoginView", "OnLostPswSuccess");
                break;
        }
        }).catch((error) => {
            console.log("Verify Code Error : " + error);

            switch(type)
            {
                //錢包登入
                case "Wallet":
                    window.unityInstance.SendMessage("LoginView", "OnWalletLoginOTPCodeError");
                    break;

                //手機註冊
                case "Register":
                    window.unityInstance.SendMessage("LoginView", "OnRegisterOTPCodeError");
                    break;
                
                //忘記密碼
                case "LostPsw":
                    window.unityInstance.SendMessage("LoginView", "OnLostPswOTPCodeError");
                    break;
            }
        });




        /*window.verifyCode(UTF8ToString(code),
                          UTF8ToString(type));*/
    },

    //寫入資料
    //refPathPtr = 資料路徑
    //jsonDataPtr = json資料
    JS_WriteDataFromFirebase: function(refPathPtr, jsonDataPtr) {
        var refPath = UTF8ToString(refPathPtr);
        var jsonData = UTF8ToString(jsonDataPtr);
        var data = JSON.parse(jsonData);

        firebase.database().ref(refPath).set(jsonData, (error) => {
            if (error) {
                console.error("The write failed... : " + error);
            } else {
                console.log("Data saved successfully!");
            }
        });




        //window.writeDate(data);
        /*window.writeLoginData(UTF8ToString(phoneNumber),
                              UTF8ToString(password))*/
    },

    //修改與擴充資料
    //refPathPtr = 資料路徑
    //jsonDataPtr = json資料
    JS_UpdateDataFromFirebase: function(refPathPtr, jsonDataPtr) {
        var refPath = UTF8ToString(refPathPtr);
        var jsonData = UTF8ToString(jsonDataPtr);
        var data = JSON.parse(jsonData);

        firebase.database().ref(refPath).update(jsonData, (error) => {
            if (error) {
                console.error("The update failed... : " + error);
            } else {
                console.log("Data updated successfully!");
            }
        });


        //window.updateDate(data);
    },

    //讀取資料
    //refPathPtr = 資料路徑
    //objNamePtr = 回傳物件名
    //callbackFunPtr = 回傳方法名
    JS_ReadDataFromFirebase(refPathPtr, objNamePtr, callbackFunPtr) {
        var refPath = UTF8ToString(refPathPtr);
        var gameObjectName = UTF8ToString(objNamePtr);
        var callbackFunctionName = UTF8ToString(callbackFunPtr);

        firebase.database().ref(refPath).once('value').then(function(snapshot) {
            var data = snapshot.val();
            var jsonData = JSON.stringify(data);
            SendMessage(gameObjectName, callbackFunctionName, jsonData);
            
        }).catch(function(error) {
            console.error("The read failed... : " + error);
            SendMessage(gameObjectName, callbackFunctionName, JSON.stringify({ error: error.message }));
        });
    },
});
