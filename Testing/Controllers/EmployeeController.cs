using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;
using System.Web.Http.Cors;

namespace Testing.Controllers
{
    [EnableCorsAttribute("*","*","*")]
    public class EmployeeController : ApiController
    {
        public IEnumerable<employee> Get()
        {
            using (mydatabaseEntities entities = new mydatabaseEntities())
            {
                return entities.employees.ToList();
            }
        }
        public employee Get(int id)
        {
            using (mydatabaseEntities entities = new mydatabaseEntities())
            {
                return entities.employees.FirstOrDefault(e => e.id == id);
            }
        }

        //post method with no response

        //public void Post([FromBody]employee emp)
        //{
        //    using (mydatabaseEntities entities = new mydatabaseEntities())
        //    {
        //        entities.employees.Add(emp);
        //        entities.SaveChanges();
        //    }
        //}

        //post method with response

        public HttpResponseMessage Post([FromBody] employee emp)
        {
            try
            {
                using (mydatabaseEntities entities = new mydatabaseEntities())
                {
                    entities.employees.Add(emp);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, emp);
                    message.Headers.Location = new Uri(Request.RequestUri + emp.id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //public void Delete(int Id)
        //{
        //    using (mydatabaseEntities entitites = new mydatabaseEntities())
        //    {
        //        entitites.employees.Remove(entitites.employees.FirstOrDefault(e => e.id == Id));
        //        entitites.SaveChanges();
        //    }
        //}

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (mydatabaseEntities entities = new mydatabaseEntities())
                {
                    var entity = entities.employees.FirstOrDefault(e => e.id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id =" + id.ToString());
                    }
                    else
                    {
                        entities.employees.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public void Put(int id, [FromBody] employee emp)
        {
            using (mydatabaseEntities entities = new mydatabaseEntities())
            {
                var entity = entities.employees.FirstOrDefault(e => e.id == id);
                entity.firstname = emp.firstname;
                entity.lastname = emp.lastname;
                entity.gender = emp.gender;
                entity.salary = emp.salary;

                entities.SaveChanges();
            }
        }
    }
}
