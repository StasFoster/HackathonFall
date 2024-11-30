using Hackathon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hackathon.Controllers
{
    public class ContactController : Controller
    {
        private List<Contact> contacts = new List<Contact>();

        public IActionResult Index()
        {
            return View();
        }
        

        
        [HttpPost]
        public IActionResult Check(Contact contact)
        {
            if (ModelState.IsValid)
            {
                contacts.Add(contact);

                // Запись списка контактов в файл
                using (StreamWriter sw = new StreamWriter("mainUser.json", true))
                {
                    foreach (var c in contacts)
                    {
                        Contact a  = new Contact {
                            Name = c.Name,
                            SurName = c.SurName,
                            Login = c.Login,
                            Password = c.Password,
                            Message = c.Message,
                        };
                        string json = JsonSerializer.Serialize(a);
                        sw.WriteLine(json);
                    }
                }

                return RedirectToAction("Good");
            }

            return View("Index");
        }
        

        public IActionResult Good()
        {
            return View();
        }

        public IActionResult List()
        {
            return View(contacts);
        }
    }
}
