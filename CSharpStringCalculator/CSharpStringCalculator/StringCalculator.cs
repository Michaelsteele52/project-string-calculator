using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CSharpStringCalculator
{
    public class StringCalculator
    {
        public static int Sum(string numbersToAdd)
        {
            if (string.IsNullOrEmpty(numbersToAdd))
            {
                return 0;
            }

            var pattern = CreateSeparatorPattern(numbersToAdd);
            var trimmedNumbers = TrimSeparatorFromString(numbersToAdd);
            var listOfNumbers = Regex.Split(trimmedNumbers, pattern);

            ThrowArgumentExceptionIfThereAreNegativeNumbers(listOfNumbers);

            return listOfNumbers
                .Select(int.Parse)
                .Where(x => x < 1001)
                .Sum();
        }

        private static void ThrowArgumentExceptionIfThereAreNegativeNumbers(IEnumerable<string> listOfNumbers)
        {
            var badNumbers = listOfNumbers.Where(x => x.Contains("-"));
            if (badNumbers.Any())
            {
                throw new ArgumentException("Negatives not allowed: " + string.Join(",", badNumbers));
            }
        }

        private static string TrimSeparatorFromString(string numbersToAdd)
        {
            if (numbersToAdd.StartsWith("//"))
            {
                var index = numbersToAdd.IndexOf('\n');
                return numbersToAdd.Substring(index + 1);
            }

            return numbersToAdd;
        }

        private static string CreateSeparatorPattern(string numbersToAdd)
        {
            var pattern = "[,\n]+";
            if (numbersToAdd.StartsWith("//"))
            {
                var separator = numbersToAdd
                    .TrimStart('/')
                    .Split('\n')[0]
                    .Replace("[", "")
                    .Replace("]", "");
                return $"{pattern}|[{separator}]+";
            }

            return pattern;
        }
    }
}