using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todolistReactAsp.Connection;
using todolistReactAsp.Models;

namespace todolistReactAsp.Controllers
{
    [Authorize]
    [Route("api/tags")]
    public class TagController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public TagController(ApplicationContext context)
        {
            _context = context;
        }
        public static async Task<string> ALo()
        {
            var defaultApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "key.json")),
            });
            Console.WriteLine(defaultApp.Name); // "[DEFAULT]"
            var message = new Message()
            {
                Data = new Dictionary<string, string>()
                {
                    ["FirstName"] = "John",
                    ["LastName"] = "Doe"
                },
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = "Message Title",
                    Body = "Message Body"
                },

                //Token = "d3aLewjvTNw:APA91bE94LuGCqCSInwVaPuL1RoqWokeSLtwauyK-r0EmkPNeZmGavSG6ZgYQ4GRjp0NgOI1p-OAKORiNPHZe2IQWz5v1c3mwRE5s5WTv6_Pbhh58rY0yGEMQdDNEtPPZ_kJmqN5CaIc",
                Topic = "news"
            };
            var messaging = FirebaseMessaging.DefaultInstance;
            var result = await messaging.SendAsync(message);
            Console.WriteLine(result); //projects/myapp/messages/2492588335721724324
            return result;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            var query = _context.Tag.AsQueryable();
            return Ok(query);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Tag tag)
        {
            await _context.AddAsync(tag);
            await _context.SaveChangesAsync();
            return CreatedAtRoute(new { id = tag.Id }, tag);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var _tag = await _context.Tag.FirstOrDefaultAsync(n => n.Id == id);
            if (_tag != null)
            {
                _context.Tag.Remove(_tag);
                await _context.SaveChangesAsync();
                return Ok("Success");
            }
            else
            {
                return Ok("Error");
            }
        }
    }
}