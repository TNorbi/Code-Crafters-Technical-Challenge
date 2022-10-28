using OneTimePassWebApp.API.Data.Models;
using System;

namespace OneTimePassWebApp.API.Data
{
    public class WebAppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<WebAppDbContext>();

                //Upload the Users table if it's empty
                if (context != null && !context.Users.Any())
                {
                    context.Users.AddRange(
                        new Users()
                        {
                            UserName = "testuser1",
                            Password = "123456",
                        },
                        new Users()
                        {
                            UserName = "testuser2",
                            Password = "23456789"
                        }
                   );

                    context.SaveChanges();
                }
            }
        }
    }
}
