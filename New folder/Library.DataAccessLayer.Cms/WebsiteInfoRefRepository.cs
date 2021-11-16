using System;
using System.Collections.Generic;
using Library.Cms.DataModel;
using Library.Common.Helper;
using System.Data;
using System.Linq; 

namespace Library.Cms.DataAccessLayer {
    public partial class WebsiteInfoRefRepository : IWebsiteInfoRefRepository 
	{
		private IDatabaseHelper _dbHelper; 
		public WebsiteInfoRefRepository(IDatabaseHelper dbHelper)
		{
			_dbHelper = dbHelper; 
		}  
		/// <summary>
/// Add a new records into the table WebsiteInfoRef 
/// </summary>
/// <param name="model">the record added </param>
/// <returns></returns>
public bool Create(WebsiteInfoRefModel model) 
		{
           try
            {
                
				 
                var parameters = new List<IDbDataParameter>
                {
                    	_dbHelper.CreateInParameter("@web_info_rcd",DbType.String,model.web_info_rcd),
	_dbHelper.CreateInParameter("@web_info_logo_l",DbType.String,model.web_info_logo_l),
	_dbHelper.CreateInParameter("@web_info_logo_e",DbType.String,model.web_info_logo_e),
	_dbHelper.CreateInParameter("@web_info_slogan_l",DbType.String,model.web_info_slogan_l),
	_dbHelper.CreateInParameter("@web_info_slogan_e",DbType.String,model.web_info_slogan_e),
	_dbHelper.CreateInParameter("@web_info_faculty_e",DbType.String,model.web_info_faculty_e),
	_dbHelper.CreateInParameter("@web_info_faculty_l",DbType.String,model.web_info_faculty_l),
	_dbHelper.CreateInParameter("@web_info_address",DbType.String,model.web_info_address),
	_dbHelper.CreateInParameter("@web_info_email",DbType.String,model.web_info_email),
	_dbHelper.CreateInParameter("@web_info_phone",DbType.String,model.web_info_phone),
	_dbHelper.CreateInParameter("@web_info_website",DbType.String,model.web_info_website),
	_dbHelper.CreateInParameter("@web_info_facebook",DbType.String,model.web_info_facebook),
	_dbHelper.CreateInParameter("@web_info_zalo",DbType.String,model.web_info_zalo),
	_dbHelper.CreateInParameter("@web_info_youtube",DbType.String,model.web_info_youtube),
	_dbHelper.CreateInParameter("@sort_order",DbType.Int32,model.sort_order),
	_dbHelper.CreateInParameter("@created_by_user_id",DbType.Guid,model.created_by_user_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction("dbo.website_info_ref_create", parameters);
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
/// Update information in the tableWebsiteInfoRef
/// </summary>
/// <param name="model">the record updated</param>
/// <returns></returns>
public bool Update(WebsiteInfoRefModel model)
		 {
           try
            {
                
                var parameters = new List<IDbDataParameter>
                {
                   	 _dbHelper.CreateInParameter("@web_info_rcd",DbType.String,model.web_info_rcd),
	 _dbHelper.CreateInParameter("@web_info_logo_l",DbType.String,model.web_info_logo_l),
	 _dbHelper.CreateInParameter("@web_info_logo_e",DbType.String,model.web_info_logo_e),
	 _dbHelper.CreateInParameter("@web_info_slogan_l",DbType.String,model.web_info_slogan_l),
	 _dbHelper.CreateInParameter("@web_info_slogan_e",DbType.String,model.web_info_slogan_e),
	 _dbHelper.CreateInParameter("@web_info_faculty_e",DbType.String,model.web_info_faculty_e),
	 _dbHelper.CreateInParameter("@web_info_faculty_l",DbType.String,model.web_info_faculty_l),
	 _dbHelper.CreateInParameter("@web_info_address",DbType.String,model.web_info_address),
	 _dbHelper.CreateInParameter("@web_info_email",DbType.String,model.web_info_email),
	 _dbHelper.CreateInParameter("@web_info_phone",DbType.String,model.web_info_phone),
	 _dbHelper.CreateInParameter("@web_info_website",DbType.String,model.web_info_website),
	 _dbHelper.CreateInParameter("@web_info_facebook",DbType.String,model.web_info_facebook),
	 _dbHelper.CreateInParameter("@web_info_zalo",DbType.String,model.web_info_zalo),
	 _dbHelper.CreateInParameter("@web_info_youtube",DbType.String,model.web_info_youtube),
	 _dbHelper.CreateInParameter("@sort_order",DbType.Int32,model.sort_order),
	 _dbHelper.CreateInParameter("@lu_user_id",DbType.Guid,model.lu_user_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction("dbo.website_info_ref_update", parameters);
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
/// Searching information in the table WebsiteInfoRef
/// </summary>
/// <param name="pageIndex">which page?</param>
/// <param name="pageSize">the number of records in a page</param>
/// <param name="lang"> Language used to display data</param>
/// <param name="total">the total number of records</param> 
/// <returns></returns>
public List<WebsiteInfoRefModel> Search(int pageIndex, int pageSize, char lang, out long total , string web_info_rcd , string web_info_faculty) 
		 { 
            total = 0;
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@page_index", DbType.Int32, pageIndex),
                    _dbHelper.CreateInParameter("@page_size", DbType.Int32,  pageSize),
                    _dbHelper.CreateInParameter("@lang", DbType.String,  lang)
                   	,_dbHelper.CreateInParameter("@web_info_rcd" ,DbType.String,web_info_rcd)
	,_dbHelper.CreateInParameter("@web_info_faculty" ,DbType.String,web_info_faculty),
                    _dbHelper.CreateOutParameter("@OUT_TOTAL_ROW", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<WebsiteInfoRefModel>("dbo.website_info_ref_search", parameters);
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
/// Get the information by using id of the table WebsiteInfoRef
/// </summary>
/// <param name="id">Id used to get the information</param>
/// <returns></returns>
public WebsiteInfoRefModel GetById(string id)
		{ 
            try
            {
                var parameters = new List<IDbDataParameter>
                { 
                    _dbHelper.CreateInParameter("@web_info_rcd",DbType.String, id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToFirstOrDefault<WebsiteInfoRefModel>("dbo.website_info_ref_get_by_id", parameters);
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
/// Delete records in the table WebsiteInfoRef 
/// </summary>
/// <param name="json_list_id">List id want to delete</param>
/// <param name="updated_by">User made the deletion</param>
/// <returns></returns>
public List<WebsiteInfoRefModel> Delete(string json_list_id,Guid updated_by)
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
                var result = _dbHelper.CallToList<WebsiteInfoRefModel>("dbo.website_info_ref_delete_multi", parameters);
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