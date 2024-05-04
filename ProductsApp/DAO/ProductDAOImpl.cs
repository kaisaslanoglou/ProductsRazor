using System.Data.SqlClient;
using ProductsApp.DAO;
using ProductsApp.Models;
using ProductsApp.Services;

namespace ProductsApp.DAO
{
    public class ProductDAOImpl : IProductDAO
    {
        public void Delete(int id)
        {
            string sql = "DELETE FROM PRODUCTS WHERE ID = @id";

            using SqlConnection? conn = DBHelper.GetConnection();
            if (conn is not null) conn.Open();

            using SqlCommand command = new(sql, conn);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteReader();

        }

        public IList<Product> GetAll()
        {
            string sql = "SELECT * FROM PRODUCTS";
            var products = new List<Product>();
            Product? product;

            using SqlConnection? conn = DBHelper.GetConnection();
            if (conn is not null) conn.Open();

            using SqlCommand command = new(sql, conn);
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                product = new()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ID")),
                    Name = reader.GetString(reader.GetOrdinal("NAME")),
                    Price = reader.GetInt32(reader.GetOrdinal("PRICE")),
                    Quantity = reader.GetInt32(reader.GetOrdinal("QUANTITY")),
                    Colour = reader.GetString(reader.GetOrdinal("COLOUR"))
                };
                products.Add(product);
            }
            return products;
        }

        public Product? GetByID(int id)
        {
            string sql = "SELECT * FROM PRODUCTS WHERE ID = @id";
            Product? product = null;

            using SqlConnection? conn = DBHelper.GetConnection();
            if (conn is not null) conn.Open();

            using SqlCommand command = new(sql, conn);
            command.Parameters.AddWithValue("@id", id);

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                product = new()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ID")),
                    Name = reader.GetString(reader.GetOrdinal("NAME")),
                    Price = reader.GetInt32(reader.GetOrdinal("PRICE")),
                    Quantity = reader.GetInt32(reader.GetOrdinal("QUANTITY")),
                    Colour = reader.GetString(reader.GetOrdinal("COLOUR"))
                };
            }
            return product;
        }




        public Product? Insert(Product? product)
        {
            if (product == null) return null;
            string sql = "INSERT INTO PRODUCTS (NAME, PRICE, QUANTITY, COLOUR) VALUES (@name, @price, @quantity, @colour)" +
                "SELECT SCOPE_IDENTITY();";

            Product? productToReturn = null;
            int insertedId = 0;

            using SqlConnection? conn = DBHelper.GetConnection();
            if (conn is not null) conn.Open();

            using SqlCommand command = new(sql, conn);
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@price", product.Price);
            command.Parameters.AddWithValue("@quantity", product.Quantity);
            command.Parameters.AddWithValue("@colour", product.Colour);

            object insertedObj = command.ExecuteScalar();
            if(insertedObj is not null)
            {
                if (!int.TryParse(insertedObj.ToString(), out insertedId))
                {
                    throw new Exception("Error in insert id");
                }
            }

            string sqlSelect = "SELECT * FROM PRODUCTS WHERE ID = @id";

            using SqlCommand sqlCommand = new(sqlSelect, conn);
            sqlCommand.Parameters.AddWithValue("@id", insertedId);

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                productToReturn = new()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ID")),
                    Name = reader.GetString(reader.GetOrdinal("NAME")),
                    Price = reader.GetInt32(reader.GetOrdinal("PRICE")),
                    Quantity = reader.GetInt32(reader.GetOrdinal("QUANTITY")),
                    Colour = reader.GetString(reader.GetOrdinal("COLOUR"))
                };
            }
            return productToReturn;

        }

        public Product? Update(Product? product)
        {
            if (product == null) return null;
            string sql = "UPDATE PRODUCTS SET NAME = @name, PRICE = @price, QUANTITY = @quantity, COLOUR = @colour WHERE ID = @id"; 
             
            Product? productToReturn = product;

            using SqlConnection? conn = DBHelper.GetConnection();
            if (conn is not null) conn.Open();

            using SqlCommand command = new(sql, conn);
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@price", product.Price);
            command.Parameters.AddWithValue("@quantity", product.Quantity);
            command.Parameters.AddWithValue("@colour", product.Colour);
            command.Parameters.AddWithValue("@id", product.Id);

            command.ExecuteNonQuery();
            return productToReturn;

        }
    }
}
