using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sacks.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
    }
}