using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientLogin.Components
{
    public class Client
    {
        // Get the keys and the value from the file country_code.txt
        // Any changes to the dictionary must be done in that file
        Dictionary<string, string> countryCode = File.ReadAllLines("ClientLogin/Assets/Dictionary/country_code.txt")
                                                .Select(x => x.Split(" = "))
                                                .ToDictionary(x => x[0], x => x[1]);
        DateTime now = DateTime.Now;
        public int[] currentDate = new int[3];
        private string name { get; set; }
        private uint age { get; set; }
        private string birthday { get; set; }
        private string email { get; set; }
        private string password { get; set; }
        private string country { get; set; }
        private int[] birthdayArr = new int[3];

        public Client(string clientName)
        {
            currentDate = [now.Year, now.Month, now.Day];
            this.name = clientName;
            age = 0;
            birthday = "";
            email = "";
            password = "";
            country = "";
        }

        public Client(string clientName, uint clientAge, string clientBirthday, string clientEmail, string clientPassword, string clientCountry)
        {
            currentDate = [now.Year, now.Month, now.Day];
            this.name = clientName;
            this.age = clientAge;
            this.birthday = clientBirthday;
            this.email = clientEmail;
            this.password = clientPassword;
            this.country = clientCountry;
        }
        public override string ToString()
        {
            //  This is called interpolation
            return $"\nName: {name}\nAge: {age}\nBirthday: {birthday}\nCountry: {country}\nEmail: {email}\nPassword: {password}\n";
        }
        public string Name
        {
            get { return name; }
            set
            {
                if (value.Length < 100)
                {
                    name = value;
                }
            }
        }
        public uint Age
        {
            get { return age; }
        }
        public string Birthday
        {
            get { return birthday; }
            set
            {
                // Parsing the date
                birthday = value;
                birthdayArr = Array.ConvertAll(birthday.Split("/"), int.Parse);
                age = (uint)(now.Year - birthdayArr[0]);
                if (now.Year >= birthdayArr[0] && now.Month >= birthdayArr[1] && now.Day >= birthdayArr[2])
                {
                    age++;
                }
            }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Password
        {
            get { return password; }
            set
            {
                if (value.Length < 40)
                {
                    password = value;
                }
            }
        }
        public string Country
        {
            get { return country; }
            set { country = countryCode[value.ToUpper()]; }
        }
        public string[] getClientFile()
        {
            string[] lines = [];
            bool isValid = true;

            // Get the data of the client from the file with their name
            try
            {
                lines = File.ReadAllLines($"ClientLogin/Assets/Clients/{this.name.ToLower().Replace(" ", "_")}.txt");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Directory not found");
            }
            finally
            {
                Console.WriteLine($"The file {this.name.ToLower().Replace(" ", "_")}.txt was found sucessfully");
            }

            // Check if there is any issue on the data provided
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "")
                {
                    isValid = false;
                }
            }
            if (!isValid)
            {
                throw new Exception("Blank fields are not allowed");
            }
            if (lines[0] == this.name || lines[0] == this.name.ToLower())
            {
                throw new Exception("The name must be specified in the intialization of the client");
            }

            return lines;
        }
    }
}