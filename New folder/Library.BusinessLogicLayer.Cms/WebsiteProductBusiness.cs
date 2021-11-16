using System;
using System.Collections.Generic;
using Library.Cms.DataModel;
using Library.Cms.DataAccessLayer;
using System.Linq;
using System.Data; 

namespace Library.Cms.BusinessLogicLayer {
    public partial class WebsiteProductBusiness : IWebsiteProductBusiness { 
        private IWebsiteProductRepository _res;
        public WebsiteProductBusiness(IWebsiteProductRepository res) 
		{
            _res = res;
        }

		/// <summary>
/// Add a new records into the table WebsiteProduct 
/// </summary>
/// <param name="model">the record added </param>
/// <returns></returns>
public bool Create(WebsiteProductModel model) 
		{
            return _res.Create(model);
        }
/// <summary>
/// Update information in the tableWebsiteProduct
/// </summary>
/// <param name="model">the record updated</param>
/// <returns></returns>
 public bool Update(WebsiteProductModel model) 
		{
            return _res.Update(model);
        }
/// <summary>
/// Searching information in the table WebsiteProduct
/// </summary>
/// <param name="pageIndex">which page?</param>
/// <param name="pageSize">the number of records in a page</param>
/// <param name="lang"> Language used to display data</param>
/// <param name="total">the total number of records</param> 
/// <returns></returns>
 public List<WebsiteProductModel> Search(int pageIndex, int pageSize, char lang,out long total  , string product_name) {
            return _res.Search(pageIndex, pageSize, lang,out total  , product_name);
        }
/// <summary>
/// Get the information by using id of the table WebsiteProduct
/// </summary>
/// <param name="id">Id used to get the information</param>
/// <returns></returns>
public WebsiteProductModel GetById(Guid? id) 
		{
            var result = _res.GetById(id);
            return result;
        }
/// <summary>
/// Delete records in the table WebsiteProduct 
/// </summary>
/// <param name="json_list_id">List id want to delete</param>
/// <param name="updated_by">User made the deletion</param>
/// <returns></returns>
public List<WebsiteProductModel> Delete(string json_list_id, Guid updated_by) 
		{
            return _res.Delete(json_list_id, updated_by);
        }

         
    }
}