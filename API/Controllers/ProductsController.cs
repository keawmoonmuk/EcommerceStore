using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
 
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        //create constucter Storecontext

        public ProductsController(IGenericRepository<Product> productsRepo,
         IGenericRepository<ProductBrand> productBrandRepo,IGenericRepository<ProductType>
        productTypeRepo , IMapper mapper)
        {
            _productsRepo = productsRepo;           // product repor
            _productBrandRepo = productBrandRepo;   // product brand
            _productTypeRepo = productTypeRepo;     // product type
            _mapper = mapper;                       // automapper
        }

        //get all product
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GerProducts()
        {
            var spec = new ProductWithTypesAndBrandsSpecification();

            var products = await _productsRepo.ListAsync(spec);

            return Ok(_mapper
            .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));

            // return product.Select(product => new ProductToReturnDto
            // {
            //    Id = product.Id,
            //    Name = product.Name,
            //    Description = product.Description,
            //    PictureUrl = product.PictureUrl,
            //    Price = product.Price,
            //    ProductBrand = product.ProductBrand.Name,
            //    ProductType = product.ProductType.Name
            // }).ToList();
        }

        //get id product
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),  StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductWithTypesAndBrandsSpecification(id);

           var product =  await _productsRepo.GetEntityWithSpec(spec);

           if(product == null) return NotFound(new ApiResponse(404));

           return _mapper.Map<Product, ProductToReturnDto>(product);

        //    return new ProductToReturnDto 
        //    {
        //        Id = product.Id,
        //        Name = product.Name,
        //        Description = product.Description,
        //        PictureUrl = product.PictureUrl,
        //        Price = product.Price,
        //        ProductBrand = product.ProductBrand.Name,
        //        ProductType = product.ProductType.Name
        //    };
        }

        //get product brand
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepo.ListAllAsync());
        }

        //get product types
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTypeRepo.ListAllAsync());
        }
    }
}