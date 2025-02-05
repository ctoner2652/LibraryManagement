using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Repository
{
    public interface IMediaRepository
    {
        void Add(Media media);
        void Update(Media media);
        List<Media> GetAll();
        List<Media> GetByMediaType(int mediatypeid);
        Media GetByMediaID(int mediaid);
        List<MediaType> GetMediaTypes();
        MediaType GetMediaType(int mediaTypeID);
        List<TopMediaReport> GetTopMediaReport();
    }
}
