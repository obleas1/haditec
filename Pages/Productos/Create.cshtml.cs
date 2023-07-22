using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MiElectronica.Pages.Productos
{
    public class CreateModel : PageModel
    {
        public productosInfo ProductosInfo = new productosInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost() 
        {
            ProductosInfo.nombre = Request.Form["nombre"];
            ProductosInfo.stock = Request.Form["stock"];
            ProductosInfo.cantidad_producto = Request.Form["cantidad_producto"];
            ProductosInfo.fecha_creacion = Request.Form["fecha_creacion"];

            if (ProductosInfo.nombre.Length == 0 || ProductosInfo.stock.Length == 0 || ProductosInfo.cantidad_producto.Length == 0)
            {
                errorMessage = "Todos los campos son requeridos";
                return;
            }
            //guardar el producto en la base de datos
            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\oscar\\OneDrive\\m\\Documentos\\MiElectronica.mdf;Integrated Security=True;Connect Timeout=30";
				using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO productos " +
                        "(nombre,stock,cantidad_producto,fecha_creacion) VALUES" +
                        "(@nombre,@stock,@cantidad_producto,@fecha_creacion);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("nombre", ProductosInfo.nombre);
						command.Parameters.AddWithValue("stock", ProductosInfo.stock);
						command.Parameters.AddWithValue("cantidad_producto", ProductosInfo.cantidad_producto);
						command.Parameters.AddWithValue("fecha_creacion", ProductosInfo.fecha_creacion);

                        command.ExecuteNonQuery();

					}
                }

			}
            catch (Exception ex) 
            {
                errorMessage = ex.Message;
                return;
            }
            ProductosInfo.nombre = ""; ProductosInfo.stock = ""; ProductosInfo.cantidad_producto = "";
            successMessage = "Producto registrado correctamente";
            return;

			Response.Redirect("/Productos/Index");
        }

    }
}
