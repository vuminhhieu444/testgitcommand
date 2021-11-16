using Library.Cms.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Cms.BusinessLogicLayer
{
    public partial interface IItemBusiness
    {
        bool Create(ItemModel model);
        bool Update(ItemModel model);
        bool UpdateDown(Guid id, Guid updated_by);
        bool UpdateUndo(Guid id, Guid updated_by);
        ItemModel GetById(Guid id);
        List<ItemModel> Search(int pageIndex, int pageSize, char lang, out long total, string item_status_rcd, string item_type_rcd, string content_search, DateTime? fr_published_date_time, DateTime? to_published_date_time, Guid? published_by_user_id, DateTime? fr_created_date_time, DateTime? to_created_date_time, Guid? created_by_user_id, Guid? item_group_id, Guid? user_id);
        List<ItemModel> Delete(string json_list_id, Guid updated_by);
        bool Remove(string json_list_id);
    }
}
