using System.IO;
using System.Collections.Generic;
public class StartClass
{
    int playCount = 0;
    int result = 0;
    int averige = result/playCount;

    List<string> alkuaineet = new List<string>();

    public static void Start()
    {
        while (true)
        {
            Console.WriteLine("Haluatko pelata (p) tai tarkastella tuloksia (t)?");
            string? input = Console.ReadLine().ToLower();
            if (input == "p")
            {
                Console.WriteLine("Pelaa");
                playCount + 1;
            }
            else if (input == "t")
            {
                Console.WriteLine($"Pelisi keskiarvo tulos on:{averige}");
            }
            else
            {
                Console.WriteLine("Virheellinen syöte. Yritä uudelleen.");
            }
        }
    }
}