using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace DevConfSkopje.Web.Helpers.ReCaptcha
{
    public static class ReCaptchaHelper
    {
        static string GoogleReCaptchaVerifyUrl = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";

        public static GoogleReCaptchaVerifyResponse VerifyGoogleReCaptcha(string token)
        {


            string url = String.Format(GoogleReCaptchaVerifyUrl, "6Lfja5wUAAAAADNFfzBKaqJD2DYN9NPVhgHnWr3W", token);

            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "GET";

            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            string response = new StreamReader(httpResponse.GetResponseStream()).ReadToEnd();

            return JsonConvert.DeserializeObject<GoogleReCaptchaVerifyResponse>(response);
        }
    }
}

public class GoogleReCaptchaVerifyResponse
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("error-codes")]
    public IEnumerable<string> ErrorCodes { get; set; }
}
