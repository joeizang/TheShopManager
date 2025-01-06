using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using BenchmarkDotNet.Validators;
using Microsoft.EntityFrameworkCore;

namespace BenchmarkTest;

public static class CategoriesQueryService
{
    public static readonly Func<ShopplatformContext, Guid, IEnumerable<CategoryDto>> GetCategories =
        EF.CompileQuery((ShopplatformContext context, Guid shopId) =>
            context.Categories.AsNoTracking()
                .Where(c => c.ShopId == shopId)
                .Select(c => c.ProjectToCategoryDto())
                .Take(10));
    
    public static async Task<IEnumerable<CategoryDto>> GetAllCategories(Guid shopId, ShopplatformContext context)
    {
        var results = await context.Categories.AsNoTracking()
            .Where(x => x.ShopId == shopId)
            .Select(x => x.ProjectToCategoryDto())
            .Take(10)
            .ToListAsync()
            .ConfigureAwait(false);
        return results;
    }
}



[MemoryDiagnoser]
[Config(typeof(BenchmarkConfig))]
public class CategoryBenchmarks
{
    private ShopplatformContext _context;
    private Guid _shopId;
    private Consumer _consumer;

    [GlobalSetup]
    public void Setup()
    {
        _context = new ShopplatformContext();
        _shopId = new Guid("01943284-0049-f7da-2725-1e3a60ba1ea4");
        _consumer = new Consumer();
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _context?.Dispose();
    }

    [Benchmark(Baseline = true)]
    public async Task GetAllCategoriesAsync()
    {
        var result = await CategoriesQueryService.GetAllCategories(_shopId, _context);
        _consumer.Consume(result);
    }

    [Benchmark]
    public void GetCategories()
    {
        var result = CategoriesQueryService
            .GetCategories(_context, _shopId).ToList();
        _consumer.Consume(result);
    }
}

public class BenchmarkConfig : ManualConfig
{
    public BenchmarkConfig()
    {
        AddValidator(JitOptimizationsValidator.DontFailOnError);
        AddJob(Job.Default
            .WithToolchain(InProcessEmitToolchain.Instance)
            .WithIterationCount(20)
            .WithWarmupCount(5));
    }
}

public record CategoryDto(string CategoryName, string CategoryDescription, Guid CategoryId, 
    Guid ShopId, string CreatedAt);

public record CreateCategoryDto(string Name, string Description, Guid ShopId);

public static class TestExtensions
{
    public static Category MapToCategory(this CreateCategoryDto createCategoryDto)
    {
        return new Category
        {
            CategoryName = createCategoryDto.Name,
            CategoryDescription = createCategoryDto.Description,
            ShopId = createCategoryDto.ShopId
        };
    }
    
    public static CategoryDto MapToCategoryDto(this Category category)
    {
        return 
            new CategoryDto(category.CategoryName, category.CategoryDescription, 
                category.CategoryId, category.ShopId, category.CreatedAt.ToString());
    }
    
    public static CategoryDto ProjectToCategoryDto(this Category category)
    {
        return new CategoryDto(category.CategoryName, category.CategoryDescription, 
            category.CategoryId, category.ShopId, category.CreatedAt.ToString());
    }
}