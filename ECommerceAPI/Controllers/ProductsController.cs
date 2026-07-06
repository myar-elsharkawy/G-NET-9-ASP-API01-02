using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTOs.ProductDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ApiBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // Get all products
        // Get BaseUrl/api/products
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAllProducts(CancellationToken ct)
        {
            var result = await _productService.GetAllProductsAsync(ct);
            return ToActionResult(result);
        }

        //Get Product By Id
        //Get BaseUrl/api/Products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id , CancellationToken ct)
        {
            var result = await _productService.GetProductByIdAsync(id, ct);
            return ToActionResult(result);
        }

        // Get All Brands
        // Get BaseUrl/api/Products/brands
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetAllBrands(CancellationToken ct)
        {
            var result = await _productService.GetAllBrandsAsync(ct);
            return ToActionResult(result);
        }

        // Get All Types
        // Get BaseUrl/api/Products/types
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<TypeDto>>> GetAllTypes(CancellationToken ct)
        {
            var result = await _productService.GetAllTypesAsync(ct);
            return ToActionResult(result);
        }
    }
}
