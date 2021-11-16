using System;
using System.Collections.Generic;
using Library.Cms.DataModel;
namespace Library.Cms.DataAccessLayer 
{
    public partial interface IWebsiteGroupTypeRefRepository 
	{ 
		bool Create(WebsiteGroupTypeRefModel model);
bool Update(WebsiteGroupTypeRefModel model);
List<WebsiteGroupTypeRefModel> Search(int pageIndex, int pageSize, char lang, out long total  , string group_type_name);
WebsiteGroupTypeRefModel GetById(string id);
List<WebsiteGroupTypeRefModel> Delete(string json_list_id, Guid updated_by);
List<DropdownOptionModel> GetListDropdown(char lang );

    }
}