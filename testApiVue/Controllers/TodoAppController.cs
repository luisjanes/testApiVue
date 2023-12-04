using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using testApiVue.Controllers;


namespace testApiVue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class Notes
    {
        public int Key { get; set; }
        public string Description { get; set; }
    }
    public class TodoAppController : ControllerBase
    {
        [HttpGet]
        [Route("GetNotes")]

        public IEnumerable<Notes> GetNotes()
        {
            using var connection = new MySqlConnection("Server=localhost; User ID=root; Password=123456; Database=todoappServer");
            var notes = connection.Query<Notes>("""SELECT * FROM todoappServer.notes""");

            return notes;
        }

        [HttpPost]
        [Route("AddNotes")]

        public Notes AddNotes([FromForm] string newNotes)
        {
            var notes = new Notes {Description = newNotes };
            using var connection = new MySqlConnection("Server=localhost; User ID=root; Password=123456; Database=todoappServer");
            connection.Execute("""INSERT INTO todoappServer.notes VALUES (null,@newNotes) """,
                new
                {
                    newNotes = newNotes
                });
            return notes;
        }

        [HttpDelete]
        [Route("DeleteNotes")]

        public Notes DeleteNotes(int key)
        {
            var notes = new Notes { Key = key };
            using var connection = new MySqlConnection("Server=localhost; User ID=root; Password=123456; Database=todoappServer");
            connection.Execute("""DELETE from todoappServer.notes u where u.key=@Key """,
                new
                {
                    Key = key
                });
            return notes;
        }
    }
}
