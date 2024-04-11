using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoListAPI.Models;
using static TodoListAPI.Models.TodoList;

namespace TodoListAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoListRepository _todolist;

        public TodoController(ITodoListRepository todolist)
        {
            _todolist = todolist;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoList>>> GetAll()
        {
            try
            {
                return Ok(await _todolist.GetAllAsync());

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TodoList>> AddTodoList(TodoList todo)
        {
            try
            {
                var todoToAdd = await _todolist.GetByTitleAsync(todo.Title);

                if (todoToAdd != null)
                {
                    return BadRequest("This Title Alredy Exist");
                }

                if (todo == null)
                {
                    return BadRequest();
                }

                var createTodo = await _todolist.AddNewTodo(todo);
                return CreatedAtAction(nameof(GetAll), new { id = todo.Id }, createTodo);

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoList>> GetById(int id)
        {
            try
            {
                var todobyid = await _todolist.GetById(id);

                if (todobyid == null)
                {
                    return NotFound();
                }

                return Ok(todobyid);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{title}")]
        public async Task<ActionResult<TodoList>> GetByTitle(string title)
        {
            try
            {
                var todobytitle = await _todolist.GetByTitleAsync(title);

                if (todobytitle == null)
                {
                    return NotFound();
                }
                return Ok(todobytitle);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{date}")]
        public async Task<ActionResult<TodoList>> GetByDate(DateTime date)
        {
            try
            {

                var getbydate = await _todolist.GetByDateAsync(date);

                if (getbydate == null)
                {
                    return NotFound();
                }

                return Ok(getbydate);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{status}")]
        public async Task<ActionResult<TodoList>> GetByStatus(_Status status)
        {
            try
            {


                var getbystatus = await _todolist.GetByStatusAsync(status);
                if (getbystatus == null)
                {
                    return NotFound();
                }

                return Ok(getbystatus);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TodoList>> UpdateTodoList(int id,TodoList todo)
        {

            try
            {
                var todoToUpdate = await _todolist.GetById(id);

                if (todoToUpdate == null || todo.Id != id)
                {
                    return NotFound();
                }

                return await _todolist.UpdateTodo(todo);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    

    [HttpDelete("{id}")]
        public async Task<ActionResult<TodoList>> DeleteTodo(int id)
        {
            try
            {


                var deleteTodo = await _todolist.GetById(id);
                if (deleteTodo == null)
                {
                    return NotFound();
                }

                await _todolist.DeleteTodo(id);

                return Ok($"Todo With {id} Deleted");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
