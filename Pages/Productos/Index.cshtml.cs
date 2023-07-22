using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MiElectronica.Pages.Productos
{
    public class IndexModel : PageModel
    {
        public List<productosInfo> listProductos = new List<productosInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\oscar\\OneDrive\\m\\Documentos\\MiElectronica.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM productos";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productosInfo productosInfo = new productosInfo();
                                productosInfo.id = "" + reader.GetInt32(0);
                                productosInfo.nombre = reader.GetString(1);
                                productosInfo.stock = reader.GetString(2);
                                productosInfo.cantidad_producto = reader.GetString(3);
                                productosInfo.fecha_creacion = reader.GetDateTime(4).ToString();

                                listProductos.Add(productosInfo);

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
    public class productosInfo
    {
        public string id;
        public string nombre;
        public string stock;
        public string cantidad_producto;
        public string fecha_creacion;

    }
}
