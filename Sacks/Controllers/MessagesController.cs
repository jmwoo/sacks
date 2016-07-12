using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sacks.Models;
using System.IO;
using Newtonsoft.Json;

namespace Sacks.Controllers
{
    public class MessagesController : ApiController
    {
        private readonly string DB_PATH = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/messages.json");

        // GET: api/Messages
        public IEnumerable<Message> Get()
        {
            return GetAllMessages();
        }

        // GET: api/Messages/5
        public Message Get(int id)
        {
            var messages = GetAllMessages();

            var message = messages.FirstOrDefault(m => m.Id == id);

            if (message != null)
            {
                return message;
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        // POST: api/Messages
        public void Post([FromBody]Message message)
        {
            var messages = GetAllMessages();
            int id = 1;
            if (messages.Count > 0)
            {
                id = messages.Max(m => m.Id) + 1;
            }

            message.Id = id;
            messages.Add(message);
            SaveMessages(messages);
        }

        // PUT: api/Messages/5
        public void Put(int id, [FromBody]Message message)
        {
            var messages = GetAllMessages();

            var match = messages.FirstOrDefault(m => m.Id == id);
            if (match != null)
            {
                match.Body = message.Body;
                match.Author = message.Author;
            }

            SaveMessages(messages);
        }

        // DELETE: api/Messages/5
        public void Delete(int id)
        {
            var messages = GetAllMessages();
            messages = messages.Where(m => m.Id != id).ToList();
            SaveMessages(messages);
        }



        private List<Message> GetAllMessages()
        {
            var fileContents = File.ReadAllText(DB_PATH);
            List<Message> allMessages = JsonConvert.DeserializeObject<List<Message>>(fileContents);
            return allMessages;
        }

        private void SaveMessages(List<Message> messages)
        {

            var str = JsonConvert.SerializeObject(messages);
            File.WriteAllText(DB_PATH, str);

        }
    }
}
