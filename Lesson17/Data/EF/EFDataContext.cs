// using Microsoft.EntityFrameworkCore;
// using System.ComponentModel.DataAnnotations.Schema;
// using System.ComponentModel.DataAnnotations;
// using Lesson17.Data.Models;
//
// namespace Lesson17.Data.EF;
//
// public class EFDataContext : DbContext, IDataContext {
//     public DbSet<Customer> Customers { get; set; }
//     public DbSet<Order> Orders { get; set; }
//     public DbSet<Product> Products { get; set; }
//
//     public EFDataContext() {
//         // Database.EnsureDeleted();
//         // Database.EnsureCreated();
//     }
//
//     public IList<Customer?> SelectCustomers() {
//         return Customers.Cast<Customer?>().ToList();
//     }
//     public int InsertCustomer(Customer customer) {
//         Customers.Add(customer);
//         return SaveChanges();
//     }
//
//     public int UpdateCustomer(Customer customer) {
//         var foundCustomer = Customers.Find(customer.Id);
//
//         if (foundCustomer == null) { return 0; }
//
//         foundCustomer.FirstName = customer.FirstName;
//         foundCustomer.LastName = customer.LastName;
//         foundCustomer.Age = customer.Age;
//         foundCustomer.Country = customer.Country;
//
//         return SaveChanges();
//     }
//
//     public int DeleteCustomer(int id) {
//         var foundCustomer = Customers.AsNoTracking().FirstOrDefault(row => row.Id == id);
//         if (foundCustomer == null) { return 0; }
//         Customers.Remove(foundCustomer);
//         return SaveChanges();
//     }
//
//     public IList<Order?> SelectOrders() => throw new NotImplementedException();
//     public int InsertOrder(Order order) => throw new NotImplementedException();
//     public int UpdateOrder(Order order) => throw new NotImplementedException();
//     public int DeleteOrder(int id) => throw new NotImplementedException();
//
//     public IList<Product?> SelectProducts() => Products.ToList();
//     public int InsertProduct(Product product) => throw new NotImplementedException();
//     public int UpdateProduct(Product product) {
//         var foundCustomer = Products.AsNoTracking().FirstOrDefault(row => row.Id == pro);
//         if (foundCustomer == null) { return 0; }
//         Products.Remove(foundCustomer);
//         return SaveChanges();
//     }
//     
//     public int DeleteProduct(int id) {
//         var foundCustomer = Products.AsNoTracking().FirstOrDefault(row => row.Id == id);
//         if (foundCustomer == null) { return 0; }
//         Products.Remove(foundCustomer);
//         return SaveChanges();
//     }
//
//     protected override void OnModelCreating(ModelBuilder modelBuilder) {
//         modelBuilder.Entity<Product>()
//             .Property(product => product.ProductType)
//             .HasConversion(
//                 productType => productType.ToString(),
//                 productType => (ProductType)Enum.Parse(typeof(ProductType), productType)
//             );
//     }
//
//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
//         optionsBuilder.UseMySQL("Datasource=localhost;Database=shop;User=root;Password=root123;");
//     }
// }
