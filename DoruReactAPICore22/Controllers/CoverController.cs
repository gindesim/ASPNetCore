using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using DoruReactAPICore22.Shared;
using DoruReactAPICore22.Models;
using System;

namespace DoruReactAPICore22.Controllers.Api
{
    [Route("api/[controller]")]
    public class CoverController : DoruApiController
    {
        public DoruContext doruDB = new DoruContext();

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<CoverModel>>> Covers()
        {
            try
            {
                return await doruDB.Cover.ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: api/Cover/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CoverModel>> GetCover(long id)
        {
            var cover = await doruDB.Cover.FindAsync(id);

            if (cover == null)
            {
                return NotFound();
            }
            return cover;
        }

        // POST: api/Cover
        [HttpPost]
        public async Task<ActionResult<CoverModel>> PostCover([FromBody] CoverModel cover)
        {
            //return coverRepo.AddCover(cover);
            try
            {
                doruDB.Cover.Add(cover);
                await doruDB.SaveChangesAsync();
                return CreatedAtAction(nameof(GetCover), new { id = cover.Id }, cover);
            }
            catch
            {
                throw;
            }
        }

        // PUT: api/Cover/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCover(long id, [FromBody] CoverModel cover)
        {
            if (id != cover.Id)
            {
                return BadRequest();
            }

            doruDB.Entry(cover).State = EntityState.Modified;
            await doruDB.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Cover/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCover(long id)
        {
            var cover = await doruDB.Cover.FindAsync(id);

            if (cover == null)
            {
                return NotFound();
            }

            doruDB.Cover.Remove(cover);
            await doruDB.SaveChangesAsync();

            return NoContent();
        }

        // SAVE: api/Cover
        [HttpGet]
        [Route("SaveToCoverJson")]
        public async Task<IActionResult> SaveToCoverJson()
        {
            string jsonString = JsonConvert.SerializeObject(doruDB.Cover);

            ReadWriteFile.WriteJson(jsonString, @"g:\cover.json");

            return NoContent();
        }

        // LOAD: api/Cover/LoadFromCoverTextFile
        [HttpGet]
        [Route("LoadFromCoverTextFile")]
        public async Task<IActionResult> LoadFromCoverTextFile()
        {
            string jsonString = ReadWriteFile.ReadTextAsJson(@"g:\covertext.txt");
            List<CoverModel> CoverList = JsonConvert.DeserializeObject<List<CoverModel>>(jsonString);
            // Sort CoverList
            //List<CoverModel> sortedCoverList = CoverList.OrderBy(x => x.series.ToLower()).ToList();
            List<CoverModel> sortedCoverList = CoverList;

            // Clear DBSet
            doruDB.Cover.RemoveRange(doruDB.Cover);

            foreach (var cover in sortedCoverList)
            {
                doruDB.Cover.Add(cover);
            }
            await doruDB.SaveChangesAsync();
            return NoContent();
        }

        // CLEAR: api/Cover
        [HttpGet]
        [Route("ClearCover")]
        public async Task<IActionResult> ClearCover()
        {
            // Clear DBSet
            doruDB.Cover.RemoveRange(doruDB.Cover);
            await doruDB.SaveChangesAsync();
            return Content("Cover has been cleared");
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<CoverModel>>> BadCovers()
        {
            try
            {
                string setupName;
                List<CoverModel> allCover = await doruDB.Cover.ToListAsync();
                List<CoverModel> result = new List<CoverModel>();
                foreach (var cover in allCover)
                {
                    setupName = $"{cover?.Series} {cover?.Cast} {cover?.Releasedate}";
                    setupName = $"{setupName.Trim()}.jpg";
                    if(setupName != cover.Filename)
                    {
                        result.Add(cover);
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
