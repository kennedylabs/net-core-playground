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
    [Route("api/sections")]
    [ApiConventionsFilter]
    public class SectionsController : Controller
    {
        private readonly IEntityRepository<SectionModel> _sectionRepo;
        private readonly IEntityRepository<ItemModel> _itemRepo;

        public SectionsController(IEntityRepository<SectionModel> sectionRepo,
             IEntityRepository<ItemModel> itemRepo)
        {
            _sectionRepo = sectionRepo;
            _itemRepo = itemRepo;
        }

        [HttpGet]
        public IQueryable<SectionModel> Query()
        {
            return _sectionRepo.Query();
        }

        [HttpGet]
        public IQueryable<SectionModel> Hierarchy()
        {
            return _sectionRepo.Query()
              .Include(s => s.Sections)
              .ThenInclude(s => s.Items)
              .Include(s => s.Items);
        }

        [HttpGet("{id}")]
        public async Task<SectionModel> Get([FromRoute] int id)
        {
            return await _sectionRepo.FindAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<SectionModel> Tree([FromRoute] int id)
        {
            return await Hierarchy().FirstOrDefaultAsync(s => s.Id == id);
        }

        [HttpGet("{name}")]
        public async Task<SectionModel> Get([FromRoute] string name)
        {
            return await Hierarchy().FirstOrDefaultAsync(s => s.Title == name);
        }

        [HttpPost]
        public async Task Post([FromBody] SectionModel section)
        {
            await _sectionRepo.CreateAsync(section);
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] SectionModel section)
        {
            await _sectionRepo.UpdateAsync(section);
        }

        [HttpDelete("{id}")]
        public async Task<SectionModel> Delete([FromRoute] int id)
        {
            var section = await _sectionRepo.FindAsync(id);

            if (section != null)
            {
                foreach (var childSection in section.Sections)
                    _sectionRepo.Remove(childSection);

                foreach (var item in section.Items)
                    _itemRepo.Remove(item);

                _sectionRepo.Remove(section);
            }

            return section;
        }
    }
}
