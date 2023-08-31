using Lesson17.Data.Models;

namespace Lesson17.Data;

public interface IDataContext
{
    public IList<Product?> SelectProducts();
    public int InsertProduct(Product product);
    public int UpdateProduct(Product product);
    public int DeleteProduct(int id);

    public IList<Customer?> SelectCustomers();
    public int InsertCustomer(Customer customer);
    public int UpdateCustomer(Customer customer);
    public int DeleteCustomer(int id);

    public IList<Order?> SelectOrders();
    public int InsertOrder(Order order);
    public int UpdateOrder(Order order);
    public int DeleteOrder(int id);
}
