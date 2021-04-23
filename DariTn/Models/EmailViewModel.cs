using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariTn.Models
{
    public class EmailViewModel
    {
        [JsonConstructor]
        public EmailViewModel()
        {

        }
        
        public EmailViewModel(string sender, string subject, string body)
        {
            Sender = sender;
            Subject = subject;
            Body = body;
        }

        public string Sender { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}