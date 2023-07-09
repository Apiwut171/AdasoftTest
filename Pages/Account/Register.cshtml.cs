using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;

namespace Adasofttest.Pages.Account
{
    public class RegisterModel : PageModel
    {
        public AccountInfo accountInfo = new AccountInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        
        }

        public void OnPost()
        {
            accountInfo.username = Request.Form["username"];
            accountInfo.email = Request.Form["email"];
            accountInfo.city = Request.Form["city"];

            if (accountInfo.username.Length == 0 || accountInfo.email.Length == 0 || accountInfo.city.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }
            try
            {
                string connectionString = "Host=dpg-cikqcrtgkuvinfkokqg0-a.singapore-postgres.render.com;Port=5432;Database=adabase;Username=user;Password=UhjAqXh34VTVW4VkAuEVJSDiXs27XcjI;SSL Mode=Require;Trust Server Certificate=true;";

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO account (username,email,city) VALUES (@username,@email,@city);";
                    
                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@username", accountInfo.username);
                        command.Parameters.AddWithValue("@email", accountInfo.email);
                        command.Parameters.AddWithValue("@city", accountInfo.city);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            accountInfo.username = "";
            accountInfo.email = "";
            accountInfo.city = "";
            successMessage = "New Account Added Successfully";

            Response.Redirect("/Account/Index");
        }
    }

    
}
