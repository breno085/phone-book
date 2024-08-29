using phone_book.Controllers;
using phone_book.Models;
using Spectre.Console;

namespace phone_book.Services;

public class ContactService
{
    public static void InsertContact()
    {
        Contact contact = new Contact
        {
            Name = AnsiConsole.Ask<string>("Name: "),
            Email = AnsiConsole.Ask<string>("Email: "),
            PhoneNumber = AnsiConsole.Ask<string>("PhoneNumber: ")
        };

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

        // UserInterface.ShowContact(contact);
    }


    public static void GetAllContacts()
    {
        var contacts = ContactController.GetAllContacts();

        // UserInterface.ShowAllContacts(contacts);
    }

    public static void UpdateContact()
    {
        Contact contact = GetContactOptionInput();

        contact.Name = AnsiConsole.Confirm("Update name? ") ? 
        AnsiConsole.Ask<string>("Contact new name: ") : contact.Name;

        contact.Email = AnsiConsole.Confirm("Update email? ") ? 
        AnsiConsole.Ask<string>("Contact new email: ") : contact.Email;

        contact.PhoneNumber = AnsiConsole.Confirm("Update phone number? ") ? 
        AnsiConsole.Ask<string>("Contact phone number: ") : contact.PhoneNumber;

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
}