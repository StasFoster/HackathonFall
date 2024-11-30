using Hackathon.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;

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
                using (StreamWriter sw = new StreamWriter("contacts.txt", true))
                {
                    foreach (var c in contacts)
                    {
                        sw.WriteLine($"{c.Name},{c.SurName},{c.Login},{c.Password},{c.Message}");
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
