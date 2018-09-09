using System;
using CSharpStringCalculator;
using NUnit.Framework;

// ReSharper disable PossibleMultipleEnumeration

namespace StringCalculatorTests
{
    //Create a simple String Calculator with a method public int Add(string numbers)
    //The method can take 0, 1 or 2 numbers, and will return their sum (for an empty string it will return 0) for example “” or “1” or “1,2”
    //Start with the simplest test case of an empty string and move to 1 and two numbers
    //Allow the Add method to handle an unknown amount of numbers
    //Allow the Add method to handle new lines between numbers (instead of commas).
    //The following input is ok: “1\n2,3” (will equal 6)
    //The following input is NOT ok: “1,\n” (not need to prove it - just clarifying)
    //Support different delimiters
    //To change a delimiter, the beginning of the string will contain a separate line that looks like this: 
    //“//[delimiter]\n[numbers…]” for example “//;\n1;2” should return three where the default delimiter is ‘;’ .
    //The first line is optional. all existing scenarios should still be supported
    //Calling Add with a negative number will throw an exception “negatives not allowed” - and the negative 
    //that was passed.if there are multiple negatives, show all of them in the exception message
    //Numbers bigger than 1000 should be ignored, so adding 2 + 1001 = 2
    //Delimiters can be of any length with the following format: “//[delimiter]\n” for example: “//[***]\n1***2***3” should return 6
    //Allow multiple delimiters like this: “//[delim1][delim2]\n” for example “//[][%]\n12%3” should return 6.
    //Make sure you can also handle multiple delimiters with length longer than one char
    public class StringCalculatorShould
    {
        [TestCase("", 0)]
        [TestCase("1", 1)]
        [TestCase("2", 2)]
        [TestCase("3", 3)]
        [TestCase("1,1", 2)]
        [TestCase("1,2", 3)]
        [TestCase("1\n1", 2)]
        [TestCase("//;\n1;1", 2)]
        [TestCase("1,1001", 1)]
        [TestCase("1,1000", 1001)]
        [TestCase("//[***]\n1***1", 2)]
        [TestCase("//[***]\n1***1,1", 3)]
        [TestCase("//[*][%]\n1*1%1", 3)]
        public void SumNumbersCorrectly(string numbersToAdd, int expectedSum)
        {
            var sumResult = StringCalculator.Sum(numbersToAdd);
            Assert.That(sumResult, Is.EqualTo(expectedSum));
        }

        [TestCase("-1", "-1")]
        [TestCase("-1,1", "-1")]
        [TestCase("-1,-1", "-1,-1")]
        [TestCase("//;\n1;-1", "-1")]
        [TestCase("//;\n1;-1;-2", "-1,-2")]
        public void ThrowNegativesNotAllowedExceptionWhenTryingToSumNegatives(string numbersWithIncorrectFormat, string numbersInExceptionMessage)
        {
            var expectedMessage = "Negatives not allowed: " + numbersInExceptionMessage;

            var exception = Assert.Throws<ArgumentException>(() => StringCalculator.Sum(numbersWithIncorrectFormat));
            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }
    }
}