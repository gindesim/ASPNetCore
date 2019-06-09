using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DoruCore22.Models;
using Microsoft.EntityFrameworkCore;
using DoruCore22.Shared;
using Newtonsoft.Json;

namespace DoruCore22.Controllers
{
    [Route("[controller]")]
    public class CastController : Controller
    {
        private readonly DoruContext doruContext;

        public CastController(DoruContext context)
        {
            doruContext = context;

            if (doruContext.CastSet.Count() == 0)
            {
                // Create a new CoverModel if collection is empty,
                // which means you can't delete all CoverModels.
                doruContext.CastSet.Add(new CastModel { firstname = "Idol", lastname = "Debut" });
                doruContext.SaveChanges();
            }
        }
        public IActionResult Index()
        {
            return View();
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
