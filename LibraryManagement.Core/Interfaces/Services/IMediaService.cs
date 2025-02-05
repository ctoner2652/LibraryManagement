using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Services
{
    public interface IMediaService
    {
        Result<List<Media>> GetAllMedia(int mediaTypeID);
        Result<Media> GetMedia(int mediaID);
        Result AddMedia(Media newMedia);
        Result<Media> UpdateMedia(Media MediaToUpdate);
        Result<List<MediaType>> GetMediaTypes();
        Result<List<Media>> GetMediaByMediaType(int mediaTypeID);
        Result ArchieveMedia(int mediaID);
        Result<List<Media>> GetArchieve();
        Result<MediaType> GetMediaType(int mediaTypeID);
        Result<List<Media>> GetAll();
        Result<List<TopMediaReport>> GetTopMediaReport();
    }
}
