namespace Print
{
    class Main
    {
        public static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("Selecione o tipo de operação");
            Console.WriteLine("- (1) Soma");
            Console.WriteLine("- (2) Subtração");
            Console.WriteLine("- (3) Multiplicação");
            Console.WriteLine("- (4) Divisão");
            Console.WriteLine("- (5) Sair");
            Console.WriteLine("-------------------");
        }
        public static void PrintResult(double ans)
        {
            Console.WriteLine("");
            Console.WriteLine($"O valor é: {ans}");
            Console.WriteLine("");
        }
    }

}