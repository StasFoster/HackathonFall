using Hackathon.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Newtonsoft.Json;


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
                Test a = new Test
                {
                    Test
                    Answers = 
                };
                string json = JsonSerializer.Serialize(a);
                file.WriteLine(json);
            }

            return View("Perfect", model); // Изменено для возврата представления Perfect с моделью Test
        }
    }
    

    public class Program
    {
        public void Main()
        {
            InitializeJson("users.json");
            InitializeJson("mainUser.json");
            InitializeJson("answers.json");
            InitializeJson("anti_answers.json");
            CreateUsers("users.json");

            var mainUserInformVector = ReadVectorFromFile("main_user_vector.txt");
            var usJsonVector = mainUserInformVector.ToList();
            UserValidation("main_user.json", "use11", "Stas", "Shustov", HashPassword("password11"), usJsonVector, new List<int> { 0 });

            var users = LoadUsers("users.json");
            List<double[]> a = new List<double[]>();
            foreach(var i in users)
            {
                a.Add(i.info_vector.ToArray());
            }

            var maxEuclideanDist = MaxEuclideanDistance(a);

            var similarityProbability = new List<Tuple<int, double>>();

            var mainUser = LoadUsers("main_user.json").First();
            List<double> a2 = new List<double>();
            a2.AddRange(mainUser.info_vector);
            foreach (var user in users)
            {
                List<double> a3 = new List<double>();
                a3.AddRange(user.info_vector);
                similarityProbability.Add(new Tuple<int, double>(
                    user.id,
                    CombineSimilarity(a2.ToArray(), a3.ToArray(), maxEuclideanDist)));
            }

            similarityProbability = similarityProbability.OrderByDescending(x => x.Item2).ToList();

            Console.WriteLine(JsonConvert.SerializeObject(similarityProbability.Skip(similarityProbability.Count - 3)));

            // Добавляем пользователей с наибольшей и наименьшей схожестью
            foreach (var user in users)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (user.id == similarityProbability[i].Item1)
                    {
                        AddUser("answers.json", user.username, user.name, user.surname, user.password, user.info_vector, user.inverse_vector);
                    }
                }

                for (int i = similarityProbability.Count - 3; i < similarityProbability.Count; i++)
                {
                    if (user.id == similarityProbability[i].Item1)
                    {
                        AddUser("anti_answers.json", user.username, user.name, user.surname, user.password, user.info_vector, user.inverse_vector);
                    }
                }
            }
        }

        public static double[] ReadVectorFromFile(string filename)
        {
            return File.ReadAllLines(filename).Select(double.Parse).ToArray();
        }

        public static double[] VectorNorm(double[] vect)
        {
            double lgVec = Math.Sqrt(vect.Sum(x => x * x));
            return lgVec != 0 ? vect.Select(x => x / lgVec).ToArray() : new double[] { 0 };
        }

        public static double CosineSimilarity(double[] normVecMain, double[] normVecSide)
        {
            double dotProduct = normVecMain.Zip(normVecSide, (x, y) => x * y).Sum();
            double lgMain = Math.Sqrt(normVecMain.Sum(x => x * x));
            double lgSide = Math.Sqrt(normVecSide.Sum(x => x * x));
            return (lgMain != 0 && lgSide != 0) ? dotProduct / (lgMain * lgSide) : 0;
        }

        public static double MaxEuclideanDistance(List<double[]> vectors)
        {
            var minVector = vectors.Aggregate((v1, v2) => v1.Zip(v2, Math.Min).ToArray());
            var maxVector = vectors.Aggregate((v1, v2) => v1.Zip(v2, Math.Max).ToArray());
            return Math.Sqrt(maxVector.Zip(minVector, (x, y) => (x - y) * (x - y)).Sum());
        }

        public static double CombineSimilarity(double[] vec1, double[] vec2, double maxEuc)
        {
            double cosSim = CosineSimilarity(vec1, vec2);
            double eucDist = Math.Sqrt(vec1.Zip(vec2, (x, y) => (x - y) * (x - y)).Sum());
            return cosSim - 0.5 * eucDist / maxEuc;
        }
        public static void InitializeJson(string filename)
        {
            if (!File.Exists(filename))
            {
                File.WriteAllText(filename, JsonConvert.SerializeObject(new List<User>(), Newtonsoft.Json.Formatting.Indented));
            }
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
            }
        }

        public static void AddUser(string filename, string username, string name, string surname,
                                    string password, List<double> informVector,
                                    List<int> inverseVector)
        {
            var users = LoadUsers(filename);
            int userId = users.Count + 1;

            var newUser = new User
            {
                id = userId,
                username = username,
                name = name,
                surname = surname,
                password = password,
                info_vector = informVector,
                inverse_vector = inverseVector
            };

            users.Add(newUser);

            File.WriteAllText(filename, JsonConvert.SerializeObject(users, Newtonsoft.Json.Formatting.Indented));
        }

        public static void UserValidation(string filename, string username,
                                           string name, string surname,
                                           string password,
                                           List<double> informVector,
                                           List<int> inverseVector)
        {
            var users = LoadUsers(filename);

            if (users.Any(user => user.username == username))
                throw new Exception("Ошибка! Такой логин уже существует!");

            AddUser(filename, username, name, surname, password, informVector, inverseVector);
        }

        public static void CreateUsers(string filename)
        {
            // Создание пользователей аналогично вашему коду на Python
            // Можно добавить пользователей в список и вызвать UserValidation для каждого.
            // Пример:
            var users = new List<User>
       {
           new User
           {
               id = 1,
               username = "user1",
               name = "Alice",
               surname = "Smith",
               password = "0b14d501a594442a01c6859541bcb3e8164d183d32937b851835442f69d5c94e",
               info_vector = new List<double> { 1, 0.5, 0.25, 0.75, 0, 1, 0.25, 0.75, 0.5, 0 ,0.75 ,0.25 ,1 },
               inverse_vector = new List<int> { 1 }
           },
           // Добавьте остальных пользователей аналогично...
       };

            foreach (var user in users)
            {
                UserValidation(filename, user.username, user.name, user.surname, user.password, user.info_vector, user.inverse_vector);
            }
        }

        public static List<User> LoadUsers(string filename)
        {
            return JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(filename));
        }
    }
}



