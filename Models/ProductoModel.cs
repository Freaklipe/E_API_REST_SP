namespace E_API_REST_SP.Models
{
    public class ProductoModel
    {
        public int _id { get; set; }
        public string? _nombre { get; set; }
        public decimal _precio { get; set; }
        public int _cantidad { get; set; }
        public string? _descripcion{ get; set; }
        public DateTime _fechaCreacion { get; set; }

    }
}
