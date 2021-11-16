using System;
using System.Collections.Generic;
using Library.Cms.DataModel;
using Library.Cms.DataAccessLayer;
using System.Linq;
using System.Data; 

namespace Library.Cms.BusinessLogicLayer {
    public partial class WebsiteInfoRefBusiness : IWebsiteInfoRefBusiness { 
        private IWebsiteInfoRefRepository _res;
        public WebsiteInfoRefBusiness(IWebsiteInfoRefRepository res) 
		{
            _res = res;
        }

		/// <summary>
/// Add a new records into the table WebsiteInfoRef 
/// </summary>
/// <param name="model">the record added </param>
/// <returns></returns>
public bool Create(WebsiteInfoRefModel model) 
		{
            return _res.Create(model);
        }
/// <summary>
/// Update information in the tableWebsiteInfoRef
/// </summary>
/// <param name="model">the record updated</param>
/// <returns></returns>
 public bool Update(WebsiteInfoRefModel model) 
		{
            return _res.Update(model);
        }
/// <summary>
/// Searching information in the table WebsiteInfoRef
/// </summary>
/// <param name="pageIndex">which page?</param>
/// <param name="pageSize">the number of records in a page</param>
/// <param name="lang"> Language used to display data</param>
/// <param name="total">the total number of records</param> 
/// <returns></returns>
 public List<WebsiteInfoRefModel> Search(int pageIndex, int pageSize, char lang,out long total  , string web_info_rcd , string web_info_faculty) {
            return _res.Search(pageIndex, pageSize, lang,out total  , web_info_rcd , web_info_faculty);
        }
/// <summary>
/// Get the information by using id of the table WebsiteInfoRef
/// </summary>
/// <param name="id">Id used to get the information</param>
/// <returns></returns>
public WebsiteInfoRefModel GetById(string id) 
		{
            var result = _res.GetById(id);
            return result;
        }
/// <summary>
/// Delete records in the table WebsiteInfoRef 
/// </summary>
/// <param name="json_list_id">List id want to delete</param>
/// <param name="updated_by">User made the deletion</param>
/// <returns></returns>
public List<WebsiteInfoRefModel> Delete(string json_list_id, Guid updated_by) 
		{
            return _res.Delete(json_list_id, updated_by);
        }

         
    }
}