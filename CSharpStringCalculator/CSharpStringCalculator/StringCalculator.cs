using System.Collections.Generic;
using System.Linq;

namespace CSharpStringCalculator
{
    public class StringCalculator
    {
        public static int Sum(string numbersToAdd)
        {
            var delims = new List<char> {',', '\n'};

            if (numbersToAdd.StartsWith("//"))
            {
                var delim = numbersToAdd[2];
                delims.Add(delim);
                numbersToAdd = numbersToAdd.Split('\n')[1];
            }

            if (string.IsNullOrEmpty(numbersToAdd))
            {
                return 0;
            }

            var listOfNumbers = numbersToAdd.Split(delims.ToArray());
            int sum = listOfNumbers.Select(x => int.Parse(x)).Sum();

            return sum;
        }
    }
}