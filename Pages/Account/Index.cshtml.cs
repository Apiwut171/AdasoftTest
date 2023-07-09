using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using System;
using System.Collections.Generic;

namespace Adasofttest.Pages.Account
{
    public class IndexModel : PageModel
    {
        public List<AccountInfo> accountList = new List<AccountInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Host=dpg-cikqcrtgkuvinfkokqg0-a.singapore-postgres.render.com;Port=5432;Database=adabase;Username=user;Password=UhjAqXh34VTVW4VkAuEVJSDiXs27XcjI;SSL Mode=Require;Trust Server Certificate=true;";

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM account";
                    
                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AccountInfo accountInfo = new AccountInfo();
                                accountInfo.id =""+ reader.GetInt32(0);
                                accountInfo.username = reader.GetString(1);
                                accountInfo.email = reader.GetString(2);
                                accountInfo.city = reader.GetString(3);
                                

                                accountList.Add(accountInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class AccountInfo
    {
        public string id;
        public string username;
        public string email;
        public string city;
        
    }
}
