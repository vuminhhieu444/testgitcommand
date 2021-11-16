using System;
using System.Collections.Generic;
using Library.Cms.DataModel;
using Library.Common.Helper;
using System.Data;
using System.Linq; 

namespace Library.Cms.DataAccessLayer {
    public partial class WebsiteProductRepository : IWebsiteProductRepository 
	{
		private IDatabaseHelper _dbHelper; 
		public WebsiteProductRepository(IDatabaseHelper dbHelper)
		{
			_dbHelper = dbHelper; 
		}  
		/// <summary>
/// Add a new records into the table WebsiteProduct 
/// </summary>
/// <param name="model">the record added </param>
/// <returns></returns>
public bool Create(WebsiteProductModel model) 
		{
           try
            {
                
				if (model.product_id == null || model.product_id == Guid.Empty)   model.product_id = Guid.NewGuid(); 
                var parameters = new List<IDbDataParameter>
                {
                    	_dbHelper.CreateInParameter("@product_id",DbType.Guid,model.product_id),
	_dbHelper.CreateInParameter("@product_name_l",DbType.String,model.product_name_l),
	_dbHelper.CreateInParameter("@product_name_e",DbType.String,model.product_name_e),
	_dbHelper.CreateInParameter("@product_description_l",DbType.String,model.product_description_l),
	_dbHelper.CreateInParameter("@product_description_e",DbType.String,model.product_description_e),
	_dbHelper.CreateInParameter("@product_link",DbType.String,model.product_link),
	_dbHelper.CreateInParameter("@product_image",DbType.String,model.product_image),
	_dbHelper.CreateInParameter("@sort_order",DbType.Int32,model.sort_order),
	_dbHelper.CreateInParameter("@created_by_user_id",DbType.Guid,model.created_by_user_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction("dbo.website_product_create", parameters);
                if ((result != null && !string.IsNullOrEmpty(result.ErrorMessage)) && result.ErrorCode!=0)
                {
                    throw new Exception(result.ErrorMessage);
                }
                else if (result.Value !=null && result.Value.ToString().IndexOf("MESSAGE") >= 0)
                {
                    throw new Exception(result.Value.ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }
/// <summary>
/// Update information in the tableWebsiteProduct
/// </summary>
/// <param name="model">the record updated</param>
/// <returns></returns>
public bool Update(WebsiteProductModel model)
		 {
           try
            {
                
                var parameters = new List<IDbDataParameter>
                {
                   	 _dbHelper.CreateInParameter("@product_id",DbType.Guid,model.product_id),
	 _dbHelper.CreateInParameter("@product_name_l",DbType.String,model.product_name_l),
	 _dbHelper.CreateInParameter("@product_name_e",DbType.String,model.product_name_e),
	 _dbHelper.CreateInParameter("@product_description_l",DbType.String,model.product_description_l),
	 _dbHelper.CreateInParameter("@product_description_e",DbType.String,model.product_description_e),
	 _dbHelper.CreateInParameter("@product_link",DbType.String,model.product_link),
	 _dbHelper.CreateInParameter("@product_image",DbType.String,model.product_image),
	 _dbHelper.CreateInParameter("@sort_order",DbType.Int32,model.sort_order),
	 _dbHelper.CreateInParameter("@lu_user_id",DbType.Guid,model.lu_user_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction("dbo.website_product_update", parameters);
                if ((result != null && !string.IsNullOrEmpty(result.ErrorMessage)) && result.ErrorCode!=0)
                {
                    throw new Exception(result.ErrorMessage);
                }
                else if (result.Value !=null && result.Value.ToString().IndexOf("MESSAGE") >= 0)
                {
                    throw new Exception(result.Value.ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
/// <summary>
/// Searching information in the table WebsiteProduct
/// </summary>
/// <param name="pageIndex">which page?</param>
/// <param name="pageSize">the number of records in a page</param>
/// <param name="lang"> Language used to display data</param>
/// <param name="total">the total number of records</param> 
/// <returns></returns>
public List<WebsiteProductModel> Search(int pageIndex, int pageSize, char lang, out long total  , string product_name) 
		 { 
            total = 0;
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@page_index", DbType.Int32, pageIndex),
                    _dbHelper.CreateInParameter("@page_size", DbType.Int32,  pageSize),
                    _dbHelper.CreateInParameter("@lang", DbType.String,  lang)
                   	,_dbHelper.CreateInParameter("@product_name" ,DbType.String,product_name),
                    _dbHelper.CreateOutParameter("@OUT_TOTAL_ROW", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<WebsiteProductModel>("dbo.website_product_search", parameters);
                if (!string.IsNullOrEmpty(result.ErrorMessage) && result.ErrorCode!=0)
                    throw new Exception(result.ErrorMessage);

                if (result.Output["OUT_TOTAL_ROW"] + "" != "")
                    total = Convert.ToInt32(result.Output["OUT_TOTAL_ROW"]);
                return result.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }
/// <summary>
/// Get the information by using id of the table WebsiteProduct
/// </summary>
/// <param name="id">Id used to get the information</param>
/// <returns></returns>
public WebsiteProductModel GetById(Guid? id)
		{ 
            try
            {
                var parameters = new List<IDbDataParameter>
                { 
                    _dbHelper.CreateInParameter("@product_id",DbType.Guid, id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToFirstOrDefault<WebsiteProductModel>("dbo.website_product_get_by_id", parameters);
                if (!string.IsNullOrEmpty(result.ErrorMessage) && result.ErrorCode!=0)
                {
                    throw new Exception(result.ErrorMessage);
                }
                return result.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }
/// <summary>
/// Delete records in the table WebsiteProduct 
/// </summary>
/// <param name="json_list_id">List id want to delete</param>
/// <param name="updated_by">User made the deletion</param>
/// <returns></returns>
public List<WebsiteProductModel> Delete(string json_list_id,Guid updated_by)
		{
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@json_list_id", DbType.String, json_list_id),
                    _dbHelper.CreateInParameter("@updated_by", DbType.Guid, updated_by),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<WebsiteProductModel>("dbo.website_product_delete_multi", parameters);
                if (!string.IsNullOrEmpty(result.ErrorMessage) && result.ErrorCode!=0)
                {
                    throw new Exception(result.ErrorMessage);
                }
                return result.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }
		 
    }
}