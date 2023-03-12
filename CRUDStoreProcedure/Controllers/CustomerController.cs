using CRUDStoreProcedure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDStoreProcedure.Controllers
{
    public class CustomerController : Controller
    {

        ApplicationDbContext _db;

        public CustomerController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public IActionResult Index()
        {

           var GetallCustomer = _db.Customers.FromSqlRaw("GetAllCustomers").ToList();
            return View(GetallCustomer);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string Name,string Mobile,string Email)
        {
            var parame = new SqlParameter[] { 
            
                   new SqlParameter()
                   {
                       ParameterName="@Name",
                       SqlDbType = System.Data.SqlDbType.VarChar,
                       Value =Name
                   },new SqlParameter()
                   {
                       ParameterName = "@Mobile",
                       SqlDbType = System.Data.SqlDbType.VarChar,
                       Value = Mobile
                   },
                   new SqlParameter()
                   {
                       ParameterName = "@Email",
                       SqlDbType = System.Data.SqlDbType.VarChar,
                       Value=Email
                   }

            };



            var insert = await _db.Database.ExecuteSqlRawAsync($" Exec AddCustomer @Name,@Mobile,@Email",parame);
            if(insert == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
            
        }
        public IActionResult GetDetails(int? Id)
        {

            var GetCustomerDetails = _db.Customers.FromSqlRaw($"GetSingleCustomerRec {Id}").AsEnumerable().FirstOrDefault();
            return View(GetCustomerDetails);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDetails (int Id,string Mobile, string Email)
        {
            var parameter = new SqlParameter[]
            {
                new SqlParameter()
                {
                    ParameterName="@Id",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value=Id

                },
                new SqlParameter()
                {
                    ParameterName="@Mobile",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value=Mobile

                },
                new SqlParameter()
                {
                    ParameterName="@Email",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value=Email

                }

            };

            var update = await _db.Database.ExecuteSqlRawAsync($" Exec UpdateCustomerRecords @Id,@Mobile,@Email", parameter);

            if(update == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();       
            }
        }
       
        public IActionResult Delete(int? Id)
        {
            var delete =  _db.Database.ExecuteSqlRaw($" Exec DeleteCustomerRecord {Id}");


            if(delete == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

            
        }
    }
}
