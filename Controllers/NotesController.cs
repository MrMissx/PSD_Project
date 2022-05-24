using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PSD_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private static readonly string DataPath = "./data/notes.json";
        public static int nextId = 1;
        public static List<Notes> notesData = new();

        public NotesController()
        {
            ReadJson();
            nextId = 1;
        }

        private void ReadJson()
        {
            string jsonString = System.IO.File.ReadAllText(DataPath);
            notesData = JsonConvert.DeserializeObject<List<Notes>>(jsonString);
        }

        private void WriteJson(Notes? newNotes = null)
        {
            if (newNotes != null)
                notesData.Add(newNotes);
            string json = JsonSerializer.Serialize(notesData);
            System.IO.File.WriteAllText(DataPath, json);
        }

        [HttpGet]
        public IActionResult Get()
        {
            ReadJson();
            return Ok(notesData);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            foreach (var note in notesData)
            {
                if (note.ID == id)
                {
                    return Ok(note);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult AddNotes([FromBody] NotesDto data)
        {
            var notes = new Notes
            {
                ID = nextId,
                Content = data.Content,
                Title = data.Title,
                Creator = data.Creator,
                CreateDate = DateTime.Now,
            };
            WriteJson(notes);
            nextId++;
            return Ok(notes);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNotes([FromBody] NotesDto data, int id)
        {
            for(int i = 0; i < notesData.Count; i++)
            {
                Notes note = notesData[i];
                if (note.ID == id)
                {
                    note.Content = data.Content;
                    note.Title = data.Title;
                    note.UpdateDate = DateTime.Now;
                    WriteJson();
                    return Ok(note);
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            for (int i = 0; i < notesData.Count; i++)
            {
                var note = notesData[i];
                if (note.ID == id)
                {
                    notesData.RemoveAt(i);
                    WriteJson();
                    return Ok();
                }
            }
            return NotFound();
            
        }
    }
}
