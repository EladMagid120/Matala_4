using Steam_1.DAL;
using System.Text.Json.Serialization;

namespace Steam_1.Models
{
    public class AppUser
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Email")]
        public string Email { get; set; }

        [JsonPropertyName("Password")]
        public string Password { get; set; }

        public bool isActive { get; set; }


        public AppUser() { }

        public AppUser(string name, string email, string password, bool isActive)
        {
            Name = name;
            Email = email;
            Password = password;
            this.isActive = isActive;
        }

        static public List<AppUser> Read()
        {
            DBServices dbs = new DBServices();

            return dbs.Read();
        }

        public int Insert()
        {
            List<AppUser> users = Read();

            foreach (AppUser user in users)
            {
                if (this.Email == user.Email)
                {
                    return 0;
                }
            }

            DBServices dbs = new DBServices();
            return dbs.Insert(this);
        }

         public bool Login(string Email, string Password)
         {
            
            DBServices dbs = new DBServices();

            foreach (AppUser user in dbs.Read())
            {
                if (user.Email == Email && user.Password == Password)
                {
                    if (!user.isActive)
                    {
                        throw new Exception("User is not active");
                    }
                    return true;
                }
            }
            return false;
         }

        public bool CheckIfAdmin(string email)
        {
            return email.ToLower() == "admin@admin.com"; // אימייל קבוע עבור Admin
        }

        public (int UserId, string Name) GetUserIdByEmail(string email)
        {
            // שימוש ב-DBServices לשליפת ה-ID לפי אימייל
            DBServices dbs = new DBServices();
            return dbs.GetUserIdByEmail(email); // קריאה למתודה הנכונה ב-DBServices
        }

        public AppUser Update()
        {
            DBServices dbs = new DBServices();
            List<AppUser> usersList = dbs.Read(); // קבלת רשימת המשתמשים

            Console.WriteLine($"Updating User: ID={this.Id}, Name={this.Name}, Email={this.Email}, Password={this.Password}");
            Console.WriteLine($"Existing Users in DB:");
            foreach (var user in usersList)
            {
                Console.WriteLine($"ID={user.Id}, Name={user.Name}, Email={user.Email}");
            }

            // בדיקת אם המשתמש קיים
            AppUser existingUser = usersList.FirstOrDefault(u => u.Id == this.Id);
            if (existingUser == null)
            {
                Console.WriteLine("User not found.");
                return null; // המשתמש לא נמצא
            }

            // בדיקת אם האימייל בשימוש על ידי משתמש אחר
            bool isEmailUsedByAnother = usersList.Any(u => u.Email == this.Email && u.Id != this.Id);
            if (isEmailUsedByAnother)
            {
                Console.WriteLine("Email is already in use.");
                return null; // האימייל תפוס
            }

            // עדכון המידע של המשתמש
            return dbs.UpdateUser(this);
        }

        public bool UpdateIsActive(bool isActive)
        {
            DBServices dbs = new DBServices();
            int result = dbs.UpdateIsActive(this.Id, isActive); // שימוש ב-Id מהמחלקה

            // אם עודכנה לפחות שורה אחת, נחזיר true
            return result > 0;
        }

        public List<object> GetUserDetails()
        {
            DBServices dbs = new DBServices();
            return dbs.GetUserDetails(); // קריאה למתודה ב-DBServices
        }
    }
}
