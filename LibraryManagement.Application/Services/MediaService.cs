using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repository;
using LibraryManagement.Core.Interfaces.Services;
using LibraryManagement.Data.Respository;

namespace LibraryManagement.Application.Services
{
    public class MediaService : IMediaService
    {
        private IMediaRepository _mediaRepository;

        public MediaService(IMediaRepository repo)
        {
            _mediaRepository = repo;
        }

        public Result AddMedia(Media newMedia)
        {
            try
            {
                _mediaRepository.Add(newMedia);
                return ResultFactory.Success();
            }
            catch (Exception ex) { 
                return ResultFactory.Fail(ex.Message);
            }

        }

        public Result ArchieveMedia(int mediaID)
        {
            try
            {
                var Media = _mediaRepository.GetByMediaID(mediaID);
                if (!Media.IsArchived)
                {
                    Media.IsArchived = true;
                    _mediaRepository.Update(Media);
                    return ResultFactory.Success();
                }
                else
                {
                    return ResultFactory.Fail("Media is already archieved and can't be archieved again.");
                }
                
            }
            catch (Exception ex) { 
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result<List<Media>> GetAllMedia(int mediaTypeID)
        {
            try
            {
                var medias = _mediaRepository.GetByMediaType(mediaTypeID);
                return ResultFactory.Success(medias);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Media>>(ex.Message);
            }
        }

        public Result<List<Media>> GetArchieve()
        {
            try
            {
                var archieve = _mediaRepository.GetAll().Where(m => m.IsArchived == true).ToList();
                return ResultFactory.Success(archieve);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Media>>(ex.Message);
            }
        }

        public Result<Media> GetMedia(int mediaID)
        {
            try
            {
                return ResultFactory.Success(_mediaRepository.GetByMediaID(mediaID));
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<Media>(ex.Message);
            }
        }

        public Result<List<Media>> GetMediaByMediaType(int mediaTypeID)
        {
            try
            {
                var medias = _mediaRepository.GetByMediaType(mediaTypeID).Where(m => m.IsArchived == false).ToList();
                return ResultFactory.Success(medias);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Media>>(ex.Message);
            }
        }

        public Result<List<MediaType>> GetMediaTypes()
        {
            try
            {
                var mediaTypes = _mediaRepository.GetMediaTypes();
                return ResultFactory.Success(mediaTypes);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<MediaType>>(ex.Message);
            }
        }

        public Result<Media> UpdateMedia(Media MediaToUpdate)
        {
            try
            {
                if (!MediaToUpdate.IsArchived)
                {
                    _mediaRepository.Update(MediaToUpdate);
                    return ResultFactory.Success(MediaToUpdate);
                }
                else
                {
                    return ResultFactory.Fail<Media>("Media is currenly archieved and can not be updated!");
                }
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<Media>(ex.Message);
            }
        }
        public Result<MediaType> GetMediaType(int mediaTypeID)
        {
            try
            {
                var type = _mediaRepository.GetMediaType(mediaTypeID);
                return ResultFactory.Success(type);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<MediaType>(ex.Message);
            }
        }

        public Result<List<Media>> GetAll()
        {
            try
            {
                var medias = _mediaRepository.GetAll();
                return ResultFactory.Success(medias);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Media>>(ex.Message);
            }
        }

        public Result<List<TopMediaReport>> GetTopMediaReport()
        {
            try
            {
                var topMedias = _mediaRepository.GetTopMediaReport();
                return ResultFactory.Success(topMedias);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<TopMediaReport>>(ex.Message);
            }
        }
    }
}
