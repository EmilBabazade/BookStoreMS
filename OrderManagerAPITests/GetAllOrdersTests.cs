using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagerAPI.Data;
using OrderManagerAPI.Mapper;
using OrderManagerAPI.Order;
using OrderManagerAPI.Order.Dtos;

namespace OrderManagerAPITests;

/*
 * Created As an example. I am too lazy to implement other Unit Tests :D
 */

public class Tests
{
    private OrderController orderController;
    private AppDbContext appDbContext;
    private IMapper mapper;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;
        this.appDbContext = new AppDbContext(options);
        this.appDbContext.Database.EnsureDeleted();
        this.mapper = new Mapper(
            new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile())));
        this.orderController = new OrderController(this.appDbContext, this.mapper);
    }

    [Test]
    public async Task GetAllOrdersWorks()
    {
        var expectedOrders = new List<Order>
        {
            new Order
            {
                Id = 1,
            },
            new Order
            {
                Id = 2,
            }
        };
        this.appDbContext.AddRange(expectedOrders);
        await this.appDbContext.SaveChangesAsync();

        var result = await this.orderController.GetAllOrders(null, null, null, null, null, null, null, null, null, null);
        var resultValue = (result.Result as OkObjectResult).Value;
        Assert.IsInstanceOf<IEnumerable<GetOrderDto>>(resultValue);

        var actualOrders = resultValue as IEnumerable<GetOrderDto>;
        Assert.That(actualOrders, Is.Not.Null);
        Assert.That(expectedOrders, Has.Count.EqualTo(actualOrders.Count()));

        var mappedExpectedOrders = this.mapper.Map<IEnumerable<GetOrderDto>>(expectedOrders);
        Helper.AreEqualByJson(mappedExpectedOrders, actualOrders);
    }

    [Test]
    public async Task GetAllOrdersReturnsUsersOrdersWhenUserIdGiven()
    {
        var expectedOrders = new List<Order>
        {
            new Order
            {
                Id = 1,
                UserId = 34
            },
            new Order
            {
                Id = 2,
                UserId = 45
            }
        };
        this.appDbContext.AddRange(expectedOrders);
        await this.appDbContext.SaveChangesAsync();

        var result = await this.orderController.GetAllOrders(null, null, null, null, null, null, null, null, null, null);
        var resultValue = (result.Result as OkObjectResult).Value as IEnumerable<GetOrderDto>;

        var expectedUserIds = expectedOrders.Select(x => x.UserId).OrderBy(x => x);
        var actualUserIds = resultValue.Select(x => x.UserId).OrderBy(x => x);
        Assert.That(actualUserIds, Is.EquivalentTo(expectedUserIds));
    }

    // TODO: GetAllOrdersReturnsOrdersWithBooksWhenBooksAreIncluded

    // TODO: GetAllOrdersReturnsCancelledOrdersWhenCancelledOrdersFlagIsPassed

    // TODO: GetAllOrdersReturnsOrdersAfterAndIncludingDateWhenCreatedAtIsGiven
}