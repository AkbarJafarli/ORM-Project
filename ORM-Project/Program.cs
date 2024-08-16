using ORM_Project.Context;
using ORM_Project.Dtos.ProductDtos;
using ORM_Project.Dtos.UserDtos;
using ORM_Project.Enum;
using ORM_Project.Exceptions;
using ORM_Project.Models;
using ORM_Project.Services;

UserService userService = new UserService();
User activeUser = new User("admin@gmail.com", "admin123");
ProductService productService = new ProductService();
var context = new AppDbContext();
OrderService orderService = new OrderService(context);
var context1 = new AppDbContext();
PaymentService paymentService = new PaymentService(context1);

Console.WriteLine("Welcome to the WOLT application.");
Console.WriteLine("Menu:");
Console.WriteLine("");
restart:
Console.WriteLine("[1]Registration");
Console.WriteLine("[2]Login");
Console.WriteLine("");
Console.Write("Select:");
if (!int.TryParse(Console.ReadLine(), out int choice))
{
    Console.WriteLine("Invalid input,please enter a number");
    goto restart;
}
switch (choice)
{
    case 1:
        bool isCorrect = false;
        bool isCorrect2 = false;
        bool isUpper = false;
        bool isCorrect3 = false;
    restart7:
        Console.Write("Enter Fullname:");
        string fullname = Console.ReadLine();
        if (char.IsUpper(fullname[0]))
        {
            isUpper = true;
        }
        else
        {
            Console.WriteLine("The name must begin with a capital letter");
            goto restart7;
        }
        if (fullname.Length >= 3)
        {
            isCorrect2 = true;
        }
        else
        {
            Console.WriteLine("Name length must be longer than 3");
            goto restart7;
        }
    restart6:
        Console.Write("Enter Email:");
        string email = Console.ReadLine();
        if (email.Contains("@"))
        {
            isCorrect = true;
        }
        else
        {
            Console.WriteLine("You must use the @ symbol");
            goto restart6;
        }
    restart8:
        Console.Write("Enter Password:");
        string password = Console.ReadLine();
        if (password.Length >= 8)
        {
            isCorrect3 = true;
        }
        else
        {
            Console.WriteLine("Password length must be greater than 7");
            goto restart8;
        }
        Console.Write("Enter Address:");
        string address = Console.ReadLine();
        RegisterDto registerDto = new RegisterDto(fullname, email, password, address);
        await userService.RegisterUserAsync(registerDto);
        Console.WriteLine("");
        Console.WriteLine("Register is succesfull.");
        goto restart;

    case 2:
        Console.Write("Enter Email:");
        string email2 = Console.ReadLine();
        Console.Write("Enter Password:");
        string password2 = Console.ReadLine();
        LoginDto loginDto = new LoginDto(email2, password2);
        try
        {
            activeUser = await userService.LoginUserAsync(loginDto);
            Console.WriteLine("");
            Console.WriteLine($"Welcome {activeUser.Fullname}");
            break;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        goto restart; ;

    default: goto restart;
}
restart2:
while (true)
{
    Console.WriteLine("");
    Console.WriteLine("[1]Add Product");
    Console.WriteLine("[2]Update Product");
    Console.WriteLine("[3]Delete Product");
    Console.WriteLine("[4]Get Products");
    Console.WriteLine("[5]Search Products");
    Console.WriteLine("[6]Get Product by Id");
    Console.WriteLine("[7]Order management menu:");
    Console.WriteLine("[0]Exit");
    Console.WriteLine("");
    Console.Write("Select:");
    if (!int.TryParse(Console.ReadLine(), out int choice2))
    {
        Console.WriteLine("Invalid input,please enter a number");
        goto restart2;
    }
    switch (choice2)
    {
        case 1:
            ProductCreateDto productCreateDto = new ProductCreateDto();
            Console.Write("Enter product name:");
            string productname = Console.ReadLine();
        restart8:
            Console.Write("Enter product price:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal productprice))
            {
                Console.WriteLine("Invalid input,please enter a correct price");
                goto restart8;
            }
        restart9:
            Console.Write("Enter product stock count:");
            if (!int.TryParse(Console.ReadLine(), out int productstock))
            {
                Console.WriteLine("Invalid input,please enter a correct stock count");
                goto restart9;
            }
            Console.Write("Enter product description:");
            string productdescription = Console.ReadLine();

            productCreateDto.ProductName = productname;
            productCreateDto.ProductPrice = productprice;
            productCreateDto.ProductStock = productstock;
            productCreateDto.ProductDescription = productdescription;
            await productService.CreateAsync(productCreateDto);
            Console.WriteLine("");
            Console.WriteLine("Product succesfully added");
            break;

        case 2:
            ProductUpdateDto productUpdateDto = new ProductUpdateDto();
            Product product = new Product();
        restart3:
            Console.Write("Enter product id:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid input,please enter a number");
                goto restart3;
            }
            Console.Write("Enter product name:");
            string ProductName = Console.ReadLine();
        restart22:
            Console.Write("Enter product price:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal ProductPrice))
            {
                Console.WriteLine("Invalid input please enter a number");
                goto restart22;
            }
        restart23:
            Console.Write("Enter product stock count:");
            if (!int.TryParse(Console.ReadLine(), out int ProductStock))
            {
                Console.WriteLine("Invalid input please enter a number");
                goto restart23;
            }
            Console.Write("Enter product description:");
            string ProductDescription = Console.ReadLine();

            productUpdateDto.Id = id;
            productUpdateDto.ProductName = ProductName;
            productUpdateDto.ProductPrice = ProductPrice;
            productUpdateDto.ProductStock = ProductStock;
            productUpdateDto.ProductDescription = ProductDescription;
            await productService.UpdateAsync(productUpdateDto);
            Console.WriteLine("");
            Console.WriteLine("Product succesfully updated");
            break;

        case 3:
        restart4:
            Console.Write("Enter product id:");
            if (!int.TryParse(Console.ReadLine(), out int id2))
            {
                Console.WriteLine("Invalid input,please enter a number");
                goto restart4;
            }
            await productService.DeleteAsync(id2);
            Console.WriteLine("");
            Console.WriteLine("Product succesfully deleted");
            break;

        case 4:
            Console.WriteLine("");
            Console.WriteLine("Products:");
            Console.WriteLine("");
            var products = await productService.GetProductAsync();
            foreach (var product1 in products)
            {
                Console.WriteLine($"Product name:{product1.Name} - Product price:{product1.Price} - Product description:{product1.Description} - Created date:{product1.CreatedDate}");
            }
            break;

        case 5:
            try
            {
                Console.WriteLine("");
                Console.Write("Enter product name:");
                string seacrh = Console.ReadLine();
                var products1 = await productService.SearchProductAsync(seacrh);
                Console.WriteLine("");
                foreach (var products2 in products1)
                {
                    Console.WriteLine($"Product id:{products2.Id} - Product name:{products2.Name} - Product price:{products2.Price} - Product description:{products2.Description} - Created date:{products2.CreatedDate}");
                }
                break;
            }
            catch (InvalidProductException ex)
            {
                throw new InvalidProductException("Product not found");
            }

        case 6:
        restart5:
            Console.Write("Enter product id:");
            if (!int.TryParse(Console.ReadLine(), out int id3))
            {
                Console.WriteLine("Invalid input,please enter a number");
                goto restart5;
            }
            try
            {
                var product2 = await productService.GetProductByIdAsync(id3);
                Console.WriteLine($"Product id:{product2.Id} - Product name:{product2.Name} - Product price:{product2.Price} - Product description:{product2.Description} - Created date:{product2.CreatedDate}");
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            break;

        case 0:
            goto restart;

        case 7:
        restart11:
            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("Menu:");
                Console.WriteLine("");
                Console.WriteLine("[1]Create order");
                Console.WriteLine("[2]Cancel order");
                Console.WriteLine("[3]Add order detail");
                Console.WriteLine("[4]Complete order");
                Console.WriteLine("[5]Get orders");
                Console.WriteLine("[6]Get order detail by order id");
                Console.WriteLine("[7]Payment");
                Console.WriteLine("[0]Exit");
                Console.WriteLine("");
                Console.Write("Select:");
                if (!int.TryParse(Console.ReadLine(), out int choice3))
                {
                    Console.WriteLine("Invalid input,please enter a number");
                    goto restart11;
                }
                switch (choice3)
                {
                    case 1:
                    restart10:

                        var order = new Order
                        {
                            UserId = activeUser.Id,
                            OrderDate = DateTime.Now,
                            TotalAmount = 0,
                            Status = OrderStatus.Pending
                        };
                        try
                        {

                            await orderService.CreateOrderAsync(order);
                            Console.WriteLine("Order created succesfully");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            break;
                        }
                        break;

                    case 2:
                    restart12:
                        Console.Write("Select Order:");
                        if (!int.TryParse(Console.ReadLine(), out int orderId))
                        {
                            Console.WriteLine("Invalid input,please enter a number");
                            goto restart12;
                        }
                        try
                        {
                            await orderService.CancelOrderAsync(orderId);
                            Console.WriteLine("Order canceled succesfully");
                        }
                        catch (NotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                            break;
                        }
                        break;

                    case 3:
                    restart13:
                        Console.Write("Enter order id:");

                        if (!int.TryParse(Console.ReadLine(), out int orderId2))
                        {
                            Console.WriteLine("Invalid input,please enter a number");
                            goto restart13;
                        }
                    restart14:
                        Console.Write("Enter product id:");
                        if (!int.TryParse(Console.ReadLine(), out int productId))
                        {
                            Console.WriteLine("Invalid input,please enter a number");
                            goto restart14;
                        }
                    restart15:
                        Console.Write("Enter product quantity:");
                        if (!int.TryParse(Console.ReadLine(), out int quantity))
                        {
                            Console.WriteLine("Invalid input,please enter a number");
                            goto restart15;
                        }
                        var orderDetail = new OrderDetail
                        {
                            OrderId = orderId2,
                            ProductId = productId,
                            Quantity = quantity,

                        };

                        await orderService.AddOrderDetailAsync(orderDetail);
                        Console.WriteLine("Order Detail successfully added");
                        break;

                    case 4:
                    restart16:
                        Console.Write("Enter order id:");
                        if (!int.TryParse(Console.ReadLine(), out int orderId3))
                        {
                            Console.WriteLine("Invalid input,please enter a number");
                            goto restart16;
                        }
                        var orderDetail2 = new OrderDetail
                        {
                            OrderId = orderId3
                        };
                        try
                        {
                            await orderService.CompleteOrderAsync(orderId3);
                            Console.WriteLine("Order completed succesfully");
                        }
                        catch (NotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                            break;
                        }
                        break;

                    case 5:
                        var orders = await orderService.GetOrderAsync();
                        foreach (var order1 in orders)
                        {
                            Console.WriteLine($"Id:{order1.Id} - Date:{order1.OrderDate} - Amount:{order1.TotalAmount} Status {order1.Status.ToString()}");
                            foreach (var orderDetail1 in order1.OrderDetails)
                            {
                                Console.WriteLine($"----Name:{orderDetail1.Product.Name} - Quantity:{orderDetail1.Quantity} - Price:{orderDetail1.PricePerItem}");
                            }
                        }
                        break;

                    case 6:
                    restart17:
                        Console.Write("Enter order id:");
                        if (!int.TryParse(Console.ReadLine(), out int orderId4))
                        {
                            Console.WriteLine("Invalid input,please enter a number");
                            goto restart17;
                        }
                        var orderDetails = await orderService.GetOrderDetailsByOrderIdAsync(orderId4);
                        foreach (var details in orderDetails)
                        {
                            Console.WriteLine($"Id:{details.Id} - Quantity:{details.Quantity} - Price per item:{details.PricePerItem}");
                        }
                        break;

                    case 0:
                        goto restart2;

                    case 7:
                    restart18:
                        while (true)
                        {
                            Console.WriteLine("Menu:");
                            Console.WriteLine("");
                            Console.WriteLine("[1]Make payment");
                            Console.WriteLine("[2]Get payments");
                            Console.WriteLine("[0]Exit");
                            Console.WriteLine("");
                        restart19:
                            Console.Write("Select:");
                            if (!int.TryParse(Console.ReadLine(), out int choice4))
                            {
                                Console.WriteLine("Invalid input,please enter a number");
                                goto restart19;
                            }
                            switch (choice4)
                            {
                                case 1:
                                restart20:
                                    Console.Write("Enter order id:");
                                    if (!int.TryParse(Console.ReadLine(), out int paymentOrderId))
                                    {
                                        Console.WriteLine("Invalid input,please enter a number");
                                        goto restart20;
                                    }
                                restart21:
                                    Console.Write("Enter amount:");
                                    if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                                    {
                                        Console.WriteLine("Invalid input,please enter a number");
                                        goto restart21;
                                    }
                                    var payment = new Payment
                                    {
                                        OrderId = paymentOrderId,
                                        Amount = amount,
                                        PaymentDate = DateTime.Now,
                                    };
                                    try
                                    {
                                        await paymentService.MakePaymentAsync(payment);
                                        Console.WriteLine("Payment succesfully made");
                                    }
                                    catch (NotFoundException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;

                                case 2:
                                    var payments = await paymentService.GetPaymentsAsync();
                                    foreach (var pay in payments)
                                    {
                                        Console.WriteLine($"Id:{pay.Id} - Order id:{pay.OrderId} - Amount:{pay.Amount} - Date:{pay.PaymentDate}");
                                    }
                                    break;

                                case 0:
                                    goto restart11;

                                default: goto restart18;
                            }

                        }
                    default: goto restart11;
                }
            }
        default: goto restart2;
    }
}







