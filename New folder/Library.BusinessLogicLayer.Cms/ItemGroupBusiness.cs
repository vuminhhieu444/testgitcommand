using Library.Cms.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library.Cms.DataAccessLayer;

namespace Library.Cms.BusinessLogicLayer
{
    public partial class ItemGroupBusiness : IItemGroupBusiness
    {
        private IItemGroupRepository _res;
        public ItemGroupBusiness(IItemGroupRepository ItemGroupRes)
        {
            _res = ItemGroupRes;
        }
        /// <summary>
        /// Add a new records into the table item_group 
        /// </summary>
        /// <param name="model">the record added </param>
        /// <returns></returns>
        public bool Create(ItemGroupModel model)
        {
            return _res.Create(model);
        }
        /// <summary>
        /// Update information in the table item_group
        /// </summary>
        /// <param name="model">the record updated</param>
        /// <returns></returns>
        public bool Update(ItemGroupModel model)
        {
            return _res.Update(model);
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
            var allItemGroups = _res.GetDataTree(lang);
            foreach (var item in allItemGroups)
            {
                string image_url = item.image_url;
                if (!string.IsNullOrEmpty(image_url) && image_url.Contains(";"))
                    item.image_url = image_url.Split(';')[0];
            }
            var lstParent = allItemGroups.Where(ds => ds.parent_item_group_id == null).OrderBy(s => s.sort_order).ToList();
            foreach (var item in lstParent)
            {
                item.children = GetHiearchyList(allItemGroups, item);
            }
            return lstParent;
        }
        public List<ItemGroupModel> GetDataTreeAll()
        {
            var allItemGroups = _res.GetDataTreeAll();
            foreach (var item in allItemGroups)
            {
                string image_url = item.image_url;
                if (!string.IsNullOrEmpty(image_url) && image_url.Contains(";"))
                    item.image_url = image_url.Split(';')[0];
            }
            var lstParent = allItemGroups.Where(ds => ds.parent_item_group_id == null).OrderBy(s => s.sort_order).ToList();
            foreach (var item in lstParent)
            {
                item.children = GetHiearchyList(allItemGroups, item);
            }
            return lstParent;
        }
        public List<ItemGroupModel> GetHiearchyList(List<ItemGroupModel> lstAll, ItemGroupModel node)
        {
            var lstChilds = lstAll.Where(ds => ds.parent_item_group_id == node.item_group_id).ToList();
            if (lstChilds.Count == 0)
                return null;
            for (int i = 0; i < lstChilds.Count; i++)
            {
                var childs = GetHiearchyList(lstAll, lstChilds[i]);
                lstChilds[i].type = (childs == null || childs.Count == 0) ? "leaf" : "";
                lstChilds[i].children = childs;
            }
            return lstChilds.OrderBy(s => s.sort_order).ToList();
        }
        /// <summary>
        /// Get the information by using id of the table item_group
        /// </summary>
        /// <param name="id">Id used to get the information</param>
        /// <returns></returns>
        public ItemGroupModel GetById(Guid id)
        {
            var result = _res.GetById(id);
            return result;
        }
        /// <summary>
        /// Delete records in the table item_group 
        /// </summary>
        /// <param name="json_list_id">List id want to delete</param>
        /// <param name="updated_by">User made the deletion</param>
        /// <returns></returns>
        public List<ItemGroupModel> Delete(string json_list_id, Guid updated_by)
        {
            return _res.Delete(json_list_id, updated_by);
        }
        /// <summary>
        /// Get information from the table item_group and push it into a list of type DropdownOptionModel
        /// </summary>
        /// <param name="lang">Language used to display data</param> 
        /// <returns></returns>
        List<DropdownOptionModel> list = null;
        public List<DropdownOptionModel> GetListDropdown(char lang)
        {
            list = new List<DropdownOptionModel>();
            var allItemGroups = _res.GetDataTree(lang);
            var lstParent = allItemGroups.Where(ds => ds.parent_item_group_id == null).ToList();
            for (int i = 0; i < lstParent.Count; i++)
            {
                if (i == lstParent.Count - 1)
                {
                    lstParent[i].last_flag = true;
                }
                lstParent[i].root_flag = true;
                lstParent[i].children = GetHiearchyListTree(allItemGroups, lstParent[i]);
            }
            return list;
        }
        public List<ItemGroupModel> GetHiearchyListTree(List<ItemGroupModel> lstAll, ItemGroupModel node)
        {
            var level = 0;
            if (!string.IsNullOrEmpty(node.key_struct))
            {
                level = node.key_struct.Split('/').Length;
            }
            list.Add(new DropdownOptionModel() { value = node.item_group_id.ToString(), label = node.item_group_name, level = level });
            var lstChilds = lstAll.Where(ds => ds.parent_item_group_id == node.item_group_id).ToList();
            if (lstChilds.Count == 0)
            {
                return null;
            }
            for (int i = 0; i < lstChilds.Count; i++)
            {
                var childs = GetHiearchyListTree(lstAll, lstChilds[i]);
                lstChilds[i].type = (childs == null || childs.Count == 0) ? "leaf" : "";
                lstChilds[i].children = childs;
            }
            return lstChilds.OrderBy(ds => ds.item_group_name).ToList();
        }

        /// <summary>sp_surgicaltrick_request_done
        /// Export data to excel from the view item_group 
        /// </summary>
        /// <param name="pageIndex">which page?</param>
        /// <param name="pageSize">the number of records in a page</param>
        /// <param name="lang">Language used to display data</param>
        /// <param name="total">the total number of records</param>
        /// <returns></returns>
        public List<ItemGroupPrintModel> ExportExcel(char lang)
        {
            return _res.ExportExcel(lang);
        }
    }
}
