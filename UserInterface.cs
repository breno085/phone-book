using phone_book.Models;
using phone_book.Services;
using Spectre.Console;
using static phone_book.Enums;

namespace phone_book
{
    public class UserInterface
    {
        public static void MainMenu()
        {
            bool isContactMenuRunning = true;

            while (isContactMenuRunning)
            {
                var option = AnsiConsole.Prompt(new SelectionPrompt<ContactMenu>()
                .Title("What you like to do?")
                .AddChoices(
                    ContactMenu.AddContact,
                    ContactMenu.DeleteContact,
                    ContactMenu.UpdateContact,
                    ContactMenu.ViewContact,
                    ContactMenu.ViewAllContacts,
                    ContactMenu.Exit
                )
                );

                switch (option)
                {
                    case ContactMenu.AddContact:
                        ContactService.InsertContact();
                        break;
                    case ContactMenu.DeleteContact:
                        ContactService.DeleteContact();
                        break;
                    case ContactMenu.UpdateContact:
                        ContactService.UpdateContact();
                        break;
                    case ContactMenu.ViewContact:
                        ContactService.GetContact();
                        break;
                    case ContactMenu.ViewAllContacts:
                        ContactService.GetAllContacts();
                        break;
                    case ContactMenu.Exit:
                        isContactMenuRunning = false;
                        break;
                }
            }
        }

        internal static void ShowAllContacts(List<Contact> contacts)
        {
            var table = new Table();
            table.AddColumn("Id");
            table.AddColumn("Name");
            table.AddColumn("Email");
            table.AddColumn("Phone Number");

            foreach (var contact in contacts)
            {
                table.AddRow(
                    contact.Id.ToString(),
                    contact.Name,
                    contact.Email,
                    contact.PhoneNumber
                );
            }

            AnsiConsole.Write(table);

            Console.WriteLine("Enter any key to continue");
            Console.ReadLine();
            Console.Clear();
        }

        internal static void ShowContact(Contact contact)
        {
            var panel = new Panel($@"
            Id: {contact.Id}
            Name: {contact.Name}
            Email: {contact.Email}
            Phone Number: {contact.PhoneNumber}")
            {
                Header = new PanelHeader("Contact Info"),
                Padding = new Padding(2, 2, 2, 2)
            };

            AnsiConsole.Write(panel);

            Console.WriteLine("Enter any key to continue");
            Console.ReadLine();
            Console.Clear();
        }
    }
}