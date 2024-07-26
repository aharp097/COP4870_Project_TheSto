using System.Data;
using Microsoft.Data.SqlClient;
using STO.Models;

namespace Sto.API.Database
{
    public class MSSQLContext
    {
        Product AddProduct(Product p)
        {
            using(SqlConnection con = new SqlConnection("Server=DESKTOP-EDGHUB0;Database=StoDB;Trusted_Connection=yes;"))
            {
                using (SqlCommand command = con.CreateCommand())
                {
                    var sql = $"Product.InsertProduct";
                    command.CommandText = sql ;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("Name", p.Name));
                    command.Parameters.Add(new SqlParameter("Description", p.Description));
                    command.Parameters.Add(new SqlParameter("Price", p.Price));
                    command.Parameters.Add(new SqlParameter("Stock", p.Stock));
                    command.Parameters.Add(new SqlParameter("Bogo", p.Bogo));
                    command.Parameters.Add(new SqlParameter("MarkedDown", p.MarkedDown));
                    command.Parameters.Add(new SqlParameter("MarkdownPercent", p.MarkDownPercent));
                   // command.Parameters.Add(new SqlParameter("Id", p.Id));
                   var id = new SqlParameter("Id", p.Id);
                    id.Direction = ParameterDirection.Output;
                    command.Parameters.Add(id);
                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();
                }

            }
            return p;
        }
    }
}
