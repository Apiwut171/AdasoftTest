using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using System;
using System.Collections.Generic;

namespace Adasofttest.Pages.Account
{
    public class EditModel : PageModel
    {
        public AccountInfo accountInfo = new AccountInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            string idString = Request.Query["id"];
            int id;
            if (!int.TryParse(idString, out id))
            {
                
            }
            try
            {
                string connectionString = "Host=dpg-cikqcrtgkuvinfkokqg0-a.singapore-postgres.render.com;Port=5432;Database=adabase;Username=user;Password=UhjAqXh34VTVW4VkAuEVJSDiXs27XcjI;SSL Mode=Require;Trust Server Certificate=true;";

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM account WHERE id=@id";

                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accountInfo.id = reader.GetInt32(0).ToString();
                                accountInfo.username = reader.GetString(1);
                                accountInfo.email = reader.GetString(2);
                                accountInfo.city = reader.GetString(3);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            accountInfo.id = Request.Form["id"];
            accountInfo.username = Request.Form["username"];
            accountInfo.email = Request.Form["email"];
            accountInfo.city = Request.Form["city"];

            if (string.IsNullOrEmpty(accountInfo.username) || string.IsNullOrEmpty(accountInfo.email) || string.IsNullOrEmpty(accountInfo.city))
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
                    string sql = "UPDATE account SET username=@username, email=@email, city=@city WHERE id=@id";

                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@username", accountInfo.username);
                        command.Parameters.AddWithValue("@email", accountInfo.email);
                        command.Parameters.AddWithValue("@city", accountInfo.city);
                        command.Parameters.AddWithValue("@id", Convert.ToInt32(accountInfo.id));

                        command.ExecuteNonQuery();
                    }

                    successMessage = "Account updated successfully";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Account/Index");
        }
    }

    
}
