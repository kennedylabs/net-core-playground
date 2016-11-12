using AccountsWebsite.Infrastructure.DataAccess;
using AccountsWebsite.Infrastructure.Filters;
using KennedyLabsWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsWebsite.Controllers
{
    [Produces("application/json")]
    [Route("api/items")]
    [ApiConventionsFilter]
    public class ItemsController : Controller
    {
        private readonly IEntityRepository<ItemModel> _itemRepo;

        public ItemsController(IEntityRepository<ItemModel> itemRepo)
        {
            _itemRepo = itemRepo;
        }

        [HttpGet]
        public IQueryable<ItemModel> Query()
        {
            return _itemRepo.Query();
        }

        [HttpGet]
        public IQueryable<ItemModel> Hierarchy()
        {
            return _itemRepo.Query();
        }

        [HttpGet("{id}")]
        public async Task<ItemModel> Get([FromRoute] int id)
        {
            return await _itemRepo.FindAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<ItemModel> Tree([FromRoute] int id)
        {
            return await Hierarchy().FirstOrDefaultAsync(s => s.Id == id);
        }

        [HttpGet("{name}")]
        public async Task<ItemModel> Get([FromRoute] string name)
        {
            return await Hierarchy().FirstOrDefaultAsync(s => s.Context == name);
        }

        [HttpPost]
        public async Task Post([FromBody] ItemModel item)
        {
            await _itemRepo.CreateAsync(item);
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] ItemModel item)
        {
            await _itemRepo.UpdateAsync(item);
        }

        [HttpDelete("{id}")]
        public async Task<ItemModel> Delete([FromRoute] int id)
        {
            var item = await _itemRepo.FindAsync(id);

            if (item != null) _itemRepo.Remove(item);

            return item;
        }
    }
}
