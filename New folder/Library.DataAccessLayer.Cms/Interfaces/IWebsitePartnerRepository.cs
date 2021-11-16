using System;
using System.Collections.Generic;
using Library.Cms.DataModel;
namespace Library.Cms.DataAccessLayer
{
    public partial interface IWebsitePartnerRepository
    {
        bool Create(WebsitePartnerModel model);
        bool Update(WebsitePartnerModel model);
        List<WebsitePartnerModel> Search(int pageIndex, int pageSize, char lang, out long total, string partner_name);
        WebsitePartnerModel GetById(Guid? id);
        List<WebsitePartnerModel> Delete(string json_list_id, Guid updated_by);

    }
}