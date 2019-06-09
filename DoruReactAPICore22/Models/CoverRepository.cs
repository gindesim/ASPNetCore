using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoruReactAPICore22.Models
{
    [Route("api/[controller]")]
    public class CoverRepository
    {
        public DoruContext doruDB = new DoruContext();

        public IEnumerable<CoverModel> GetAllCovers()
        {
            try
            {
                return doruDB.Cover.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        //To Add new Cover record     
        public int AddCover(CoverModel cover)
        {
            try
            {
                doruDB.Cover.Add(cover);
                doruDB.SaveChanges();
                return 1;
            }
            catch
            {
                throw;
            }
        }
        //To Update the records of a particluar Cover    
        public int UpdateCover(CoverModel cover)
        {
            try
            {
                doruDB.Entry(cover).State = EntityState.Modified;
                doruDB.SaveChanges();
                return 1;
            }
            catch
            {
                throw;
            }
        }
        //Get the details of a particular Cover    
        public CoverModel GetCoverData(long id)
        {
            try
            {
                CoverModel cover = doruDB.Cover.Find(id);
                return cover;
            }
            catch
            {
                throw;
            }
        }
        //To Delete the record of a particular Cover    
        public int DeleteCover(long id)
        {
            try
            {
                CoverModel cover = doruDB.Cover.Find(id);
                doruDB.Cover.Remove(cover);
                doruDB.SaveChanges();
                return 1;
            }
            catch
            {
                throw;
            }
        }

    }
}
