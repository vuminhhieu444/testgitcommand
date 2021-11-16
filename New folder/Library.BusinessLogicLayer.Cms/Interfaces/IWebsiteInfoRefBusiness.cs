using System;
using System.Collections.Generic;
using Library.Cms.DataModel; 

namespace Library.Cms.BusinessLogicLayer {
    public partial interface IWebsiteInfoRefBusiness { 
		bool Create(WebsiteInfoRefModel model);
bool Update(WebsiteInfoRefModel model );
List<WebsiteInfoRefModel> Search(int pageIndex, int pageSize, char lang,out long total  , string web_info_rcd , string web_info_faculty);
WebsiteInfoRefModel GetById(string id);
List<WebsiteInfoRefModel> Delete(string json_list_id, Guid updated_by);

    }
}