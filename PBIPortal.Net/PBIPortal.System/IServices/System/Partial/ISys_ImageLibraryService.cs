using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Enums;
using PBIPortal.Entity.DomainModels;
using System.Collections.Generic;

namespace PBIPortal.System.IServices
{
    public partial interface ISys_ImageLibraryService
    {
       void AddImage(string imageUrls, ImageSourceType sourceType, bool saveChanges = false);

       void AddImage(string imageUrls, string tableName);

        string GetImage(int page);
        string GetVideo(int page);
        KeyValuePair<string, string> GetVideoIframe(int id);
    }
 }

