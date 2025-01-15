using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel;

public class StartClass
{

    public List<string> alkuaineet;

    public StartClass()
    {
        if (File.Exists("alkuaineet.txt"))
        {
            alkuaineet = new List<string>(File.ReadAllLines("alkuaineet.txt"));
        }
        else
        {
            Console.WriteLine("Tiedostoa alkuaineet.txt ei löydy.");
            alkuaineet = new List<string>();
        }

    }

    public void Start()
    {
        while (true)
        {
            Console.WriteLine("Haluatko pelata (p) tai tarkastella tuloksia (t)?");
            string? input = Console.ReadLine()?.ToLower();

            if (input == "p")
            {
                StartGame();
            }
            else if (input == "t")
            {
                //Console.WriteLine($"Pelisi keskiarvo tulos on:{averige}");
            }
            else
            {
                Console.WriteLine("Virheellinen syöte. Yritä uudelleen.");
            }
        }
    }

    private void StartGame()
    {
        if (alkuaineet.Count < 20)
        {
            Console.WriteLine("Tiedostossa ei tarpeeksi alkuaineita");
            return;
        }

        HashSet<string> userAnswers = new HashSet<string>();
        int correctAnswers = 0;

        Console.WriteLine("Anna viisi eri alkuainetta ensimmäisten 20 alkuaineen joukosta:");

        for (int i = 1; i <= 5; i++)
        {
            Console.Write($"Alkuaine {i}/5: ");
            string? answer = Console.ReadLine()?.Trim().ToLower();

            if (string.IsNullOrEmpty(answer))
            {
                Console.WriteLine("Vastaus ei voi olla tyhjä. Yritä uudelleen.");
                i--;
                continue;
            }

            if (userAnswers.Contains(answer))
            {
                Console.WriteLine("Olet jo syöttänyt tämän nimisen alkuaineen. Yritä uudelleen.");
                i--;
                continue;
            }

            userAnswers.Add(answer);

            if (alkuaineet.Exists(x => x.ToLower() == answer))
            {
                correctAnswers++;
            }
        }

        int wrongAnswers = 5 - correctAnswers;

        Console.WriteLine($"\nPeli päättyi! Oikeita vastauksia: {correctAnswers}, Vääriä vastauksia: {wrongAnswers}\n");

        SaveResult(correctAnswers, wrongAnswers);
    }

    private void SaveResult(int correctAnswers, int wrongAnswers)
    {
        string directoryName = DateTime.Now.ToString("ddMMyyyy");
        string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), directoryName);

        if (!Directory.Exists(directoryPath)) ;
        {
            Directory.CreateDirectory(directoryPath);
        }

        string filePath = Path.Combine(directoryPath, "tulokset.json");

        List<object> results = new List<object>();

        if (File.Exists(filePath))
        {
            string existingData = File.ReadAllText(filePath);
            results = JsonConvert.DeserializeObject<List<object> > (existingData) ?? new List<object>();

        }

        var newResult = new
        {
            Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            correctAnswers = correctAnswers,
            wrongAnswers = wrongAnswers
        };
        results.Add(newResult);

        string json = JsonConvert.SerializeObject(results, Formatting.Indented);
        File.WriteAllText(filePath, json);

        Console.WriteLine($"Tulokset tallennettu tiedostoon");
    }
}
