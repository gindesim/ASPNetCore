using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoruReactAPICore22.Models;
using DoruReactAPICore22.Shared;
using Newtonsoft.Json;

namespace DoruReactAPICore22.Controllers.Api
{
    [Route("api/[controller]")]
    public class CastController : DoruApiController
    {
        private readonly DoruContext doruContext;

        public CastController(DoruContext context)
        {
            doruContext = context;

            if (doruContext.Cast.Count() == 0)
            {
                // Create a new CastModel if collection is empty,
                // which means you can't delete all CastModels.
                doruContext.Cast.Add(new CastModel() { Firstname = "Idol", Lastname = "Debut" });
                doruContext.SaveChanges();
            }
        }

        // GET: api/Cast
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CastModel>>> GetCastSet()
        {
            return await doruContext.Cast.ToListAsync();
        }

        // GET: api/Cast/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CastModel>> GetCast(long id)
        {
            var Cast = await doruContext.Cast.FindAsync(id);

            if (Cast == null)
            {
                return NotFound();
            }

            return Cast;
        }

        // POST: api/Cast
        [HttpPost]
        public async Task<ActionResult<CastModel>> PostCast([FromBody] CastModel Cast)
        {
            //CastModel Cast = new CastModel { label = "NewCast", filename = "New Cast" };

            doruContext.Cast.Add(Cast);
            await doruContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCast), new { id = Cast.Id }, Cast);
        }

        // PUT: api/Cast/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCast(long id, [FromBody] CastModel Cast)
        {
            if (id != Cast.Id)
            {
                return BadRequest();
            }

            doruContext.Entry(Cast).State = EntityState.Modified;
            await doruContext.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Cast/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCast(long id)
        {
            var Cast = await doruContext.Cast.FindAsync(id);

            if (Cast == null)
            {
                return NotFound();
            }

            doruContext.Cast.Remove(Cast);
            await doruContext.SaveChangesAsync();

            return NoContent();
        }

        // SAVE: api/Cast
        [HttpGet]
        [Route("SaveCast")]
        public async Task<IActionResult> SaveCast()
        {
            string jsonString = JsonConvert.SerializeObject(doruContext.Cast);

            ReadWriteFile.WriteJson(jsonString, @"g:\Cast.json");

            return NoContent();
        }

        // SAVE: api/Cast
        [HttpGet]
        [Route("LoadCast")]
        public async Task<IActionResult> LoadCast()
        {
            string jsonString = ReadWriteFile.ReadJson(@"g:\Cast.json");
            List<CastModel> CastList = JsonConvert.DeserializeObject<List<CastModel>>(jsonString);
            // Clear DBSet
            doruContext.Cast.RemoveRange(doruContext.Cast);

            foreach (var Cast in CastList)
            {
                doruContext.Cast.Add(Cast);
            }
            await doruContext.SaveChangesAsync();

            return NoContent();
        }

        // SAVE: api/Cast
        [HttpGet]
        [Route("LoadFromText")]
        public async Task<IActionResult> LoadFromText()
        {
            string jsonString = ReadWriteFile.ReadTextAsJson(@"g:\covertext.txt");
            List<CoverModel> CoverList = JsonConvert.DeserializeObject<List<CoverModel>>(jsonString);
            List<CastModel> CastList = new List<CastModel>();
            foreach (var cover in CoverList)
            {
                CastModel cast = new CastModel();
                cast.Firstname = cover.Cast;
                cast.Lastname = cover.Filename;
                CastList.Add(cast);
            }
            // Sort CastList
            //List<CastModel> sortedCastList = CastList.OrderBy(x => x.firstname.ToLower()).ToList();
            List<CastModel> sortedCastList = CastList;

            // Clear DBSet
            doruContext.Cast.RemoveRange(doruContext.Cast);

            foreach (var cast in sortedCastList)
            {
                doruContext.Cast.Add(cast);
            }
            await doruContext.SaveChangesAsync();

            return NoContent();
        }

        // SAVE: api/Cast
        [HttpGet]
        [Route("MakeCastIndex")]
        public async Task<ActionResult<IEnumerable<CastModel>>> MakeSortedCastDictionary()
        {
            List<CastModel> castList = new List<CastModel>();
            Dictionary<string, List<string>> castDictionary = new Dictionary<string, List<string>>();

            foreach (var Cast in doruContext.Cast)
            {
                if (castDictionary.ContainsKey(Cast.Firstname))
                {
                    // Add new value to list in dictionary value
                    castDictionary[Cast.Firstname].Add(Cast.Lastname);
                }
                else
                {
                    // Add new entry to dictionary
                    List<string> coverList = new List<string>();
                    coverList.Add(Cast.Lastname);
                    castDictionary.Add(Cast.Firstname, coverList);
                }
            }

            foreach(var dict in castDictionary)
            {
                CastModel cast = new CastModel();
                cast.Firstname = dict.Key;
                cast.Lastname = dict.Value.Count().ToString();
                castList.Add(cast);
            }
            return castList;
        }

    }
}
