using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntakeSysteemBack.DAL
{
    public class BaseDAL
    {
        public string applicationName = "VooruitzendenIntakeSysteem";

        public string clientId = "926426372487-1fc0vsf9vgk5sli19ubit0f7ik2mqjge.apps.googleusercontent.com";//ConfigurationManager.AppSettings["ClientId"];
        public string clientSecret = "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";//ConfigurationManager.AppSettings["ClientSecret"];
        public string calendarId = "primary";//"https://calendar.google.com/calendar/u/0?cid=bWljaGFlbGRiZWRuYXJla0BnbWFpbC5jb20";//ConfigurationManager.AppSettings["CalendarId"];

        public string authorizationCode = "4/0AWtgzh4yn9UkLE9H7FD0nROogwBxcG9flzVEci28csBjHGXH2xbbxVN9kV5lzC85yu0Wbw";
        public string refreshToken = "1//04C3MdQusrMkhCgYIARAAGAQSNwF-L9Irw03C_lgfYOojePpYCSOSl8mWRj0Jo5xNk-inNFT8AF-WlK1P0Z4GlEilgtlyveOl5kQ";
        public string accessToken = "ya29.a0AVvZVspc7Fa9Yjvqq7MTWm1RapeWLngLG92Z_u5BDLFCGxaaoCzRSLfBnM4a9qDWDStP3Gb7Hgo-m4lB0CSsVRIiLlFuXwshAhjiJtAmQ6qV0W5oPlPsbSqDFGB0Xfh0K5BbpRtEDi3vYz9pShs8mKSPze7qaCgYKASMSARMSFQGbdwaIjQt8ZFvqMRsiWvMyjwJXRw0163";
    }
}
