using System;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Gmail;
using Microsoft.IdentityModel.Tokens;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using IntakeSysteemBack.DAL;
using IntakeSysteemBack.Interfaces;
using IntakeSysteemBack.Models;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using System.IO;
using Google.Apis.Util.Store;
using MimeKit;

namespace IntakeSysteemBack.DAL
{
    public class CalendarDAL : BaseDAL, ICalendar
    {
       

       
        public void createCalendarEvent(CalendarEvent EventFromFront)
        {
            
            try
            {
                //Set credentials for google api
                var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets
                    {
                        ClientId = clientId,
                        ClientSecret = clientSecret,
                    },
                        new[] { CalendarService.Scope.Calendar, },
                        "primary",
                        CancellationToken.None).Result;

                var service = new CalendarService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = applicationName,
                });

                // Define parameters of request.
                EventsResource.ListRequest request = service.Events.List(calendarId.ToString());
                request.TimeMin = DateTime.Now;
                request.ShowDeleted = false;
                request.SingleEvents = true;
                request.MaxResults = 10;
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

                // List events.
                Events events = request.Execute();
                if (events.Items != null && events.Items.Count > 0)
                {
                    foreach (var eventItem in events.Items)
                    {
                        string when = eventItem.Start.DateTime.ToString();
                        if (String.IsNullOrEmpty(when))
                        {
                            when = eventItem.Start.Date;
                        }
                    }
                }
                var ev = new Event();
                Google.Apis.Calendar.v3.EventsResource.InsertRequest insert_event = new EventsResource.InsertRequest(service, ev, "primary");


                //Convert string to start date.
                EventDateTime start = new EventDateTime();
                var date = EventFromFront.Date.Split('-');
                var startTime = EventFromFront.startTime.Split(':');      
                var timeToAdd = new DateTime(Convert.ToInt32(date[0]), Convert.ToInt32(date[1]), Convert.ToInt32(date[2]), Convert.ToInt32(startTime[0]), Convert.ToInt32(startTime[1]), 0);
                start.DateTime = timeToAdd;

                //End time is always half an hour after start
                EventDateTime end = new EventDateTime();
                end.DateTime = timeToAdd.AddMinutes(30);

                //Set original sender
                var orgin = new Google.Apis.Calendar.v3.Data.Event.OrganizerData();
                orgin.Email = "michael@vooruitzenden.nl";
                orgin.DisplayName = orgin.Email.Split("@")[0];

                //Map remaining values
                ev.Organizer = orgin;
                ev.Start = start;
                ev.GuestsCanSeeOtherGuests = true;
                insert_event.SendNotifications = true;
                ev.Location = "kantoor";

                //Add attendees, everything after the comma is a new attendee
                var attends = EventFromFront.CC.Split(',');
                var list = new List<EventAttendee>();


                //If attendee is from vooruitzenden they only have their names displayed instead of the whole adress
                foreach (var at in attends)
                {
                    if (IsValidEmail(at))
                    {
                        var atvar = new EventAttendee();

                        if (at.Contains("@vooruitzenden.nl"))
                        {
                            var splitforeach = at.Split('@');
                            atvar.DisplayName = splitforeach[0];
                        }
                        atvar.Email = at;

                        list.Add(atvar);
                    }                
                }
         
                //Map remaining values to event
                ev.Attendees = list;
                ev.End = end;
                ev.ColorId = EventFromFront.Color.ToString();
                ev.Summary = "Afspraakbevesting: " + EventFromFront.Name;


                //make body from drive document
                string[] documentEdits= { EventFromFront.Name, "Elizabeth Lozano", EventFromFront.Date + " om "+ EventFromFront.startTime + " uur","Auto monteur"};
                TemplateDAL tdal = new TemplateDAL();

                ev.Description = tdal.getEditedDocumentStringFromDrive("nothingATM", documentEdits);

                //https://lukeboyle.com/blog/posts/google-calendar-api-color-id <- spiekbrief voor later voor kleuren

                //Create Calender event.
                insert_event.Execute();
            }
            catch (Exception e)
            {

            }
        }

        public bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();
            //Validate if you got an email adress and not a random string
            if (trimmedEmail.EndsWith("."))
            {
                return false; 
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }

}
