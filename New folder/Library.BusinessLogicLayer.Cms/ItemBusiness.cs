using Library.Cms.DataAccessLayer;
using Library.Cms.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Cms.BusinessLogicLayer
{
    public partial class ItemBusiness : IItemBusiness
    {
        private IItemRepository _res;
        public ItemBusiness(IItemRepository itemRes)
        {
            _res = itemRes;
        }
        /// <summary>
        /// Add a new records into the table item
        /// </summary>
        /// <param name="model">the record added </param>
        /// <returns></returns>
        public bool Create(ItemModel model)
        {
            return _res.Create(model);
        }
        /// <summary>
        /// Update information in the table item
        /// </summary>
        /// <param name="model">the record updated</param>
        /// <returns></returns>
        public bool Update(ItemModel model)
        {
            return _res.Update(model);
        }
        public bool UpdateDown(Guid id, Guid updated_by)
        {
            return _res.UpdateDown(id, updated_by);
        }
        public bool UpdateUndo(Guid id, Guid updated_by)
        {
            return _res.UpdateUndo(id, updated_by);
        }
        /// <summary>
        /// Get the information by using id of the table item
        /// </summary>
        /// <param name="id">Id used to get the information</param>
        /// <returns></returns>
        public ItemModel GetById(Guid id)
        {
            var result = _res.GetById(id);
            return result;
        }
        /// <summary>
        /// Delete records in the table item 
        /// </summary>
        /// <param name="json_list_id">List id want to delete</param>
        /// <param name="updated_by">User made the deletion</param>
        /// <returns></returns>
        public List<ItemModel> Delete(string json_list_id, Guid updated_by)
        {
            return _res.Delete(json_list_id, updated_by);
        }
        public bool Remove(string json_list_id)
        {
            return _res.Remove(json_list_id);
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
            return _res.Search(pageIndex, pageSize, lang, out total, item_status_rcd, item_type_rcd, content_search, fr_published_date_time, to_published_date_time, published_by_user_id, fr_created_date_time, to_created_date_time, created_by_user_id, item_group_id, user_id);
        }
    }
}
