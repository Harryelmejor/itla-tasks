using System;
using System.Collections.Generic;

class PersonalAgenda
{
    // Dictionary: key = email (unique), value = all contact data as a single string
    static Dictionary<string, string> contacts = new Dictionary<string, string>();

    static void Main(string[] args)
    {
        Console.WriteLine("=== PERSONAL AGENDA ===");

        while (true)
        {
            Console.WriteLine("\n1. Add contact");
            Console.WriteLine("2. Edit contact");
            Console.WriteLine("3. Delete contact");
            Console.WriteLine("4. Search contact by email");
            Console.WriteLine("5. View all contacts");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");

            string option = Console.ReadLine();

            if (option == "1") AddContact();
            else if (option == "2") EditContact();
            else if (option == "3") DeleteContact();
            else if (option == "4") SearchContact();
            else if (option == "5") ListAllContacts();
            else if (option == "6") break;
            else Console.WriteLine("Invalid option.");
        }

        Console.WriteLine("Goodbye!");
    }

    static void AddContact()
    {
        Console.Write("First name: ");
        string firstName = Console.ReadLine();
        Console.Write("Last name: ");
        string lastName = Console.ReadLine();
        Console.Write("Phone number: ");
        string phone = Console.ReadLine();
        Console.Write("Email (must be unique): ");
        string email = Console.ReadLine().ToLower().Trim();

        if (contacts.ContainsKey(email))
        {
            Console.WriteLine("  A contact with this email already exists.");
            return;
        }

        Console.Write("Address: ");
        string address = Console.ReadLine();
        Console.Write("Age: ");
        string age = Console.ReadLine();
        Console.Write("Favorite? (y/n): ");
        string isFavorite = Console.ReadLine();

        string data = $"First name: {firstName}, Last name: {lastName}, Phone: {phone}, Address: {address}, Age: {age}, Favorite: {isFavorite}";
        contacts[email] = data;
        Console.WriteLine(" Contact added.");
    }

    static void EditContact()
    {
        Console.Write("Enter the email of the contact to edit: ");
        string email = Console.ReadLine().ToLower().Trim();

        if (!contacts.ContainsKey(email))
        {
            Console.WriteLine("  Contact not found.");
            return;
        }

        Console.Write("New first name: ");
        string firstName = Console.ReadLine();
        Console.Write("New last name: ");
        string lastName = Console.ReadLine();
        Console.Write("New phone number: ");
        string phone = Console.ReadLine();
        Console.Write("New address: ");
        string address = Console.ReadLine();
        Console.Write("New age: ");
        string age = Console.ReadLine();
        Console.Write("Favorite? (y/n): ");
        string isFavorite = Console.ReadLine();

        string data = $"First name: {firstName}, Last name: {lastName}, Phone: {phone}, Address: {address}, Age: {age}, Favorite: {isFavorite}";
        contacts[email] = data;
        Console.WriteLine(" Contact updated.");
    }

    static void DeleteContact()
    {
        Console.Write("Email of the contact to delete: ");
        string email = Console.ReadLine().ToLower().Trim();

        if (contacts.Remove(email))
            Console.WriteLine(" Contact deleted.");
        else
            Console.WriteLine("  Contact not found.");
    }

    static void SearchContact()
    {
        Console.Write("Enter the email to search: ");
        string email = Console.ReadLine().ToLower().Trim();

        if (contacts.TryGetValue(email, out string data))
            Console.WriteLine($"\nEmail: {email}\n{data}");
        else
            Console.WriteLine("  Contact not found.");
    }

    static void ListAllContacts()
    {
        if (contacts.Count == 0)
        {
            Console.WriteLine(" No contacts found.");
            return;
        }

        Console.WriteLine("\n--- All Contacts ---");
        foreach (var contact in contacts)
        {
            Console.WriteLine($"Email: {contact.Key}\n{contact.Value}\n");
        }
    }
}