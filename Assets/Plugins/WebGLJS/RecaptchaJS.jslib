mergeInto(LibraryManager.library, {

    //設置Recaptcha驗證監聽
    JS_SetupRecaptchaVerifier: function() {
        window.recaptchaVerifier = new firebase.auth.RecaptchaVerifier('recaptcha-button', {
            'size': 'invisible',
            'callback': (response) => {
                console.log('reCAPTCHA solved');
                signInWithPhoneNumber();
            }
        });

        recaptchaVerifier.render().then((widgetId) => {
            window.recaptchaWidgetId = widgetId;
        });



        //window.setupRecaptchaVerifier();
    },

    //開啟Recaptcha小工具
    JS_OpenRecaptchaTool: function() {
        var badge = document.querySelector('.grecaptcha-badge');
        if (badge) {
            badge.style.visibility = 'visible';
        } else {
            console.error("reCAPTCHA badge not found.");
        }
    },

    //關閉Recaptcha小工具
    JS_CloseRecaptchaTool: function() {
        var badge = document.querySelector('.grecaptcha-badge');
        if (badge) {
            badge.style.visibility = 'hidden';
        } else {
            console.error("reCAPTCHA badge not found.");
        }
    },

    //觸發Recaptcha驗證
    JS_TriggerRecaptcha: function(phoneNumber) {
        window.currPhoneNumber = phoneNumber;
        console.log('Phone number set for reCAPTCHA:', phoneNumber);
        document.getElementById('recaptcha-button').click();



        //window.triggerRecaptcha(UTF8ToString(phoneNumber));
    },
});
