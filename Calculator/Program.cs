using MyMath = Operations.MathOp;
using MyPrint = Print.Main;

Console.Clear();
Console.WriteLine("Calculadora feita em C# - 02-Maio-2022");
Menu();

void Menu()
{
    MyPrint.PrintMenu();
    ushort mode = GetMode();

    switch (mode)
    {
        case 1: Calculate(MyMath.Sum); break;
        case 2: Calculate(MyMath.Sub); break;
        case 3: Calculate(MyMath.Times); break;
        case 4: Calculate(MyMath.Div); break;
        case 5: System.Environment.Exit(0); break;
        default: Menu(); break;
    }
    Menu();
}



ushort GetMode() => ushort.Parse(Console.ReadLine());

void Calculate(Func<double, double, double> Operacao)
{
    (double value1, double value2) = GetValues();

    double ans = Operacao(value1, value2);

    MyPrint.PrintResult(ans);
    Console.ReadKey();
}

(double, double) GetValues()
{
    Console.WriteLine("Primeiro valor: ");
    double value1 = double.Parse(Console.ReadLine());
    Console.WriteLine("Segundo valor: ");
    double value2 = double.Parse(Console.ReadLine());

    return (value1, value2);
}



