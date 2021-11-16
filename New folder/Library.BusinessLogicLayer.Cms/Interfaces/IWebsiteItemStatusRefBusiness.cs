using System;
using System.Collections.Generic;
using Library.Cms.DataModel; 

namespace Library.Cms.BusinessLogicLayer {
    public partial interface IWebsiteItemStatusRefBusiness { 
		bool Create(WebsiteItemStatusRefModel model);
bool Update(WebsiteItemStatusRefModel model );
List<WebsiteItemStatusRefModel> Search(int pageIndex, int pageSize, char lang,out long total  , string item_status_name);
WebsiteItemStatusRefModel GetById(string id);
List<WebsiteItemStatusRefModel> Delete(string json_list_id, Guid updated_by);
List<DropdownOptionModel> GetListDropdown(char lang );

    }
}