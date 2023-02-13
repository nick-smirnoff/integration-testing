using IntegrationTesting.Web.API.Notes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTesting.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly NotesDbContext _dbContext;

        public NotesController(NotesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
            var notes = await _dbContext.Notes.ToListAsync();
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote([FromRoute] int id)
        {
            var note = await _dbContext.Notes.FirstOrDefaultAsync(note => note.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        [HttpPost]
        public async Task<ActionResult<Note>> CreateNote([FromBody] CreateNote payload)
        {
            var note = new Note() { Content = payload.Content };
            _dbContext.Notes.Add(note);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Note>> UpdateNote([FromRoute] int id, [FromBody] UpdateNote payload)
        {
            var note = await _dbContext.Notes.FirstOrDefaultAsync(note => note.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            note.Content = payload.Content;
            await _dbContext.SaveChangesAsync();

            return Ok(note);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Note>> UpdateNote([FromRoute] int id)
        {
            var note = await _dbContext.Notes.FirstOrDefaultAsync(note => note.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            _dbContext.Notes.Remove(note);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}