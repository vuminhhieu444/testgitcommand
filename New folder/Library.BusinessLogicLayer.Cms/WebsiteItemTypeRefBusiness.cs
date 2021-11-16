using System;
using System.Collections.Generic;
using Library.Cms.DataModel;
using Library.Cms.DataAccessLayer;
using System.Linq;
using System.Data; 

namespace Library.Cms.BusinessLogicLayer {
    public partial class WebsiteItemTypeRefBusiness : IWebsiteItemTypeRefBusiness { 
        private IWebsiteItemTypeRefRepository _res;
        public WebsiteItemTypeRefBusiness(IWebsiteItemTypeRefRepository res) 
		{
            _res = res;
        }

		/// <summary>
/// Add a new records into the table WebsiteItemTypeRef 
/// </summary>
/// <param name="model">the record added </param>
/// <returns></returns>
public bool Create(WebsiteItemTypeRefModel model) 
		{
            return _res.Create(model);
        }
/// <summary>
/// Update information in the tableWebsiteItemTypeRef
/// </summary>
/// <param name="model">the record updated</param>
/// <returns></returns>
 public bool Update(WebsiteItemTypeRefModel model) 
		{
            return _res.Update(model);
        }
/// <summary>
/// Searching information in the table WebsiteItemTypeRef
/// </summary>
/// <param name="pageIndex">which page?</param>
/// <param name="pageSize">the number of records in a page</param>
/// <param name="lang"> Language used to display data</param>
/// <param name="total">the total number of records</param> 
/// <returns></returns>
 public List<WebsiteItemTypeRefModel> Search(int pageIndex, int pageSize, char lang,out long total  , string item_type_rcd , string item_type_name) {
            return _res.Search(pageIndex, pageSize, lang,out total  , item_type_rcd , item_type_name);
        }
/// <summary>
/// Get the information by using id of the table WebsiteItemTypeRef
/// </summary>
/// <param name="id">Id used to get the information</param>
/// <returns></returns>
public WebsiteItemTypeRefModel GetById(string id) 
		{
            var result = _res.GetById(id);
            return result;
        }
/// <summary>
/// Delete records in the table WebsiteItemTypeRef 
/// </summary>
/// <param name="json_list_id">List id want to delete</param>
/// <param name="updated_by">User made the deletion</param>
/// <returns></returns>
public List<WebsiteItemTypeRefModel> Delete(string json_list_id, Guid updated_by) 
		{
            return _res.Delete(json_list_id, updated_by);
        }
/// <summary>
/// Get information from the table WebsiteItemTypeRef and push it into a list of type DropdownOptionModel
/// </summary>
/// <param name="lang">Language used to display data</param> 
/// <returns></returns>
public List<DropdownOptionModel> GetListDropdown(char lang )
		{ 
			var result = _res.GetListDropdown(lang );
			return result == null ? null : result;
		}

         
    }
}