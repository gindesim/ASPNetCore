using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DoruCore22.Models;
using Microsoft.EntityFrameworkCore;
using DoruCore22.Shared;
using Newtonsoft.Json;

namespace DoruCore22.Controllers.Api
{
    [Route("api/[controller]")]
    public class CoverApiController : DoruApiController
    {
        private readonly DoruContext doruContext;

        public CoverApiController(DoruContext context)
        {
            doruContext = context;

            if (doruContext.CoverSet.Count() == 0)
            {
                // Create a new CoverModel if collection is empty,
                // which means you can't delete all CoverModels.
                doruContext.CoverSet.Add(new CoverModel { label = "NewCover", filename = "New Cover" });
                doruContext.SaveChanges();
            }
        }

        // GET: api/Cover
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoverModel>>> GetCoverSet()
        {
            return await doruContext.CoverSet.ToListAsync();
        }

        // GET: api/Cover/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CoverModel>> GetCover(long id)
        {
            var cover = await doruContext.CoverSet.FindAsync(id);

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
            //CoverModel cover = new CoverModel { label = "NewCover", filename = "New Cover" };

            doruContext.CoverSet.Add(cover);
            await doruContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCover), new { id = cover.Id }, cover);
        }

        // PUT: api/Cover/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCover(long id, [FromBody] CoverModel cover)
        {
            if (id != cover.Id)
            {
                return BadRequest();
            }

            doruContext.Entry(cover).State = EntityState.Modified;
            await doruContext.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Cover/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCover(long id)
        {
            var cover = await doruContext.CoverSet.FindAsync(id);

            if (cover == null)
            {
                return NotFound();
            }

            doruContext.CoverSet.Remove(cover);
            await doruContext.SaveChangesAsync();

            return NoContent();
        }

        // SAVE: api/Cover
        [HttpGet]
        [Route("SaveCover")]
        //public async Task<IActionResult> SaveCover()
        //{
        //    string jsonString = JsonConvert.SerializeObject(doruContext.CoverSet);

        //    ReadWriteFile.WriteJson(jsonString, @"g:\cover.json");

        //    return NoContent();
        //}

        // SAVE: api/Cover
        [HttpGet]
        [Route("LoadCover")]
        public async Task<IActionResult> LoadCover()
        {
            string jsonString = ReadWriteFile.ReadTextAsJson(@"g:\cover.json");
            List<CoverModel> coverList = JsonConvert.DeserializeObject<List<CoverModel>>(jsonString);
            while(doruContext.CoverSet.Count() > 0)
            {
                var cover = doruContext.CoverSet.First();
                doruContext.CoverSet.Remove(cover);
                await doruContext.SaveChangesAsync();
            }

            foreach (var cover in coverList)
            {
                doruContext.CoverSet.Add(cover);
                await doruContext.SaveChangesAsync();
            }

            return NoContent();
        }

        // SAVE: api/Cover
        [HttpGet]
        [Route("LoadFromText")]
        public async Task<IActionResult> LoadFromText()
        {
            string jsonString = ReadWriteFile.ReadTextAsJson(@"g:\covertext.txt");
            List<CoverModel> CoverList = JsonConvert.DeserializeObject<List<CoverModel>>(jsonString);
            // Sort CoverList
            //List<CoverModel> sortedCoverList = CoverList.OrderBy(x => x.series.ToLower()).ToList();
            List<CoverModel> sortedCoverList = CoverList;

            // Clear DBSet
            doruContext.CoverSet.RemoveRange(doruContext.CoverSet);

            foreach (var cover in sortedCoverList)
            {
                doruContext.CoverSet.Add(cover);
            }
            await doruContext.SaveChangesAsync();

            return NoContent();
        }

        #region Use later

        //[HttpPost]
        //public JsonResult UpdateUsersDetail(string coverJson)
        //{
        //    //var js = new JavaScriptSerializer();
        //    //UserModel[] user = js.Deserialize<UserModel[]>(usersJson);
        //    //var js = new JsonSerializer();
        //    //CoversModel[] user = js.Deserialize<CoversModel[]>(coverJson);

        //    //TODO: user now contains the details, you can do required operations  
        //    //return Json("User Details are updated");
        //    return null;
        //}

        //[HttpGet]
        //public ActionResult Sample()
        //{
        //    return View();
        //}

        //// GET api/values
        ////[HttpGet]
        //public JsonResult Get()
        //{
        //    //try
        //    //{

        //    //}
        //    //catch (Exception e)
        //    //{

        //    //}

        //    var cover = GetCovers();
        //    return Json(cover);
        //}

        //public JsonResult GetList()
        //{
        //    //try
        //    //{

        //    //}
        //    //catch (Exception e)
        //    //{

        //    //}

        //    var cover = GetCoversList();
        //    return Json(cover);
        //}

        //public CoverModel GetCovers()
        //{
        //    //try
        //    //{

        //    //}
        //    //catch (Exception e)
        //    //{

        //    //}
        //    CoverModel oneCover = new CoverModel
        //    {
        //        label = "Label",
        //        filename = "Label Casts Date"
        //    };
        //    return oneCover;
        //}

        //private List<CoverModel> GetCoversList()
        //{
        //    var usersList = new List<CoverModel>
        //    {
        //        new CoverModel
        //        {
        //            label = "SSNI",
        //            filename = "SSNI Cast",
        //        },
        //        new CoverModel
        //        {
        //            label = "PPPD",
        //            filename = "PPPD Cast",
        //        },
        //        new CoverModel
        //        {
        //            label = "JUFE",
        //            filename = "JUFE Cast",
        //        }
        //    };

        //    return usersList;
        //}

        #endregion

    }
}
