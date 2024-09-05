using phone_book.Controllers;
using phone_book.Models;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace phone_book.Services;

public class ContactService
{
    public static void InsertContact()
    {
        Contact contact = new Contact();
        
        contact.Name = AnsiConsole.Ask<string>("Name: ");
        contact.Email = ValidateEmail();
        contact.PhoneNumber = ValidateBrazilPhoneNumber();

        ContactController.AddContact(contact);
    }

    public static void DeleteContact()
    {
        Contact contactToDelete = GetContactOptionInput();

        ContactController.DeleteContact(contactToDelete);
    }

    public static void GetContact()
    {
        Contact contact = GetContactOptionInput();

        UserInterface.ShowContact(contact);
    }


    public static void GetAllContacts()
    {
        var contacts = ContactController.GetAllContacts();

        UserInterface.ShowAllContacts(contacts);
    }

    public static void UpdateContact()
    {
        Contact contact = GetContactOptionInput();

        contact.Name = AnsiConsole.Confirm("Update name? ") ?
        AnsiConsole.Ask<string>("Contact new name: ") : contact.Name;

        contact.Email = AnsiConsole.Confirm("Update email? ") ?
        ValidateEmail() : contact.Email;

        contact.PhoneNumber = AnsiConsole.Confirm("Update phone number? ") ?
        ValidateBrazilPhoneNumber() : contact.PhoneNumber;

        ContactController.UpdateContact(contact);
    }

    public static Contact GetContactOptionInput()
    {
        var contacts = ContactController.GetAllContacts();

        var contactsArray = contacts.Select(x => x.Name).ToArray();
        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
        .Title("Choose Contact")
        .AddChoices(contactsArray));

        var id = contacts.Single(x => x.Name == option).Id;
        var contact = ContactController.GetContactById(id);

        return contact;
    }

    public static string ValidateEmail()
    {
        string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        string contactEmail = GetUserInput("Email: ");
        while (!Regex.IsMatch(contactEmail.Trim(), emailPattern))
        {
            AnsiConsole.MarkupLine("[red]Invalid email format. Please try again.[/]");
            contactEmail = GetUserInput("Email: ");
        }

        return contactEmail.Trim();
    }

    public static string ValidateBrazilPhoneNumber()
    {
        string phonePattern = @"^\d{2}\s9\d{8}$";

        string contactNumber = GetUserInput("Phone number (Format: XX 9XXXXXXXX): ");
        while (!Regex.IsMatch(contactNumber.Trim(), phonePattern))
        {
            AnsiConsole.MarkupLine("[red]Invalid phone number format. Please use XX 9XXXXXXXX.[/]");
            contactNumber = GetUserInput("Phone number (Format: XX 9XXXXXXXX): ");
        }

        return contactNumber.Trim();
    }

    private static string GetUserInput(string prompt)
    {
        return AnsiConsole.Ask<string>(prompt);
    }
}