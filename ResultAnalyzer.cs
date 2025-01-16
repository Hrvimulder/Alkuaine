using Newtonsoft.Json;
public class ResultAnalyzer
{
    public void CalculateAverageCorrectAnswers()
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string[] directories = Directory.GetDirectories(currentDirectory);

        int totalCorrectAnswers = 0;
        int totalEntries = 0;

        foreach (string directory in directories)
        {
            string jsonFilePath = Path.Combine(directory, "tulokset.json");
            if (File.Exists(jsonFilePath))
            {
                string jsonContent = File.ReadAllText(jsonFilePath);
                List<ResultEntry> results = JsonConvert.DeserializeObject<List<ResultEntry>>(jsonContent) ?? new List<ResultEntry>();

                foreach (var result in results)
                {
                    totalCorrectAnswers += result.CorrectAnswers;
                    totalEntries++;
                }
            }
        }

        if (totalEntries > 0)
        {
            double average = (double)totalCorrectAnswers / totalEntries;
            Console.WriteLine($"Kaikkien tulosten oikein vastattujen kysymysten keskiarvo: {average:F2}");
        }
        else
        {
            Console.WriteLine("Ei tuloksia saatavilla.");
        }
    }
}