namespace SoftBlueFund.Exercises
{
    // var notas = new AverageWeighted();
    // notas.Menu();

    public class AverageWeighted
    {
        public AverageWeighted()
        {
            Grades = new List<double>();
            WeightList = new List<double>();
        }
        public List<double> Grades { get; set; }
        public List<double> WeightList { get; set; }
        public void Menu()
        {
            while (true)
            {
                Console.WriteLine($"Digite a Nota: ");
                var grade = GetGrade();
                if (grade == -1)
                {
                    Console.WriteLine($"A média ponderada é: {CalculateAverageWeighted()}");
                    break;
                }

                Grades.Add(grade);
                Console.WriteLine($"Digite o Peso: ");
                WeightList.Add(GetWeight());
                Console.WriteLine("------");
            }
        }
        public void GetGradesAndWeight()
        {
            Console.WriteLine($"Digite a Nota: ");
            Grades.Add(GetGrade());
            Console.WriteLine($"Digite o Peso: ");
            WeightList.Add(GetWeight());
            Console.WriteLine();
        }

        double GetGrade()
        {
            var grade = Console.ReadLine();
            return grade == null || grade == "" ? GetGrade() : double.Parse(grade);
        }
        double GetWeight()
        {
            var weight = Console.ReadLine();
            return weight == null || weight == "" ? GetWeight() : double.Parse(weight);
        }

        public double CalculateAverageWeighted()
        {
            var products = new List<double>();

            foreach (var (grade, weight) in Grades.Zip(WeightList))
            {
                products.Add(grade * weight);
            }

            return products.Sum() / WeightList.Sum();
        }

    }
}