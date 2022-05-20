using System.Linq;
using System.Text.RegularExpressions;
using Codewars;

// var kata = new Kata();

// Console.WriteLine(Kata.Bmi(56.3, 123.3));
// Console.WriteLine(Kata.HighAndLow("1 2 -3 4 5"));
// Console.WriteLine(Kata.FindNextSquare(121));
// Console.WriteLine(Kata.FindNextSquare(114));
// Console.WriteLine(Kata.RemoveFirstAndLastChar("Minha String"));
// Console.WriteLine(Kata.SquareDigits(9119));
// Console.WriteLine(Kata.ValidatePin("12132"));
// Console.WriteLine(Kata.ValidatePin("1213a"));
// Console.WriteLine(Kata.ValidatePin("121"));
// Console.WriteLine(Kata.ValidatePin("1212"));
// Console.WriteLine(Kata.Disemvowel("No offense but,\nYour writing is among the worst I've ever read"));
// Console.WriteLine(Kata.NbYear(1500000, 2.5, 10000, 2000000));
// Console.WriteLine("-----");
// Console.WriteLine(Kata.NbYear(1000, 1, 10, 5000));
// Console.WriteLine(Kata.ToJadenCase("How can mirrors be real if our eyes aren't real"));
Console.WriteLine(Kata.Rgb(255, 123, 213));
Console.WriteLine(Kata.Rgb(255, 255, 255));

// Console.WriteLine("FFFFFF  " + Kata.Rgb(255, 255, 255));
// Console.WriteLine("FFFFFF  " + Kata.Rgb(255, 255, 300));
// Console.WriteLine("000000  " + Kata.Rgb(0, 0, 0));
// Console.WriteLine("9400D3  " + Kata.Rgb(148, 0, 211));
// Console.WriteLine("9400D3  " + Kata.Rgb(148, -20, 211));
// Console.WriteLine("90C3D4  " + Kata.Rgb(144, 195, 212));
// Console.WriteLine("D435FF  " + Kata.Rgb(212, 53, 132312));

Kata.IsIsogram("Dermatoglyphics");
Kata.IsIsogram("isogram");
Kata.IsIsogram("moose");
Kata.IsIsogram("isIsogram");
Kata.IsIsogram("aba");
Kata.IsIsogram("moOse");
Kata.IsIsogram("thumbscrewjapingly");
Kata.IsIsogram("");





namespace Codewars
{
    public static class Kata
    {
        public static string StringClean(string s)
        {
            // "a984 abs1fd df2!" => "a absfd df!" 
            return Regex.Replace(s, @"\d", "");
        }

        public static long[] Digitize(long n)
        {
            // 348597 => [7,9,5,8,4,3]
            return n.ToString()
             .Reverse()
             .Select(t => Convert.ToInt64(t.ToString()))
             .ToArray();
        }

        public static int Paperwork(int n, int m)
        {
            return (n < 0 || m < 0) ? 0 : n * m;
        }

        public static string Bmi(double weight, double height)
        {
            double bmi = weight / (height * height);

            if (bmi <= 18.5) return "Underweight";
            if (bmi <= 25.0) return "Normal";
            if (bmi <= 30.0) return "Overweight";
            return "Obese";
        }

        public static int Grow(int[] x)
        {
            // [1, 2, 3, 4] => 1 * 2 * 3 * 4 = 24
            int result = 1;
            foreach (int number in x)
            {
                result *= number;
            }
            return result;
            //return x.Aggregate((runningProduct, nextFactor) => runningProduct * nextFactor);
        }

        public static int[] DoubleElement(int[] x)
        {
            // [1, 2, 3] --> [2, 4, 6]
            int[] doubled = new int[x.Length];

            for (int i = 0; i < x.Length; i++)
            {
                doubled[i] = x[i] * 2;
            }
            return doubled;

            // return Array.ConvertAll(x, n => n * 2);
        }

        public static string HighAndLow(string numbers)
        {
            // "1 2 3 4 5"  -> return "5 1"
            // "1 2 -3 4 5" -> return "5 -3"
            IEnumerable<int> numbersList = numbers.Split(' ').Select(x => Convert.ToInt32(x));
            int high = numbersList.Max();
            int low = numbersList.Min();

            return $"{high} {low}";
            // var parsed = numbers.Split().Select(int.Parse);
            // return parsed.Max() + " " + parsed.Min();
        }

        public static long FindNextSquare(long num)
        {
            // 121 --> 144
            // 625 --> 676
            // 114 --> -1 since 114 is not a perfect square
            if (Math.Sqrt(num) % 1 != 0)
                return -1;
            var number = Math.Sqrt(num);
            return (long)Math.Pow(number + 1, 2);
        }

