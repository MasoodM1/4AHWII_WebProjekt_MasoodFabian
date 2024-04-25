namespace _4AHWII_WebProjekt_MasoodFabian.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Passwort { get; set; }
        public string Email { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Telefon { get; set; }
        public string Adresse { get; set; }
        public DateTime Geburtsdatum { get; set; }
        public bool Newsletter { get; set; }

        public User(int id, string username, string password, string email, string vorname, string nachname, string phoneNumber, string address, DateTime geburtsdatum, bool newsletter)
        {
            Id = id;
            Username = username;
            Passwort = password;
            Email = email;
            Vorname = vorname;
            Nachname = nachname;
            Telefon = phoneNumber;
            Adresse = address;
            Geburtsdatum = geburtsdatum;
            Newsletter = newsletter;
        }

        public User()
        {

        }

        public override string ToString()
        {
            return "Username:" + Username + "Email: " + Email + "Vorname: " + Vorname + "Nachname: " + Nachname
                + "Phone: " + Telefon + "Geburtsdatum: " + Geburtsdatum;
        }
    }
}