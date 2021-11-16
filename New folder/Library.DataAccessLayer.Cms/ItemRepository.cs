using Library.Cms.DataModel;
using Library.Common.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Library.Cms.DataAccessLayer
{
    public partial class ItemRepository : IItemRepository
    {
        private IDatabaseHelper _dbHelper;
        public ItemRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        /// <summary>
        /// Add a new records into the table item
        /// </summary>
        /// <param name="model">the record added </param>
        /// <returns></returns>
        public bool Create(ItemModel model)
        {
            try
            {

                if (model.item_id == Guid.Empty) model.item_id = Guid.NewGuid();
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@item_id",DbType.Guid,model.item_id),
                    _dbHelper.CreateInParameter("@item_group_id",DbType.Guid,model.item_group_id),
                    _dbHelper.CreateInParameter("@image_url",DbType.String,model.image_url),
                    _dbHelper.CreateInParameter("@item_title_e",DbType.String,model.item_title_e),
                    _dbHelper.CreateInParameter("@item_title_l",DbType.String,model.item_title_l),
                    _dbHelper.CreateInParameter("@item_sub_title_e",DbType.String,model.item_sub_title_e),
                    _dbHelper.CreateInParameter("@item_sub_title_l",DbType.String,model.item_sub_title_l),
                    _dbHelper.CreateInParameter("@item_detail_e",DbType.String,model.item_detail_e),
                    _dbHelper.CreateInParameter("@item_detail_l",DbType.String,model.item_detail_l),
                    _dbHelper.CreateInParameter("@item_type_rcd",DbType.String,model.item_type_rcd),
                    _dbHelper.CreateInParameter("@item_status_rcd",DbType.String,model.item_status_rcd),
                    _dbHelper.CreateInParameter("@published_date_time",DbType.DateTime,model.published_date_time),
                    _dbHelper.CreateInParameter("@published_by_user_id",DbType.Guid,model.published_by_user_id),
                    _dbHelper.CreateInParameter("@created_by_user_id",DbType.Guid,model.created_by_user_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction("dbo.website_item_create", parameters);
                if ((result != null && !string.IsNullOrEmpty(result.ErrorMessage)) && result.ErrorCode != 0)
                {
                    throw new Exception(result.ErrorMessage);
                }
                else if (result.Value != null && result.Value.ToString().IndexOf("MESSAGE") >= 0)
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
        /// Update information in the table item
        /// </summary>
        /// <param name="model">the record updated</param>
        /// <returns></returns>
        public bool Update(ItemModel model)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                     _dbHelper.CreateInParameter("@item_id",DbType.Guid,model.item_id),
                     _dbHelper.CreateInParameter("@item_group_id",DbType.Guid,model.item_group_id),
                     _dbHelper.CreateInParameter("@image_url",DbType.String,model.image_url),
                     _dbHelper.CreateInParameter("@item_title_e",DbType.String,model.item_title_e),
                     _dbHelper.CreateInParameter("@item_title_l",DbType.String,model.item_title_l),
                     _dbHelper.CreateInParameter("@item_sub_title_e",DbType.String,model.item_sub_title_e),
                     _dbHelper.CreateInParameter("@item_sub_title_l",DbType.String,model.item_sub_title_l),
                     _dbHelper.CreateInParameter("@item_detail_e",DbType.String,model.item_detail_e),
                     _dbHelper.CreateInParameter("@item_detail_l",DbType.String,model.item_detail_l),
                     _dbHelper.CreateInParameter("@item_type_rcd",DbType.String,model.item_type_rcd),
                     _dbHelper.CreateInParameter("@item_status_rcd",DbType.String,model.item_status_rcd),
                     _dbHelper.CreateInParameter("@published_date_time",DbType.DateTime,model.published_date_time),
                     _dbHelper.CreateInParameter("@published_by_user_id",DbType.Guid,model.published_by_user_id),
                     _dbHelper.CreateInParameter("@lu_user_id",DbType.Guid,model.lu_user_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction("dbo.website_item_update", parameters);
                if ((result != null && !string.IsNullOrEmpty(result.ErrorMessage)) && result.ErrorCode != 0)
                {
                    throw new Exception(result.ErrorMessage);
                }
                else if (result.Value != null && result.Value.ToString().IndexOf("MESSAGE") >= 0)
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
        public bool UpdateDown(Guid id, Guid updated_by)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@item_id",DbType.Guid, id),
                    _dbHelper.CreateInParameter("@lu_user_id",DbType.Guid, updated_by),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction("dbo.website_item_update_down", parameters);
                if ((result != null && !string.IsNullOrEmpty(result.ErrorMessage)) && result.ErrorCode != 0)
                {
                    throw new Exception(result.ErrorMessage);
                }
                else if (result.Value != null && result.Value.ToString().IndexOf("MESSAGE") >= 0)
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
        public bool UpdateUndo(Guid id, Guid updated_by)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@item_id",DbType.Guid, id),
                    _dbHelper.CreateInParameter("@lu_user_id",DbType.Guid, updated_by),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction("dbo.website_item_update_undo", parameters);
                if ((result != null && !string.IsNullOrEmpty(result.ErrorMessage)) && result.ErrorCode != 0)
                {
                    throw new Exception(result.ErrorMessage);
                }
                else if (result.Value != null && result.Value.ToString().IndexOf("MESSAGE") >= 0)
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
        /// Get the information by using id of the table item
        /// </summary>
        /// <param name="id">Id used to get the information</param>
        /// <returns></returns>
        public ItemModel GetById(Guid id)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@item_id",DbType.Guid, id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToFirstOrDefault<ItemModel>("dbo.website_item_get_by_id", parameters);
                if (!string.IsNullOrEmpty(result.ErrorMessage) && result.ErrorCode != 0)
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
        /// Delete records in the table item
        /// </summary>
        /// <param name="json_list_id">List id want to delete</param>
        /// <param name="updated_by">User made the deletion</param>
        /// <returns></returns>
        public List<ItemModel> Delete(string json_list_id, Guid updated_by)
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
                var result = _dbHelper.CallToList<ItemModel>("dbo.website_item_delete_multi", parameters);
                if (!string.IsNullOrEmpty(result.ErrorMessage) && result.ErrorCode != 0)
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
        public bool Remove(string json_list_id)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@json_list_id", DbType.String, json_list_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction("dbo.website_item_remove_multi", parameters);
                if ((result != null && !string.IsNullOrEmpty(result.ErrorMessage)) && result.ErrorCode != 0)
                {
                    throw new Exception(result.ErrorMessage);
                }
                else if (result.Value != null && result.Value.ToString().IndexOf("MESSAGE") >= 0)
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
        /// Searching information in the table item
        /// </summary>
        /// <param name="pageIndex">Which page?</param>
        /// <param name="pageSize">The number of records in a page</param>
        /// <param name="lang"> Language used to display data</param>
        /// <param name="total">The total number of records</param> 
        /// <returns></returns>
        public List<ItemModel> Search(int pageIndex, int pageSize, char lang, out long total, string item_status_rcd, string item_type_rcd, string content_search, DateTime? fr_published_date_time, DateTime? to_published_date_time, Guid? published_by_user_id, DateTime? fr_created_date_time, DateTime? to_created_date_time, Guid? created_by_user_id, Guid? item_group_id, Guid? user_id)
        {
            total = 0;
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@page_index", DbType.Int32, pageIndex),
                    _dbHelper.CreateInParameter("@page_size", DbType.Int32,  pageSize),
                    _dbHelper.CreateInParameter("@lang", DbType.String,  lang)
                    ,_dbHelper.CreateInParameter("@item_type_rcd" ,DbType.String,item_type_rcd)
                    ,_dbHelper.CreateInParameter("@item_status_rcd" ,DbType.String,item_status_rcd)
                    ,_dbHelper.CreateInParameter("@content_search" ,DbType.String,content_search)
                    ,_dbHelper.CreateInParameter("@fr_published_date_time" ,DbType.DateTime,fr_published_date_time)
                    ,_dbHelper.CreateInParameter("@to_published_date_time" ,DbType.DateTime,to_published_date_time)
                    ,_dbHelper.CreateInParameter("@published_by_user_id" ,DbType.Guid,published_by_user_id)
                    ,_dbHelper.CreateInParameter("@fr_created_date_time" ,DbType.DateTime,fr_created_date_time)
                    ,_dbHelper.CreateInParameter("@to_created_date_time" ,DbType.DateTime,to_created_date_time)
                    ,_dbHelper.CreateInParameter("@created_by_user_id" ,DbType.Guid,created_by_user_id)
                    ,_dbHelper.CreateInParameter("@item_group_id" ,DbType.Guid,item_group_id)
                    ,_dbHelper.CreateInParameter("@user_id" ,DbType.Guid,user_id),
                    _dbHelper.CreateOutParameter("@OUT_TOTAL_ROW", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<ItemModel>("dbo.website_item_search", parameters);
                if (!string.IsNullOrEmpty(result.ErrorMessage) && result.ErrorCode != 0)
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
    }
}
