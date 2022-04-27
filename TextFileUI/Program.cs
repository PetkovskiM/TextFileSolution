using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace TextFileUI
{
    public class Program
    {
        private static IConfiguration _config;
        private static string _textFile;
        private static TextFileDataAccess da = new TextFileDataAccess();
        static void Main(string[] args)
        {
            InitializeConfiguration();

            _textFile = _config.GetValue<string>("TextFile");

            ContactModel contact1 = new ContactModel();

            ContactModel contact2 = new ContactModel();

            List<ContactModel> contacts = new List<ContactModel>
            {
                contact1, contact2
            };

            //CreateContact(contact1);
            //CreateContact(contact2);
            //UpdateContact("");
            //RemovePhoneNumberFromUser("");
            //RemoveUser();

            Console.WriteLine();
            GetAllContacts();

        }

        private static void InitializeConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _config = builder.Build();
        }

        private static void GetAllContacts()
        {
            var contacts = da.ReadAllRecords(_textFile);

            foreach (var contact in contacts)
            {
                Console.WriteLine($"{contact.FirstName} {contact.LastName}");
            }
        }

        private static void CreateContact(ContactModel contact)
        {
            var contacts = da.ReadAllRecords(_textFile);
            contacts.Add(contact);
            da.WriteAllRecords(contacts, _textFile);
        }

        private static void UpdateContact(string firstName)
        {
            var contacts = da.ReadAllRecords(_textFile);
            contacts[0].FirstName = firstName;
            da.WriteAllRecords(contacts, _textFile);
        }

        private static void RemovePhoneNumberFromUser(string phoneNumber)
        {
            var contacts = da.ReadAllRecords(_textFile);
            contacts[0].PhoneNumbers.Remove(phoneNumber);
            da.WriteAllRecords(contacts, _textFile);
        }

        private static void RemoveUser()
        {
            var contacts = da.ReadAllRecords(_textFile);
            contacts.RemoveAt(0);
            da.WriteAllRecords(contacts, _textFile);
        }
    }
}
