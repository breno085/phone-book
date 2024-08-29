using phone_book.Context;
using phone_book.Models;

namespace phone_book.Controllers;

public class ContactController
{
    public static void AddContact(Contact contact)
    {
        using var db = new ContactContext();

        db.Add(contact);
        db.SaveChanges();
    }

    public static void DeleteContact(Contact contact)
    {
        using var db = new ContactContext();

        db.Remove(contact);
        db.SaveChanges();
    }

    public static Contact GetContactById(int id)
    {
        using var db = new ContactContext();

        var contact = db.Contacts.Find(id);

        return contact;
    }

    public static List<Contact> GetContactsByName(string name)
    {
        using var db = new ContactContext();

        var contacts = db.Contacts.Where(x =>
        x.Name.ToLower().Contains(name.ToLower()));

        return contacts.ToList();
    }

    public static List<Contact> GetAllContacts()
    {
        using var db = new ContactContext();

        var contacts = db.Contacts.ToList();

        return contacts;
    }

    public static void UpdateContact(Contact contact)
    {
        using var db = new ContactContext();

        db.Update(contact);
        db.SaveChanges();
    }

}