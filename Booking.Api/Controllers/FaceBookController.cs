using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    public class FaceBookController : Controller
    {
        public async Task Index()
        {
            Response.ContentType = "text/html;charset=utf-8";
            await Response.WriteAsync("<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Document</title>\r\n</head>\r\n<body>\r\n    <div>\r\n        <fb:login-button \r\n            scope=\"public_profile,email\"\r\n            onlogin=\"checkLoginState();\">\r\n        </fb:login-button>\r\n        <div id=\"authstatus\"></div>\r\n    </div>\r\n</body>\r\n<script>\r\n    window.fbAsyncInit = function() {\r\n      FB.init({\r\n        appId      : '929592965898660',\r\n        cookie     : true,\r\n        xfbml      : true,\r\n        version    : 'v21.0'\r\n      });\r\n        \r\n      FB.AppEvents.logPageView();   \r\n        \r\n    };\r\n  \r\n    (function(d, s, id){\r\n       var js, fjs = d.getElementsByTagName(s)[0];\r\n       if (d.getElementById(id)) {return;}\r\n       js = d.createElement(s); js.id = id;\r\n       js.src = \"https://connect.facebook.net/en_US/sdk.js\";\r\n       fjs.parentNode.insertBefore(js, fjs);\r\n     }(document, 'script', 'facebook-jssdk'));\r\n\r\n     function checkLoginState(){\r\n        FB.getLoginStatus(function(response) {\r\n        //statusChangeCallback(response);\r\n        $(\"#authstatus\").html(\"<code>\"+ JSON.stringify(response, null, 2)+\"</code>\");\r\n        });\r\n     }\r\n  </script>\r\n</html>\r\n");
        }
    }
}
