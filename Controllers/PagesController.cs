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
    [Route("api/pages")]
    [ApiConventionsFilter]
    public class PagesController : Controller
    {
        private readonly IEntityRepository<PageModel> _pageRepo;
        private readonly IEntityRepository<SectionModel> _sectionRepo;
        private readonly IEntityRepository<ItemModel> _itemRepo;

        public PagesController(IEntityRepository<PageModel> pageRepo,
            IEntityRepository<SectionModel> sectionRepo, IEntityRepository<ItemModel> itemRepo)
        {
            _pageRepo = pageRepo;
            _sectionRepo = sectionRepo;
            _itemRepo = itemRepo;
        }

        [HttpGet]
        public IQueryable<string> Names()
        {
            return _pageRepo.Query().Select(p => p.Name);
        }

        [HttpGet]
        public IQueryable<PageModel> Query()
        {
            return _pageRepo.Query();
        }

        [HttpGet]
        public IQueryable<PageModel> Hierarchy()
        {
            return _pageRepo.Query()
                .Include(p => p.Sections)
                .ThenInclude(s => s.Sections)
                .ThenInclude(s => s.Items)
                .Include(p => p.Sections)
                .ThenInclude(s => s.Items);
        }

        [HttpGet("{id}")]
        public async Task<PageModel> Get([FromRoute] int id)
        {
            return await _pageRepo.FindAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<PageModel> Tree([FromRoute] int id)
        {
            return await Hierarchy().FirstOrDefaultAsync(p => p.Id == id); ;
        }

        [HttpGet("{name}")]
        public async Task<PageModel> Get([FromRoute] string name)
        {
            return await Hierarchy().FirstOrDefaultAsync(p => p.Name == name);
        }

        [HttpPost]
        public async Task Post([FromBody] PageModel page)
        {
            await _pageRepo.CreateAsync(page);
        }
        
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] PageModel page)
        {
            await _pageRepo.UpdateAsync(page);
        }

        [HttpDelete("{id}")]
        public async Task<PageModel> Delete(int id)
        {
            var page = await _pageRepo.FindAsync(id);

            if (page != null)
            {
                foreach (var section in page.AllSections)
                    _sectionRepo.Remove(section);

                foreach (var item in page.AllItems)
                    _itemRepo.Remove(item);

                _pageRepo.Remove(page);
            }

            return page;
        }
    }
}
