using SoftBlueFund.Exercises;

Number.Print10to25();
Console.WriteLine(Number.SumOfEvenNumbersUntil());



public static class Number
{
    public static void Print10to25()
    {
        for (int i = 10; i < 26; i++)
        {
            Console.WriteLine(i);

        }
    }

    public static int SumOfEvenNumbersUntil(int num = 100)
    {
        int result = 0;
        for (int i = 1; i <= num; i += 2)
        {
            result += i;
        }

        return result;
    }
}