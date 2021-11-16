using System;
using System.Collections.Generic;
using Library.Cms.DataModel;
using Library.Common.Helper;
using System.Data;
using System.Linq; 

namespace Library.Cms.DataAccessLayer {
    public partial class WebsiteSlideRepository : IWebsiteSlideRepository 
	{
		private IDatabaseHelper _dbHelper; 
		public WebsiteSlideRepository(IDatabaseHelper dbHelper)
		{
			_dbHelper = dbHelper; 
		}  
		/// <summary>
/// Add a new records into the table WebsiteSlide 
/// </summary>
/// <param name="model">the record added </param>
/// <returns></returns>
public bool Create(WebsiteSlideModel model) 
		{
           try
            {
                
				if (model.slide_id == null || model.slide_id == Guid.Empty)   model.slide_id = Guid.NewGuid(); 
                var parameters = new List<IDbDataParameter>
                {
                    	_dbHelper.CreateInParameter("@slide_id",DbType.Guid,model.slide_id),
	_dbHelper.CreateInParameter("@slide_title_l",DbType.String,model.slide_title_l),
	_dbHelper.CreateInParameter("@slide_title_e",DbType.String,model.slide_title_e),
	_dbHelper.CreateInParameter("@slide_image",DbType.String,model.slide_image),
	_dbHelper.CreateInParameter("@slide_url",DbType.String,model.slide_url),
	_dbHelper.CreateInParameter("@slide_type",DbType.Int32,model.slide_type),
	_dbHelper.CreateInParameter("@sort_order",DbType.Int32,model.sort_order),
	_dbHelper.CreateInParameter("@is_show",DbType.Boolean,model.is_show),
	_dbHelper.CreateInParameter("@created_by_user_id",DbType.Guid,model.created_by_user_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction("dbo.website_slide_create", parameters);
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
/// Update information in the tableWebsiteSlide
/// </summary>
/// <param name="model">the record updated</param>
/// <returns></returns>
public bool Update(WebsiteSlideModel model)
		 {
           try
            {
                
                var parameters = new List<IDbDataParameter>
                {
                   	 _dbHelper.CreateInParameter("@slide_id",DbType.Guid,model.slide_id),
	 _dbHelper.CreateInParameter("@slide_title_l",DbType.String,model.slide_title_l),
	 _dbHelper.CreateInParameter("@slide_title_e",DbType.String,model.slide_title_e),
	 _dbHelper.CreateInParameter("@slide_image",DbType.String,model.slide_image),
	 _dbHelper.CreateInParameter("@slide_url",DbType.String,model.slide_url),
	 _dbHelper.CreateInParameter("@slide_type",DbType.Int32,model.slide_type),
	 _dbHelper.CreateInParameter("@sort_order",DbType.Int32,model.sort_order),
	 _dbHelper.CreateInParameter("@is_show",DbType.Boolean,model.is_show),
	 _dbHelper.CreateInParameter("@lu_user_id",DbType.Guid,model.lu_user_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction("dbo.website_slide_update", parameters);
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
/// Searching information in the table WebsiteSlide
/// </summary>
/// <param name="pageIndex">which page?</param>
/// <param name="pageSize">the number of records in a page</param>
/// <param name="lang"> Language used to display data</param>
/// <param name="total">the total number of records</param> 
/// <returns></returns>
public List<WebsiteSlideModel> Search(int pageIndex, int pageSize, char lang, out long total  , string slide_title) 
		 { 
            total = 0;
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@page_index", DbType.Int32, pageIndex),
                    _dbHelper.CreateInParameter("@page_size", DbType.Int32,  pageSize),
                    _dbHelper.CreateInParameter("@lang", DbType.String,  lang)
                   	,_dbHelper.CreateInParameter("@slide_title" ,DbType.String,slide_title),
                    _dbHelper.CreateOutParameter("@OUT_TOTAL_ROW", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<WebsiteSlideModel>("dbo.website_slide_search", parameters);
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
/// Get the information by using id of the table WebsiteSlide
/// </summary>
/// <param name="id">Id used to get the information</param>
/// <returns></returns>
public WebsiteSlideModel GetById(Guid? id)
		{ 
            try
            {
                var parameters = new List<IDbDataParameter>
                { 
                    _dbHelper.CreateInParameter("@slide_id",DbType.Guid, id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToFirstOrDefault<WebsiteSlideModel>("dbo.website_slide_get_by_id", parameters);
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
/// Delete records in the table WebsiteSlide 
/// </summary>
/// <param name="json_list_id">List id want to delete</param>
/// <param name="updated_by">User made the deletion</param>
/// <returns></returns>
public List<WebsiteSlideModel> Delete(string json_list_id,Guid updated_by)
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
                var result = _dbHelper.CallToList<WebsiteSlideModel>("dbo.website_slide_delete_multi", parameters);
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