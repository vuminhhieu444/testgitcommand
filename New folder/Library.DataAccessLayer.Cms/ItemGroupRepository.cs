using Library.Cms.DataModel;
using Library.Common.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Library.Cms.DataAccessLayer
{
    public partial class ItemGroupRepository : IItemGroupRepository
    {
        private IDatabaseHelper _dbHelper;
        public ItemGroupRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        /// <summary>
        /// Add a new records into the table item_group 
        /// </summary>
        /// <param name="model">the record added </param>
        /// <returns></returns>
        public bool Create(ItemGroupModel model)
        {
            try
            {
                if (model.item_group_id == Guid.Empty) model.item_group_id = Guid.NewGuid();
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@parent_item_group_id",DbType.Guid,model.parent_item_group_id),
                    _dbHelper.CreateInParameter("@item_group_id",DbType.Guid,model.item_group_id),
                    _dbHelper.CreateInParameter("@item_group_name_e",DbType.String,model.item_group_name_e),
                    _dbHelper.CreateInParameter("@item_group_name_l",DbType.String,model.item_group_name_l),
                    _dbHelper.CreateInParameter("@item_group_code",DbType.String,model.item_group_code),
                    _dbHelper.CreateInParameter("@item_group_url",DbType.String,model.item_group_url),
                    _dbHelper.CreateInParameter("@group_type_rcd",DbType.String,model.group_type_rcd),
                    _dbHelper.CreateInParameter("@icon_class",DbType.String,model.icon_class),
                    _dbHelper.CreateInParameter("@sort_order",DbType.Int32,model.sort_order),
                    _dbHelper.CreateInParameter("@image_url",DbType.String,model.image_url),
                    _dbHelper.CreateInParameter("@created_by_user_id",DbType.Guid,model.created_by_user_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction("dbo.website_item_group_create", parameters);
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
        /// Update information in the table item_group
        /// </summary>
        /// <param name="model">the record updated</param>
        /// <returns></returns>
        public bool Update(ItemGroupModel model)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                     _dbHelper.CreateInParameter("@parent_item_group_id",DbType.Guid,model.parent_item_group_id),
                     _dbHelper.CreateInParameter("@item_group_id",DbType.Guid,model.item_group_id),
                     _dbHelper.CreateInParameter("@item_group_name_e",DbType.String,model.item_group_name_e),
                     _dbHelper.CreateInParameter("@item_group_name_l",DbType.String,model.item_group_name_l),
                     _dbHelper.CreateInParameter("@item_group_code",DbType.String,model.item_group_code),
                     _dbHelper.CreateInParameter("@item_group_url",DbType.String,model.item_group_url),
                     _dbHelper.CreateInParameter("@group_type_rcd",DbType.String,model.group_type_rcd),
                     _dbHelper.CreateInParameter("@icon_class",DbType.String,model.icon_class),
                     _dbHelper.CreateInParameter("@sort_order",DbType.Int32,model.sort_order),
                     _dbHelper.CreateInParameter("@image_url",DbType.String,model.image_url),
                     _dbHelper.CreateInParameter("@lu_user_id",DbType.Guid,model.lu_user_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction("dbo.website_item_group_update", parameters);
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
        /// Searching information in the table item_group
        /// </summary>
        /// <param name="pageIndex">which page?</param>
        /// <param name="pageSize">the number of records in a page</param>
        /// <param name="lang"> Language used to display data</param>
        /// <param name="total">the total number of records</param> 
        /// <returns></returns>
        public List<ItemGroupModel> GetDataTree(char lang)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@lang", DbType.String,  lang),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<ItemGroupModel>("dbo.website_item_group_get_data_tree", parameters);
                if (!string.IsNullOrEmpty(result.ErrorMessage) && result.ErrorCode != 0)
                    throw new Exception(result.ErrorMessage);
                return result.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ItemGroupModel> GetDataTreeAll()
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<ItemGroupModel>("dbo.website_item_group_get_data_tree_all", parameters);
                if (!string.IsNullOrEmpty(result.ErrorMessage) && result.ErrorCode != 0)
                    throw new Exception(result.ErrorMessage);
                return result.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get the information by using id of the table item_group
        /// </summary>
        /// <param name="id">Id used to get the information</param>
        /// <returns></returns>
        public ItemGroupModel GetById(Guid id)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@item_group_id",DbType.Guid, id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToFirstOrDefault<ItemGroupModel>("dbo.website_item_group_get_by_id", parameters);
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
        /// Delete records in the table item_group 
        /// </summary>
        /// <param name="json_list_id">List id want to delete</param>
        /// <param name="updated_by">User made the deletion</param>
        /// <returns></returns>
        public List<ItemGroupModel> Delete(string json_list_id, Guid updated_by)
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
                var result = _dbHelper.CallToList<ItemGroupModel>("dbo.website_item_group_delete_multi", parameters);
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
        /// Get information from the table item_group and push it into a list of type DropdownOptionModel
        /// </summary>
        /// <param name="lang">Language used to display data</param> 
        /// <returns></returns>
        public List<DropdownOptionModel> GetListDropdown(char lang)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@lang", DbType.String, lang),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<DropdownOptionModel>("dbo.website_item_group_get_list_dropdown", parameters);
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
        /// Export data to excel from the table item_group 
        /// <returns></returns>
        public List<ItemGroupPrintModel> ExportExcel(char lang)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@lang", DbType.String, lang),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<ItemGroupPrintModel>("dbo.website_item_group_exportexcel", parameters);
                if (!string.IsNullOrEmpty(result.ErrorMessage) && result.ErrorCode != 0)
                    throw new Exception(result.ErrorMessage);
                return result.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        } 
    }
}
