using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataAccessLibrary
{
    public class TextFileDataAccess
    {
        public List<ContactModel> ReadAllRecords(string textFile)
        {
            
            if(File.Exists(textFile) == false) 
            {
                return new List<ContactModel>();
            }
            var lines = File.ReadAllLines(textFile);
            List<ContactModel> output = new List<ContactModel>();

            foreach (var line in lines)
            {
                ContactModel model = new ContactModel();
                var vals = line.Split(',');

                if(vals.Length < 4)
                {
                    throw new Exception($"invalid row of data {line}");
                }

                model.FirstName = vals[0];
                model.LastName = vals[1];
                model.Emails = vals[2].Split(';').ToList();
                model.PhoneNumbers = vals[3].Split(';').ToList();

                output.Add(model);

            }

            return output;
        }

        public void WriteAllRecords(List<ContactModel> contacts, string textFile)
        {
            List<string> lines = new List<string>();

            foreach(var c in contacts)
            {
                lines.Add($"{c.FirstName},{c.LastName},{string.Join(';', c.Emails)},{string.Join(';', c.PhoneNumbers)}");
            }

            File.WriteAllLines(textFile, lines);
        }
    }
}
