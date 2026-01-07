using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using System.Linq;


namespace IntakeSysteemBack.DAL
{
    public class WhatsappDAL
    {
        public void whatsappMessageTest()
        {
            const string accountSid = "specify your ssid here";
            const string authToken = "specify auth token here";
            TwilioClient.Init(accountSid, authToken);
            var message = MessageResource.Create(from: new Twilio.Types.PhoneNumber("whatsapp: from number in E.164 format "), body: "Good morning Prasad", to: new Twilio.Types.PhoneNumber("whatsapp:TO number in E.164 format "));
            Console.WriteLine(message.Sid);
        }
    }
}
