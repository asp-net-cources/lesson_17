using MySqlConnector;
using System.Data;
using System.Data.Common;
using Lesson17.Data.Models;

namespace Lesson17.Data.Ado;

public class RawSqlDataContext : IDataContext
{
	private readonly string _connectionString;

	public RawSqlDataContext(string connectionString) {
        _connectionString = connectionString;
    }

    public IList<Customer?> SelectCustomers() {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = "SELECT id, first_name, last_name, age, country FROM Customers;";

        var command = new MySqlCommand(query, connection);

        using var reader = command.ExecuteReader();

        var result = new List<Customer?>();

        while (reader.Read()) {
            var customer = new Customer {
                Id = (int)reader[0],
                FirstName = (string)reader[1],
                LastName = (string)reader[2],
                Age = (int)reader[3],
                Country = (string)reader[4]
            };

            result.Add(customer);
        }

        return result;
    }

    public IList<Order?> SelectOrders() {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = "SELECT order_id, customer_id, product_id, count, created_at FROM Orders;";

        var command = new MySqlCommand(query, connection);

        using var reader = command.ExecuteReader();

        var result = new List<Order?>();

        while (reader.Read()) {
            var order = new Order {
                OrderId = (int)reader["order_id"],
                CustomerId = (int)reader["customer_id"],
                ProductId = (int)reader["product_id"],
                Count = (int)reader["count"],
                CreatedAt = (DateTime)reader["created_at"]
            };

            result.Add(order);
        }

        return result;
    }

    public IList<Product?> SelectProducts() {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = "SELECT id, name, description, price, product_type FROM Products;";

        var command = new MySqlCommand(query, connection);

        using var reader = command.ExecuteReader();

        var result = new List<Product?>();

        while (reader.Read()) {
            var productTypeRaw = reader.GetString(4);
            Enum.TryParse<ProductType>(productTypeRaw, out var productType);

            var product = new Product {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Description = reader.GetString(2),
                Price = reader.IsDBNull(3) ? 0 : reader.GetDouble(3),
                ProductType = productType
            };

            result.Add(product);
        }

        return result;
    }

    public int InsertProduct(Product product) {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        var query = "INSERT INTO Products (id, name, description, price, product_type) VALUES (@id, @name, @description, @price, @product_type);";
        var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("id", product.Id);
        command.Parameters.AddWithValue("name", product.Name);
        command.Parameters.AddWithValue("description", product.Description);
        command.Parameters.AddWithValue("price", product.Price);
        command.Parameters.AddWithValue("product_type", product.ProductType.ToString());
        return command.ExecuteNonQuery();
    }

    public int UpdateProduct(Product product) {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        var query = @"
            UPDATE Products
                SET name = @name,
                    description = @description,
                    price = @price,
                    product_type = @product_type
            WHERE id = @id;
        ";

        var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("id", product.Id);
        command.Parameters.AddWithValue("name", product.Name);
        command.Parameters.AddWithValue("description", product.Description);
        command.Parameters.AddWithValue("price", product.Price);
        command.Parameters.AddWithValue("product_type", product.ProductType);

        return command.ExecuteNonQuery();
    }

    public int DeleteProduct(int id) {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        var query = "DELETE FROM Products WHERE id = @id;";
        var command = new MySqlCommand(query, connection);

        var param = new MySqlParameter {
            ParameterName = "id",
            Value = id
        };

        command.Parameters.AddWithValue("id", id);
        return command.ExecuteNonQuery();
    }

    public int InsertCustomer(Customer customer) => throw new NotImplementedException();
    public int UpdateCustomer(Customer customer) => throw new NotImplementedException();
    public int DeleteCustomer(int id) => throw new NotImplementedException();
    public int InsertOrder(Order order) => throw new NotImplementedException();
    public int UpdateOrder(Order order) => throw new NotImplementedException();
    public int DeleteOrder(int id) => throw new NotImplementedException();
}
