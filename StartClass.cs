public class StartClass
{
    int playCount = 0;
    int averige = 0:
    public static void Start()
    {
        while (true)
        {
            Console.WriteLine("Haluatko pelata (p) tai tarkastella tuloksia (t)?");
            string? input = Console.ReadLine().ToLower();
            if (input == "p")
            {
                Console.WriteLine("Pelaa");
            }
            else if (input == "t")
            {
                Console.WriteLine("Tarkastele");
            }
            else
            {
                Console.WriteLine("Virheellinen syöte. Yritä uudelleen.");
            }
        }
    }
}