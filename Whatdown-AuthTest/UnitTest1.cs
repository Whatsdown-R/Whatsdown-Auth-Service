using Microsoft.EntityFrameworkCore;
using Whatsdown_Authentication_Service.Data;
using Whatsdown_Authentication_Service.Logic;
using Whatsdown_Authentication_Service.Models;
using Xunit;

namespace Whatdown_AuthTest
{
    public class UnitTest1
    {
        private DbContextOptions<AuthenticationContext> options = new DbContextOptionsBuilder<AuthenticationContext>()
        .UseInMemoryDatabase(databaseName: "UserDatabase")
        .Options;


/*        [Fact(DisplayName = "Test Registers user")]
       
        public void RegisterUser()
        {
            using (var context = new AuthenticationContext(options))
            {
                AuthLogic logic = new AuthLogic(context);
                RegisterModel registerModel = new RegisterModel("Email@hotmail.com","Password","password","Revely","MALE");
                logic.Register(registerModel);
                
                

            }
        }*/
    }
}
