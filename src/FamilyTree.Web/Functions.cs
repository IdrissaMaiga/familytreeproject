using Irony.Parsing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileSystemGlobbing;
using Nest;
using OrchardCore.ContentManagement;
using OrchardCore.Contents.Recipes;
using System.Globalization;
using System.Text;

namespace FamilyTree.Web
{


    public class Functions
    {


        // Define a method to get a list of PersonViewModel instances from a family content item
        public static List<PersonViewModel> GetFamilyMembers(ContentItem family)
        {
            var allFamilyMembers = family.Content.BagPart.ContentItems;
            var familyMembers = new List<PersonViewModel>();

            foreach (var contentItem in allFamilyMembers)

            {
                List<string> imgpath = new List<string>();
                var kids = new List<PersonViewModel>();


                foreach (var img in contentItem.Person.Galerie.Paths)
                {
                    imgpath.Add((string)img);
                }



                var person = new PersonViewModel()
                {
                    Name = contentItem.DisplayText,
                    Gender = contentItem.Person.Gender.Text, // Assuming this property exists in your ContentItem
                                                             // Map other properties accordingly
                                                             // For example:
                    DateOfBirth = contentItem.Person.DateOfBirth.Value.ToString(), // Convert DateTime to string if needed
                    Autobiography = contentItem.Person.Autobiography.Text,
                    Father = contentItem.Person.Father.Text,
                    Mother = contentItem.Person.Mother.Text,
                    Husband = contentItem.Person.Husband.Text,
                    Wife = contentItem.Person.Wife.Text,
                    Address = contentItem.Person.Address.Text,
                    Generation = contentItem.Person.Generation.Text,
                    wifecount = 0,

                    // Map Children recursively if needed
                    // For example:

                    // Set ImagePath if available
                    ImagePath = (imgpath.Count > 0) ? imgpath.ToArray()[0] : "",
                    MariedSince = (contentItem.Person.MariedSince.Value != null) ? time(contentItem.Person.MariedSince.Value.ToString()) : null
                };
                familyMembers.Add(person);
            }

            return familyMembers;
        }
        private static readonly Dictionary<int, string> RomanValues = new Dictionary<int, string>()
    {
        { 1000, "M" },
        { 900, "CM" },
        { 500, "D" },
        { 400, "CD" },
        { 100, "C" },
        { 90, "XC" },
        { 50, "L" },
        { 40, "XL" },
        { 10, "X" },
        { 9, "IX" },
        { 5, "V" },
        { 4, "IV" },
        { 1, "I" }
    };

        public static string IntToRoman(int number)
        {
            if (number <= 0 || number > 3999)
                throw new ArgumentOutOfRangeException(nameof(number), "Number must be between 1 and 3999.");

            StringBuilder roman = new StringBuilder();
            foreach (var kvp in RomanValues)
            {
                while (number >= kvp.Key)
                {
                    roman.Append(kvp.Value);
                    number -= kvp.Key;
                }
            }
            return roman.ToString();
        }
        public static int RomanToInt(string roman)
        {
            if (string.IsNullOrEmpty(roman))
                return -1;

            Dictionary<char, int> romanValues = new Dictionary<char, int>()
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000}
        };

            int result = 0;
            int prevValue = 0;

            for (int i = roman.Length - 1; i >= 0; i--)
            {
                int value = romanValues[roman[i]];

                if (value < prevValue)
                {
                    result -= value;
                }
                else
                {
                    result += value;
                }

                prevValue = value;
            }

            return result;
        }
        public static DateTime time(string dstring)
        {

            string[] dateParts = dstring.Split(new char[] { ' ', '/', ':' });

            // Extract date and time components
            int day = int.Parse(dateParts[0]);
            int month = int.Parse(dateParts[1]);
            int year = int.Parse(dateParts[2]);
            int hour = int.Parse(dateParts[3]);
            int minute = int.Parse(dateParts[4]);
            int second = int.Parse(dateParts[5]);
            // Create a new DateTime object
            return new DateTime(year, month, day, hour, minute, second);


        }
        public static int? CalculateAge(string dateString)
        {
            // Define the custom date and time format specifier

            if (!string.IsNullOrEmpty(dateString))
            {
                DateTime parsedDate = time(dateString);


                // Get the current date
                DateTime currentDate = DateTime.UtcNow;

                // Calculate the age
                int age = currentDate.Year - parsedDate.Year;

                // Check if the birthday has occurred this year
                if (parsedDate.Date > currentDate.AddYears(-age))
                {
                    age--;
                }

                return age;
            }
            return null;
        }
        // Handle the case where the birth date string is not in a valid format


    }
}






