using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data.Respository
{
    public class EFMediaRepository : IMediaRepository
    {
        private LibraryContext _dbContext;

        public EFMediaRepository(string connectionString)
        {
            _dbContext = new LibraryContext(connectionString);
        }

        public void Add(Media media)
        {
            _dbContext.Media.Add(media);
            _dbContext.SaveChanges();
        }

        public List<Media> GetAll()
        {
            return _dbContext.Media.ToList();
        }

        public Media GetByMediaID(int mediaid)
        {
            var media = _dbContext.Media.FirstOrDefault(m => m.MediaID == mediaid);
            return media;
        }

        public List<Media> GetByMediaType(int mediatypeid)
        {
            return _dbContext.Media.ToList().Where(m => m.MediaTypeID == mediatypeid).ToList();
        }

        public List<MediaType> GetMediaTypes()
        {
            return _dbContext.MediaType.ToList();
        }

        public void Update(Media media)
        {
           _dbContext.Media.Update(media);
           _dbContext.SaveChanges();
        }

        public MediaType GetMediaType(int mediaTypeID)
        {
            return _dbContext.MediaType.FirstOrDefault(m => m.MediaTypeID ==  mediaTypeID);
        }

        public List<TopMediaReport> GetTopMediaReport()
        {
            var topMedia = _dbContext.TopMediaReport
            .FromSqlRaw(@"
                SELECT TOP 3 m.title, COUNT(c.mediaID) AS CheckoutCount
                FROM checkoutlog c
                JOIN media m ON c.mediaID = m.mediaID
                GROUP BY m.mediaID, m.title
                ORDER BY CheckoutCount DESC")
            .ToList();
            return topMedia;
        }
    }
}
