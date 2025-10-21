using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        

        private SqlConnection conn =
        new SqlConnection("Server=mssqlstud.fhict.local;Database=dbi568311_webgame;User Id=dbi568311_webgame;Password=francisco;TrustServerCertificate = true");

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            string sql = "SELECT name, email FROM [user];";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            string name = "";
            string email = "";

            while (reader.Read())
            {
                email += reader["email"].ToString() + "\n";
                name += reader["name"].ToString() + "\n";
            }
            conn.Close();
            ViewData["Names"] = name;
            ViewData["Emails"] = email;
        }
    }
}
