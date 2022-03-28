using CRUD_WebAPI__SP_.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;

namespace CRUD_WebAPI__SP_.Controllers
{
    public class HomeController : Controller
    {
        string dbConn = @"Data Source=DESKTOP-CKOPJQ7\SQLEXPRESS;Initial Catalog=CRUD;User ID=sa;Password=1234";
        SqlDataReader dr;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            UserInfo ui = new UserInfo();

            SqlConnection mainConn = new SqlConnection(dbConn);
            mainConn.Open();
            SqlCommand selectCmd = new SqlCommand("UserInfoProcedure", mainConn);
            selectCmd.CommandType = System.Data.CommandType.StoredProcedure;
            selectCmd.Parameters.Add(new SqlParameter("@filter", "Read"));
            selectCmd.Parameters.Add(new SqlParameter("@UserName", "Read"));
            dr = selectCmd.ExecuteReader();

            while (dr.Read())
            {
                ui.UserName = dr.GetValue(1).ToString();
            }
            mainConn.Close();

            return View(ui);
        }

        [HttpPost]
        public IActionResult Index(UserInfo ui)
        {
            SqlConnection mainConn = new SqlConnection(dbConn);
            mainConn.Open();
            SqlCommand insertCmd = new SqlCommand("UserInforProcedure", mainConn);
            insertCmd.CommandType = System.Data.CommandType.StoredProcedure;
            insertCmd.Parameters.Add(new SqlParameter("@filter", "Create"));
            insertCmd.Parameters.Add(new SqlParameter("@UserName", "Create"));
            return View(ui);
        }

        [HttpPut]
        public IActionResult UpdateUserInfo (UserInfo ui)
        {
            SqlConnection mainConn = new SqlConnection (dbConn);
            mainConn.Open ();
            SqlCommand updateCmd = new SqlCommand("UserInforProcedure", mainConn);
            updateCmd.CommandType = System.Data.CommandType.StoredProcedure;
            updateCmd.Parameters.Add(new SqlParameter("@filter","Update"));
            updateCmd.Parameters.Add(new SqlParameter("@UserName", "Update"));
            updateCmd.Parameters.Add(new SqlParameter("@NewUserName", "Update"));
            return View(ui);
        }

        [HttpDelete]
        public IActionResult DeleteUserInfo (UserInfo ui)
        {
            SqlConnection mainConn = new SqlConnection(dbConn);
            mainConn.Open();
            SqlCommand deleteCmd = new SqlCommand("UserInforProcedure", mainConn);
            deleteCmd.CommandType = System.Data.CommandType.StoredProcedure;
            deleteCmd.Parameters.Add(new SqlParameter("@filter", "Delete"));
            deleteCmd.Parameters.Add(new SqlParameter("@UserName", "Delete"));
            return View(ui);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}