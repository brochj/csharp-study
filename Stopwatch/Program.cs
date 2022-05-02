Menu();

static void Menu()
{
    Console.Clear();
    System.Console.WriteLine("S = Segundos");
    System.Console.WriteLine("M = Minuto");
    System.Console.WriteLine("0 = Sair");
    System.Console.WriteLine("Quanto tempo desejar contar?");

    string data = Console.ReadLine().ToLower();
    char type = char.Parse(data.Substring(data.Length - 1, 1));
    int time = int.Parse(data.Substring(0, data.Length - 1));

    int MULTIPLIER = 1;

    if (type == 'm')
        MULTIPLIER = 60;
    if (time == 0) System.Environment.Exit(0);

    Start(time * MULTIPLIER);

    Menu();

}

static void Start(int time)
{
    int currentTime = 0;

    while (currentTime != time)
    {

        Console.Clear();
        currentTime++;
        System.Console.WriteLine(currentTime);
        Thread.Sleep(1000);
    }

    System.Console.Clear();
    System.Console.WriteLine("Stopwatch finalizado");
}