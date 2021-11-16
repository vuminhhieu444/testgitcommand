using System;
using System.Collections.Generic;
using Library.Cms.DataModel; 

namespace Library.Cms.BusinessLogicLayer {
    public partial interface IWebsiteItemTypeRefBusiness { 
		bool Create(WebsiteItemTypeRefModel model);
bool Update(WebsiteItemTypeRefModel model );
List<WebsiteItemTypeRefModel> Search(int pageIndex, int pageSize, char lang,out long total  , string item_type_rcd , string item_type_name);
WebsiteItemTypeRefModel GetById(string id);
List<WebsiteItemTypeRefModel> Delete(string json_list_id, Guid updated_by);
List<DropdownOptionModel> GetListDropdown(char lang );

    }
}