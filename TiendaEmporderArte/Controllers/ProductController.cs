namespace TiendaEmporderArte.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using TiendaEmporderArte.Models;
    using TiendaEmporderArte.Services;

    public class ProductController : Controller
    {
        private readonly ICosmosDBServiceProduct _cosmosDBServiceProduct;
        public ProductController(ICosmosDBServiceProduct cosmosDBServiceProduct)
        {
            _cosmosDBServiceProduct = cosmosDBServiceProduct;
        }
        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await this._cosmosDBServiceProduct.GetProductsAsync("SELECT * FROM c"));
        }

        [ActionName("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync([Bind("Id, Description, Quantity, Price, Size, Color")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid().ToString();
                await _cosmosDBServiceProduct.AddProductAsync(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync([Bind("Id, Description, Quantity, Price, Size, Color")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _cosmosDBServiceProduct.UpdateProductAsync(product.Id, product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Product product = await _cosmosDBServiceProduct.GetProductAsync(id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Product product = await _cosmosDBServiceProduct.GetProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync([Bind("Id")] string id)
        {
            await _cosmosDBServiceProduct.DeleteProductAsync(id);
            return RedirectToAction("Index");
        } 

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            return View(await _cosmosDBServiceProduct.GetProductAsync(id));
        }
    }
}
