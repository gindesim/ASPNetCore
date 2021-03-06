﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DoruReactAPICore22.Models;

namespace DoruReactAPICore22.Controllers
{
    public class MVCCoverController : Controller
    {
        private readonly DoruContext doruContext;

        public MVCCoverController(DoruContext context)
        {
            doruContext = context;

            if (doruContext.Cover.Count() == 0)
            {
                // Create a new CoverModel if collection is empty,
                // which means you can't delete all CoverModels.
                doruContext.Cover.Add(new CoverModel { Label = "NewCover", Filename = "New Cover" });
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
