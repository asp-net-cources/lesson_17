using MySqlConnector;
using System.Data;
using Lesson17.Data.Models;

namespace Lesson17.Data.Ado;

public class AdoDataContext : IDataContext
{
    private readonly DataSet _dataSet = new DataSet();
    private readonly string _connectionString;

    private DataTable? Customers { get => _dataSet.Tables["Customers"]; }
    private DataTable? Products { get => _dataSet.Tables["Products"]; }
    private DataTable? Orders { get => _dataSet.Tables["Orders"]; }

    public AdoDataContext(string connectionString) {
        _connectionString = connectionString;
        Init();
    }

    public int DeleteCustomer(int id) => throw new NotImplementedException();
    public int DeleteOrder(int id) => throw new NotImplementedException();
    public int DeleteProduct(int id) {
        var row = Products?.Select($"id = {id}").FirstOrDefault();

        if (row == null) {
            return 0;
        }

        row.Delete();
        Init();

        return 1;
    }

    public int InsertCustomer(Customer customer) => throw new NotImplementedException();
    public int InsertOrder(Order order) => throw new NotImplementedException();
    public int InsertProduct(Product product) {
        if (Products == null) {
            return 0;
        }

        var row = Products.NewRow();
        row["id"] = product.Id;
        row["name"] = product.Name;
        row["description"] = product.Description;
        row["price"] = product.Price;
        row["product_type"] = product.ProductType.ToString();

        Products.Rows.Add(row);

        Init();

        return 1;
    }

    public IList<Customer?> SelectCustomers() {
        var result = new List<Customer?>();

        if (Customers == null) {
            return result;
        }

        foreach (DataRow row in Customers.Rows) {
            var customer = new Customer {
                Id = (int)row[0],
                FirstName = (string)row[1],
                LastName = (string)row[2],
                Age = (int)row[3],
                Country = (string)row[4]
            };

            result.Add(customer);
        }

        return result;
    }

    public IList<Order?> SelectOrders() => throw new NotImplementedException();

    public IList<Product?> SelectProducts() {
        var result = new List<Product?>();

        if (Products == null) {
            return result;
        }

        var products = from product in Products.AsEnumerable()
                       select dataRowToProduct(product);

        return products.ToList();
    }

    public int UpdateCustomer(Customer customer) => throw new NotImplementedException();
    public int UpdateOrder(Order order) => throw new NotImplementedException();
    public int UpdateProduct(Product product) {
        var row = Products?.Select($"id = {product.Id}").FirstOrDefault();

        if (row == null) {
            return 0;
        }

        row["name"] = product.Name;
        row["description"] = product.Description;
        row["price"] = product.Price;
        row["product_type"] = product.ProductType;

        using var connection = new MySqlConnection(_connectionString);
        var productsAdapter = new MySqlDataAdapter();
        var query = @"
            UPDATE Products
                SET name = @name,
                    description = @description,
                    price = @price,
                    product_type = @product_type
            WHERE id = @id;
        ";

        productsAdapter.UpdateCommand = new MySqlCommand(query, connection);
        productsAdapter.UpdateCommand.Parameters.Add("id", DbType.Int32).SourceColumn = "id";
        productsAdapter.UpdateCommand.Parameters.Add("name", DbType.Int32).SourceColumn = "name";
        productsAdapter.UpdateCommand.Parameters.Add("description", DbType.Int32).SourceColumn = "description";
        productsAdapter.UpdateCommand.Parameters.Add("price", DbType.Double).SourceColumn = "price";
        productsAdapter.UpdateCommand.Parameters.Add("product_type", DbType.String).SourceColumn = "product_type";

        productsAdapter.Update(Products!);

        Init();

        return 1;
    }

    private void Init() {
        _dataSet.Clear();

        using var connection = new MySqlConnection(_connectionString);
        var customersAdapter = new MySqlDataAdapter("SELECT * FROM Customers", connection);
        var productsAdapter = new MySqlDataAdapter("SELECT * FROM Products", connection);
        var ordersAdapter = new MySqlDataAdapter("SELECT * FROM Orders", connection);
        
        customersAdapter.Fill(_dataSet, "Customers");
        productsAdapter.Fill(_dataSet, "Products");
        ordersAdapter.Fill(_dataSet, "Orders");

        //_dataSet.Relations.Add(new DataRelation("FK_Order_Customer", Customers.Columns["id"], Orders.Columns["customer_id"]));
    }

    private Product dataRowToProduct(DataRow row) {
        var productTypeRaw = (string)row["product_type"];
        Enum.TryParse<ProductType>(productTypeRaw, out var productType);

        var product = new Product {
            Id = (int)row[0],
            Name = (string)row[1],
            Description = (string)row[2],
            Price = row.IsNull(3) ? 0 : (double)row[3],
            ProductType = productType
        };

        return product;
    }
}
