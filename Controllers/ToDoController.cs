using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RAD4M_Test.Model;

namespace RAD4M_Test.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly DataContext _context;

        public ToDoController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ToDo
        // for this code use to display attribute or get all todo list
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDo>>> GetToDo()
        {
            return await _context.ToDo.ToListAsync();
        }


        // GET: api/ToDo/5
        // get specific todo

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDo>> GetToDo(int id)
        {
            var toDo = await _context.ToDo.FindAsync(id);

            if (toDo == null)
            {
                return NotFound();
            }

            return toDo;
        }

        //get incoming todo
        [HttpGet("incoming")]
        public IEnumerable<ToDo> GetToIncoming()
        {

            var toDoModel = _context.ToDo.Where(e => e.TodoDate >= DateTime.Now);
            if (toDoModel == null)
            {
            //    return BadRequest();
            }

            return toDoModel.ToArray();
        }

        // PUT: api/ToDo/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDo(int id, ToDo toDo)
        {
            if (id != toDo.Todoid)
            {
                return BadRequest();
            }

            _context.Entry(toDo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Set Todo percent complete (declare precentage only on body)

        [HttpPut("changeprecentage/{id}")]
        public async Task<IActionResult> UpdatePrecentage(int id, ToDo toDoModel)
        {
            if (id != toDoModel.Todoid)
            {
                return BadRequest();
            }
            var todochange = await _context.ToDo.FindAsync(id);
            todochange.TodoPrecentage = toDoModel.TodoPrecentage;
            _context.SaveChanges();


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/ToDo

        [HttpPost]
        public async Task<ActionResult<ToDo>> PostToDo(ToDo toDo)
        {
            _context.ToDo.Add(toDo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDo", new { id = toDo.Todoid }, toDo);
        }

        // DELETE: api/ToDo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ToDo>> DeleteToDo(int id)
        {
            var toDo = await _context.ToDo.FindAsync(id);
            if (toDo == null)
            {
                return NotFound();
            }

            _context.ToDo.Remove(toDo);
            await _context.SaveChangesAsync();

            return toDo;
        }

        private bool ToDoExists(int id)
        {
            return _context.ToDo.Any(e => e.Todoid == id);
        }

        // PUT: api/ToDoModels/MarkDone/5
        // Mark Todo as Done (declare precentage only on body)
        [HttpPut("markdone/{id}")]
        public async Task<IActionResult> MarkDone(int id)
        {

            var todochange = await _context.ToDo.FindAsync(id);
            todochange.TodoStatus = true;
            _context.SaveChanges();


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

    }
}

