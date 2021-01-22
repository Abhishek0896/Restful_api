using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeDataAccess;
namespace Testing
{
    public class EmployeeSecurity
    {
        public static bool Login(string username, string password)
        {
            using (mydatabaseEntities entities = new mydatabaseEntities())
            {
                return entities.users.Any(u => u.name.Equals(username,
                StringComparison.OrdinalIgnoreCase) && u.password == password);
            }
        }
    }
}