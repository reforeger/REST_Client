using System;
using System.IO;
using System.Net;
using System.Text;

namespace REST_Client
{
    public enum httpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    class RestClient
    {
        public string endPoint { get; set; }
        public httpVerb httpMethod { get; set; }

        public RestClient()
        {
            endPoint = "";
            httpMethod = httpVerb.GET;
        }
        public string makeRequest()
        {
            string strResponseValue = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);

            request.Method = httpMethod.ToString();

            using (HttpWebResponse resposne = (HttpWebResponse)request.GetResponse())
            {
                if (resposne.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("error code: " + resposne.StatusCode.ToString());

                }
                //Proecess the resppnse stream... (could be JSON, XML or HTML etc..._

                using (Stream responseStream = resposne.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            strResponseValue = reader.ReadToEnd();
                        }
                    }
                }

            }

                return strResponseValue;
            }
        }
}
