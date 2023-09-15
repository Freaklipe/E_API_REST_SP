using E_API_REST_SP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace E_API_REST_SP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        // Faltan las validacion y control de errores
        public readonly string con;

        public ProductosController(IConfiguration configuration)
        {
            con = configuration.GetConnectionString("conexionSQL");
        }

        [HttpGet]
        public IEnumerable<ProductoModel> Get()
        {
            List<ProductoModel> productos = new();

            using (SqlConnection connection = new(con))
            {
                connection.Open();
                using SqlCommand cmd = new("ObtenerProductos", connection);
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using SqlDataReader reader = cmd.ExecuteReader();
                    {
                        while (reader.Read())
                        {
                            ProductoModel producto = new ()
                            {
                                _id = Convert.ToInt32(reader["Id"]),
                                _nombre = reader["Nombre"].ToString(),
                                _precio = Convert.ToDecimal(reader["Precio"]),
                                _cantidad = Convert.ToInt32(reader["Cantidad"]),
                                _descripcion = reader["Descripcion"].ToString(),
                                _fechaCreacion = Convert.ToDateTime(reader["FechaCreacion"])
                            };
                            productos.Add(producto);
                        }
                    }
                }
            }
            return productos;
        }

        [HttpPost]
        public void Post([FromBody] ProductoModel producto)
        {
            using SqlConnection connection = new(con);
            {
                connection.Open();
                using SqlCommand cmd = new("InsertarProducto", connection);
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", producto._nombre);
                    cmd.Parameters.AddWithValue("@Precio", producto._precio);
                    cmd.Parameters.AddWithValue("@Cantidad", producto._cantidad);
                    cmd.Parameters.AddWithValue("@Descripcion", producto._descripcion);
                    cmd.Parameters.AddWithValue("@FechaCreacion", producto._fechaCreacion);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        [HttpPut("{id}")]
        public void Put([FromBody] ProductoModel producto, int id)
        {
            using SqlConnection connection = new(con);
            {
                connection.Open();
                using SqlCommand cmd = new("ActualizarProducto", connection);
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Nombre", producto._nombre);
                    cmd.Parameters.AddWithValue("@Precio", producto._precio);
                    cmd.Parameters.AddWithValue("@Cantidad", producto._cantidad);
                    cmd.Parameters.AddWithValue("@Descripcion", producto._descripcion);
                    cmd.Parameters.AddWithValue("@FechaCreacion", producto._fechaCreacion);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using SqlConnection connection = new(con);
            {
                connection.Open();
                using SqlCommand cmd = new("EliminarProducto", connection);
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }   
}
