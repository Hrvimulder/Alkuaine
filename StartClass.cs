using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel;

//Create a startClass and initialize alkuaineet list.
public class StartClass
{

    public List<string> alkuaineet;

    //Create constructor which checks if alkuaineet.txt exists and if it is true, then read file to list alkuaineet. If file does not exist it gives an error message.
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
    //Create Start method to ask what user want to do.
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
    //Create method to start game. It checks that alkuaine list has 20 alkuaine. 
    private void StartGame()
    {
        if (alkuaineet.Count < 20)
        {
            Console.WriteLine("Tiedostossa ei tarpeeksi alkuaineita");
            return;
        }
        //Create a collection to store user answers and variable to store amount of answers.
        HashSet<string> userAnswers = new HashSet<string>();
        int correctAnswers = 0;

        Console.WriteLine("Anna viisi eri alkuainetta ensimmäisten 20 alkuaineen joukosta:");

        for (int i = 1; i <= 5; i++)
        {
            //Print message to start quiz and get userinput. Userinput is trimmed and non-casesensitive.
            Console.Write($"Alkuaine {i}/5: ");
            string? answer = Console.ReadLine()?.Trim().ToLower();
            //Check userinput, cannot be null or empty. If false, give error message.
            if (string.IsNullOrEmpty(answer))
            {
                Console.WriteLine("Vastaus ei voi olla tyhjä. Yritä uudelleen.");
                i--;
                continue;
            }
            //Check userAnswers collection to confirm that same answer is not given twice.
            if (userAnswers.Contains(answer))
            {
                Console.WriteLine("Olet jo syöttänyt tämän nimisen alkuaineen. Yritä uudelleen.");
                i--;
                continue;
            }
            //If userAnswer is not given twice, then add it to userAnswers collection.
            userAnswers.Add(answer);
            //If answer is correct, then add correctAnswer counter +1.
            if (alkuaineet.Exists(x => x.ToLower() == answer))
            {
                correctAnswers++;
            }
        }

        int wrongAnswers = 5 - correctAnswers;

        Console.WriteLine($"\nPeli päättyi! Oikeita vastauksia: {correctAnswers}, Vääriä vastauksia: {wrongAnswers}\n");

        SaveResult(correctAnswers, wrongAnswers);
    }
    //Create method to save results in a file.
    private void SaveResult(int correctAnswers, int wrongAnswers)
    {
        //Save directory name in a variable. Name is created based on this date.
        //Save directory path in a variable. Path is created based on current directory combined with created directory.
        string directoryName = DateTime.Now.ToString("ddMMyyyy");
        string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), directoryName);
        //Check that directory exists, if not create one.
        if (!Directory.Exists(directoryPath)) ;
        {
            Directory.CreateDirectory(directoryPath);
        }
        //Save file path in a variable. Path is based on directory path and tulokset.json.
        string filePath = Path.Combine(directoryPath, "tulokset.json");
        //Create a list to save results.
        List<object> results = new List<object>();
        //Check if file path exists, if true, read file and save it as a string in a variable.
        //Try convert existingData string to List<object>. If conversion fails, then create empty list.
        if (File.Exists(filePath))
        {
            string existingData = File.ReadAllText(filePath);
            results = JsonConvert.DeserializeObject<List<object> > (existingData) ?? new List<object>();

        }
        //Create new object variable to contain Date and answer count.
        var newResult = new
        {
            Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            correctAnswers = correctAnswers,
            wrongAnswers = wrongAnswers
        };
        //Add object to results list to store info above.
        results.Add(newResult);
        //Create new variable to be saved in a file. Convert results list a json text format and add indents
        string json = JsonConvert.SerializeObject(results, Formatting.Indented);
        File.WriteAllText(filePath, json);

        Console.WriteLine($"Tulokset tallennettu tiedostoon");
    }
}
