using System.Data;
using Microsoft.Data.SqlClient;
using STO.Models;

namespace Sto.API.Database
{
    public class MSSQLContext
    {
        public Product AddProduct(Product p)
        {
            using(SqlConnection con = new SqlConnection("Server=DESKTOP-EDGHUB0;Database=StoDB;Trusted_Connection=yes;TrustServerCertificate=True"))
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
                    if (p.Bogo == true)
                    {
                        command.Parameters.Add(new SqlParameter("Bogo", 1));
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter("Bogo", 0));
                    }
                    if (p.MarkedDown == true)
                    {
                        command.Parameters.Add(new SqlParameter("Bogo", 1));
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter("Bogo", 0));
                    }

                    command.Parameters.Add(new SqlParameter("MarkdownPercent", p.MarkDownPercent));
                   // command.Parameters.Add(new SqlParameter("Id", p.Id));
                   var id = new SqlParameter("Id", p.Id);
                    id.Direction = ParameterDirection.Output;
                    command.Parameters.Add(id);
                    try
                    {

                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();
                        p.Id = (int)id.Value;
                    }
                    catch (Exception ex) 
                    { 

                    }
                }

            }
            return p;
        }
        public List<Product> GetProducts()
        {
            var products = new List<Product>();
            using (SqlConnection con = new SqlConnection("Server=DESKTOP-EDGHUB0;Database=StoDB;Trusted_Connection=yes;TrustServerCertificate=True"))
            {
                using (SqlCommand command = con.CreateCommand())
                {
                    var sql = $"SELECT Id, Name,Description, Stock, Price, Bogo, MarkedDown, MarkDownPercent FROM PRODUCT";
                    
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    try
                    {

                        con.Open();
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            bool bg, md;
                            if ((int)reader["Bogo"] == 1)
                            {
                                bg = true;
                            }
                            else
                            {
                                bg = false;
                            }
                            if ((int)reader["MarkedDown"] == 1)
                            {
                                md = true;
                            }
                            else
                            {
                                md = false;
                            }
                            products.Add(new Product
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"].ToString(),
                                Price = (decimal)reader["Price"],
                                Stock = (int)reader["Stock"],
                                Bogo = bg,
                                MarkedDown = md,
                                MarkDownPercent = (int)reader["MarkDownPercent"]
                            });
                        }
                        con.Close();

                    }
                    catch (Exception ex)
                    {

                    }
                }

            }
            return products;
        }
        public Product Delete(int Id)
        {
            var product = new Product();
            using (SqlConnection con = new SqlConnection("Server=DESKTOP-EDGHUB0;Database=StoDB;Trusted_Connection=yes;TrustServerCertificate=True"))
            {
                using (SqlCommand fetchcommand = con.CreateCommand())
                {
                    var sql = $"SELECT Id, Name,Description, Stock, Price, Bogo, MarkedDown, MarkDownPercent FROM PRODUCT WHERE Id = @Id";

                    fetchcommand.CommandType = CommandType.Text;
                    fetchcommand.CommandText = sql;
                    try
                    {

                        con.Open();
                        var reader = fetchcommand.ExecuteReader();

                        while (reader.Read())
                        {
                            bool bg, md;
                            if ((int)reader["Bogo"] == 1)
                            {
                                bg = true;
                            }
                            else
                            {
                                bg = false;
                            }
                            if ((int)reader["MarkedDown"] == 1)
                            {
                                md = true;
                            }
                            else
                            {
                                md = false;
                            }
                            product.Id = (int)reader["Id"];
                            product.Name = reader["Name"].ToString();
                            product.Description = reader["Description"].ToString();
                            product.Price = (decimal)reader["Price"];
                            product.Stock = (int)reader["Stock"];
                            product.Bogo = bg;
                            product.MarkedDown = md;
                            product.MarkDownPercent = (int)reader["MarkDownPercent"];
                        }
                        con.Close();
                    }
                    catch (Exception ex)
                    {

                    }
                }

                using (SqlCommand command = con.CreateCommand())
                {
                    var sql = $"DELETE FROM Product WHERE Id = @Id";

                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    try
                    {

                        con.Open();
                        var reader = command.ExecuteNonQuery();


                        con.Close();

                    }
                    catch (Exception ex)
                    {

                    }
                }

            }
            return product;
        }    
        
    }
}
