using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoruCore22.Models;
using DoruCore22.Shared;
using Newtonsoft.Json;

namespace DoruCore22.Controllers.Api
{
    [Route("api/[controller]")]
    public class CastApiController : DoruApiController
    {
        private readonly DoruContext doruContext;

        public CastApiController(DoruContext context)
        {
            doruContext = context;

            //if (doruContext.CastSet.Count() == 0)
            //{
            //    // Create a new CastModel if collection is empty,
            //    // which means you can't delete all CastModels.
            //    doruContext.CastSet.Add(new CastModel { firstname = "Idol", lastname = "Debut" });
            //    doruContext.SaveChanges();
            //}
        }

        // GET: api/Cast
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CastModel>>> GetCastSet()
        {
            return await doruContext.CastSet.ToListAsync();
        }

        // GET: api/Cast/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CastModel>> GetCast(long id)
        {
            var Cast = await doruContext.CastSet.FindAsync(id);

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

            doruContext.CastSet.Add(Cast);
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
            var Cast = await doruContext.CastSet.FindAsync(id);

            if (Cast == null)
            {
                return NotFound();
            }

            doruContext.CastSet.Remove(Cast);
            await doruContext.SaveChangesAsync();

            return NoContent();
        }

        // SAVE: api/Cast
        [HttpGet]
        [Route("SaveCast")]
        public async Task<IActionResult> SaveCast()
        {
            string jsonString = JsonConvert.SerializeObject(doruContext.CastSet);

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
            doruContext.CastSet.RemoveRange(doruContext.CastSet);

            foreach (var Cast in CastList)
            {
                doruContext.CastSet.Add(Cast);
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
                cast.firstname = cover.cast;
                cast.lastname = cover.filename;
                CastList.Add(cast);
            }
            // Sort CastList
            //List<CastModel> sortedCastList = CastList.OrderBy(x => x.firstname.ToLower()).ToList();
            List<CastModel> sortedCastList = CastList;

            // Clear DBSet
            doruContext.CastSet.RemoveRange(doruContext.CastSet);

            foreach (var cast in sortedCastList)
            {
                doruContext.CastSet.Add(cast);
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

            foreach (var Cast in doruContext.CastSet)
            {
                if (castDictionary.ContainsKey(Cast.firstname))
                {
                    // Add new value to list in dictionary value
                    castDictionary[Cast.firstname].Add(Cast.lastname);
                }
                else
                {
                    // Add new entry to dictionary
                    List<string> coverList = new List<string>();
                    coverList.Add(Cast.lastname);
                    castDictionary.Add(Cast.firstname, coverList);
                }
            }

            foreach(var dict in castDictionary)
            {
                CastModel cast = new CastModel();
                cast.firstname = dict.Key;
                cast.lastname = dict.Value.Count().ToString();
                castList.Add(cast);
            }
            return castList;
        }

        #region Use later

        //[HttpPost]
        //public JsonResult UpdateUsersDetail(string CastJson)
        //{
        //    //var js = new JavaScriptSerializer();
        //    //UserModel[] user = js.Deserialize<UserModel[]>(usersJson);
        //    //var js = new JsonSerializer();
        //    //CastsModel[] user = js.Deserialize<CastsModel[]>(CastJson);

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

        //    var Cast = GetCasts();
        //    return Json(Cast);
        //}

        //public JsonResult GetList()
        //{
        //    //try
        //    //{

        //    //}
        //    //catch (Exception e)
        //    //{

        //    //}

        //    var Cast = GetCastsList();
        //    return Json(Cast);
        //}

        //public CastModel GetCasts()
        //{
        //    //try
        //    //{

        //    //}
        //    //catch (Exception e)
        //    //{

        //    //}
        //    CastModel oneCast = new CastModel
        //    {
        //        label = "Label",
        //        filename = "Label Casts Date"
        //    };
        //    return oneCast;
        //}

        //private List<CastModel> GetCastsList()
        //{
        //    var usersList = new List<CastModel>
        //    {
        //        new CastModel
        //        {
        //            label = "SSNI",
        //            filename = "SSNI Cast",
        //        },
        //        new CastModel
        //        {
        //            label = "PPPD",
        //            filename = "PPPD Cast",
        //        },
        //        new CastModel
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