        public static string RemoveFirstAndLastChar(string s)
        {
            return s.Remove(0, 1).Remove(s.Length - 2, 1);
            // return s.Substring(1,(s.Length - 2));
        }

        public static int SquareDigits(int n)
        {
            // 9119 -> 81 1 1 81 -> 811181
            string result = "";

            foreach (char number in n.ToString())
            {
                result += Math.Pow(int.Parse(number.ToString()), 2);
                Console.WriteLine($"result: {result}");
                Console.WriteLine("-----");
            }

            return int.Parse(result);
        }

        public static int CountSheeps(bool[] sheeps)
        {
            // [true, false, true, true] -> [true, true, true] -> 3
            bool[] presents = Array.FindAll(sheeps, s => s == true);
            return presents.Length;
            // return sheeps.Count(sheep => sheep);
        }

        public static bool IsTriangle(int a, int b, int c)
        {
            bool[] conditions = new bool[4];
            conditions[0] = a > 0 && b > 0 && c > 0;
            conditions[1] = a + b > c;
            conditions[2] = a + c > b;
            conditions[3] = b + c > a;
            return conditions.All(c => c);
            // return Array.TrueForAll(conditions, c => c);

        }

        public static bool ValidatePin(string pin)
        {
            // "1234"   -->  true
            // "12345"  -->  false
            // "a234"   -->  false
            return !Regex.IsMatch(pin, @"\D") && (pin.Length == 4 || pin.Length == 6);
            // return pin.All(n => Char.IsDigit(n)) && (pin.Length == 4 || pin.Length == 6);
            // return Regex.IsMatch(pin, @"^(\d{6}|\d{4})\z");

        }

        public static string Disemvowel(string str)
        {
            string vowels = "aeiou";

            foreach (char vowel in vowels)
            {
                str = str.Replace(Char.ToString(vowel), "");
            }

            return str;
            // return Regex.Replace(str,"[aeiou]", "", RegexOptions.IgnoreCase);
        }

        public static string CreatePhoneNumber(int[] numbers)
        {
            string str = String.Concat(numbers);

            return $"({str.Substring(0, 3)}) {str.Substring(3, 3)}-{str.Substring(6, 4)}";
        }

        public static int NbYear(int p0, double percent, int aug, int p)
        {
            // NbYear(1500, 5, 100, 5000) -> 15
            // NbYear(1500000, 2.5, 10000, 2000000) -> 10
            int numberOfYears = 0;
            while (p0 < p)
            {
                p0 = (int)(p0 * (1 + percent / 100)) + aug;
                numberOfYears++;
            }
            return numberOfYears;
            // int year;
            // for (year = 0; p0 < p; year++)
            //     p0 += (int)(p0 * percent / 100) + aug;
            // return year;
        }

        public static string ToJadenCase(this string phrase)
        {
            // Not Jaden-Cased: "How can mirrors be real if our eyes aren't real"
            // Jaden-Cased:     "How Can Mirrors Be Real If Our Eyes Aren't Real"
            var words = phrase.Split(' ').Select(word => word.Substring(0, 1).ToUpper() + word.Substring(1));
            return String.Join(' ', words);
            // return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(phrase);
        }

        public static string Rgb(int r, int g, int b)
        {
            return DecToHex(r) + DecToHex(g) + DecToHex(b);
        }

        static string DecToHex(int dec)
        {
            if (dec > 255) dec = 255;
            if (dec < 0) dec = 0;

            int left = dec / 16;
            int right = dec % 16;
            return MappingNumberToLetter(left) + MappingNumberToLetter(right);
        }

        static string MappingNumberToLetter(int number)
        {
            switch (number)
            {
                case 10: return "A";
                case 11: return "B";
                case 12: return "C";
                case 13: return "D";
                case 14: return "E";
                case 15: return "F";
                default: return number.ToString();
            }
        }
        public static bool IsIsogram(string str)
        {
            // "Dermatoglyphics" --> true - Não repete nenhuma letra
            // "aba" --> false - repete a letra "a"
            // "moOse" --> false (ignore letter case)
            var distinctLetters = str.ToLower().Distinct().ToList<char>();

            return str.Length == distinctLetters.Count();
            // return str.ToLower().Distinct().Count()==str.Length;
        }

        public static int[] SortArray(int[] array)
        {
            //   You will be given an array of numbers. You have to sort the odd numbers in ascending order while leaving the even numbers at their original positions.
            // [7, 1]  =>  [1, 7]
            // [5, 8, 6, 3, 4]  =>  [3, 8, 6, 5, 4]
            // [9, 8, 7, 6, 5, 4, 3, 2, 1, 0]  =>  [1, 8, 3, 6, 5, 4, 7, 2, 9, 0]

            bool isOdd(int num) => num % 2 != 0;
            int[] odds;
            foreach (var item in array)
            {
                List.
            }
            return array;
        }

    }



}


