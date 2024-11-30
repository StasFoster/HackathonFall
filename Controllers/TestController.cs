using Hackathon.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace Hackathon.Controllers
{
    public class TestController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save(Test model)
        {
            // Получение ответов
            var answers = model.Answers;

            // Запись ответов в файл
            using (var file = new StreamWriter("contacts.txt", true))
            {
                file.WriteLine(string.Join("|", answers));
            }

            return View("Perfect", model); // Изменено для возврата представления Perfect с моделью Test
        }
    }
}



