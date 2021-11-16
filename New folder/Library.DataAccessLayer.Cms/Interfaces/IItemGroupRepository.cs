using Library.Cms.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Cms.DataAccessLayer
{
    public partial interface IItemGroupRepository
    {
        bool Create(ItemGroupModel model);
        bool Update(ItemGroupModel model);
        List<ItemGroupModel> GetDataTree(char lang);
        List<ItemGroupModel> GetDataTreeAll();
        ItemGroupModel GetById(Guid id);
        List<ItemGroupModel> Delete(string json_list_id, Guid updated_by);
        List<DropdownOptionModel> GetListDropdown(char lang);
        List<ItemGroupPrintModel> ExportExcel(char lang);
    }
}
