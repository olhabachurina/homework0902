
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using homework0902;
using homework0902.Models;

namespace homework0902;

class Program
{
    static async Task Main()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        var serviceProvider = services.BuildServiceProvider();

        var adminService = serviceProvider.GetService<IAdminService>();
        var userService = serviceProvider.GetService<IUserService>();
        while (true)
        {
            Console.WriteLine("1. Получить список товаров");
            Console.WriteLine("2. Получить товар по ID");
            Console.WriteLine("3. Создать товар");
            Console.WriteLine("4. Выход");

            Console.Write("Выберите действие: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await DisplayProducts(userService);
                    break;
                case "2":
                    await DisplayProductById(userService);
                    break;
                case "3":
                    await AddProduct(adminService);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    static async Task DisplayProducts(IUserService userService)
    {
        try
        {
            var products = await userService.GetProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Не удалось получить список товаров: {ex.Message}");
        }
    }

    static async Task DisplayProductById(IUserService userService)
    {
        Console.Write("Введите ID товара: ");
        var productId = int.Parse(Console.ReadLine());
        try
        {
            var product = await userService.GetProductById(productId);
            Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Не удалось найти товар: {ex.Message}");
        }
    }

    static async Task AddProduct(IAdminService adminService)
    {
        Console.Write("Введите название товара: ");
        var name = Console.ReadLine();
        Console.Write("Введите цену товара: ");
        var price = decimal.Parse(Console.ReadLine());

        var newProduct = new Product { Name = name, Price = price };

        try
        {
            await adminService.AddProduct(newProduct);
            Console.WriteLine("Товар успешно создан.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Не удалось создать товар: {ex.Message}");
        }


        //        // Создаем два экземпляра товара
        var productId = 1;
        var product1 = await adminService.GetProductById(productId);
        var product2 = await adminService.GetProductById(productId);

        // Администратор 1 изменяет один экземпляр
//        product1.Price += 19;
//        try
//        {
//            await adminService.UpdateProduct(productId, product1);
//        }
//        catch (DbUpdateException ex)
//        {
//            Console.WriteLine($"Ошибка при обновлении товара: {ex.InnerException?.Message}");
//        }

//        // Администратор 2 изменяет другой экземпляр
//        product2.Price -= 9;
//        try
//        {
//            await adminService.UpdateProduct(productId, product2);
//        }
//        catch (DbUpdateException ex)
//        {
//            Console.WriteLine($"Ошибка при обновлении товара: {ex.InnerException?.Message}");
//        }
//    }
//        public static void ConfigureServices(IServiceCollection services)
//    {
//        services.AddDbContext<OnlineStoreContext>(options =>
//            options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=OnlainStore;Trusted_Connection=True;TrustServerCertificate=True;"));
//        services.AddTransient<IAdminService, AdminService1>();
//        services.AddTransient<IUserService, UserService>();
//    }
//}
public class UserService : IUserService
{
    private readonly OnlineStoreContext _context;
    private string customerId;
    private string shippingAddress;
    public UserService(OnlineStoreContext context)
    {
        _context = context;
    }
    public async Task<Product> GetProductById(int productId)
    {
        return await _context.Products.FindAsync(productId);
    }
    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }


    public async Task<Order> PlaceOrder(int productId, int quantity)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null)
        {
            throw new ArgumentException("Товар не найден", nameof(productId));
        }

        if (product.Quantity < quantity)
        {
            throw new ArgumentException("Недостаточное количество товара", nameof(quantity));
        }

        // Создание заказа
        var order = new Order
        {
            CustomerId = customerId,
            OrderDate = DateTime.UtcNow,
            TotalPrice = product.Price * quantity,
            Status = "Pending",
            ShippingAddress = shippingAddress,
            OrderItems = new List<OrderItem>
        {
            new OrderItem
            {
                ProductId = productId,
                Quantity = quantity,
               PriceAtOrder = product.Price
            }
        }
        };

        return order;
    }
}

public static void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<OnlineStoreContext>(options =>
        options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=OnlainStore;Trusted_Connection=True;TrustServerCertificate=True;"));
    services.AddTransient<IAdminService, AdminService1>();
    services.AddTransient<IUserService, UserService>();
}