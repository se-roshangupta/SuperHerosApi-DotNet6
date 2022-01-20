using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHerosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHerosController : ControllerBase
    {
        //To Access heros list from every where
        private static List<SuperHeros> heros = new List<SuperHeros>
            {
                new SuperHeros {Id=1, Name="Captain America", FirstName="Chris", LastName="Evans", Place="New York City"},
                new SuperHeros {Id=2, Name="Iron Man", FirstName="Robert", LastName="Downey Jr", Place="New York City"},
                new SuperHeros {Id=3, Name="Thor", FirstName="Chris", LastName="Hemsworth", Place="New York City"},
                new SuperHeros {Id=4, Name="Natasha Romanoff", FirstName="Scarlett", LastName="Johansson", Place="New york"}
            };
        private readonly DataContext _context;

        public SuperHerosController(DataContext context)
        {
            _context = context;
        }

        //Get Hero List
        [HttpGet]
        //public async Task<IActionResult> Get()
        public async Task<ActionResult<List<SuperHeros>>> Get() //To show In Schemas
        {
            //To show heros list static array above
            /*var heros = new List<SuperHeros>
            {
                new SuperHeros {Id=1, Name="Captain America", FirstName="Chris", LastName="Evans", Place="New York City"},
                new SuperHeros {Id=2, Name="Iron Man", FirstName="Robert", LastName="Downey Jr", Place="New York City"},
                new SuperHeros {Id=3, Name="Thor", FirstName="Chris", LastName="Hemsworth", Place="New York City"}
            };*/
            //return Ok(heros);

            // From Database 
            return Ok(await _context.SuperHeros.ToListAsync());
        }

        // Get Single Heros By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHeros>> Get(int id)
        {
            var hero = await _context.SuperHeros.FindAsync(id);
            if(hero == null)
            {
                return BadRequest("Hero Not Found!");
            }
            return Ok(hero);
        }

        // To Add New Heros
        [HttpPost]
        public async Task<ActionResult<List<SuperHeros>>> AddHeros(SuperHeros hero)
        {
            _context.SuperHeros.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeros.ToListAsync());
        }

        // To Update Heros
        [HttpPut]
        public async Task<ActionResult<List<SuperHeros>>> UpdateHeros(SuperHeros request)
        {
            var dbHero = await _context.SuperHeros.FindAsync(request.Id);
            if (dbHero == null)
            {
                return BadRequest("Hero Not Found!");
            }

            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeros.ToListAsync());
        }

        // Delete Single Heros By Id
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHeros>>> DeleteHreo(int id)
        {
            var dbHero = await _context.SuperHeros.FindAsync(id);
            if (dbHero == null)
            {
                return BadRequest("Hero Not Found!");
            }
            _context.SuperHeros.Remove(dbHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeros.ToListAsync());
        }
    }
}
