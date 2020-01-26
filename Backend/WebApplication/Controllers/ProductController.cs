using System;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Business.Interface;
using WebApplication.Model;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductBusiness productBusiness;

        public ProductController(IProductBusiness productBusiness)
        {
            this.productBusiness = productBusiness;
        }

        [HttpGet()]
        public IActionResult GetAll()
        {
            var result = productBusiness.GetAll();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            productBusiness.Delete(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(productBusiness.GetById(id));
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductModel product)
        {
            try
            {
                var result = productBusiness.Create(product);
                return Created("", result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] ProductModel product, int id)
        {
            try
            {
                return Ok(productBusiness.Update(product, id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
