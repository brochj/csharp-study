
internal class Program
{
    private static void Main(string[] args)
    {
        int[] numbers = { 1, 2, 3, 4, };

        Console.WriteLine(ArrayToString(numbers));

    }
    public static string ArrayToString(int[] array)
    {
        return String.Join('-', array);
    }
}