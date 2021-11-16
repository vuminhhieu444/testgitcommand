using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Cms.DataModel
{
    public partial class ItemGroupModel
    {
        public Guid? parent_item_group_id { get; set; }
        public Guid item_group_id { get; set; }
        public string item_group_name { get; set; }
        public string item_group_name_e { get; set; }
        public string item_group_name_l { get; set; }
        public string item_group_code { get; set; }
        public string item_group_url { get; set; }
        public string key_struct { get; set; }
        public string group_type_rcd { get; set; }
        public int? count_childs { get; set; }
        public string icon_class { get; set; }
        public int? sort_order { get; set; }
        public string image_url { get; set; }
        public Guid created_by_user_id { get; set; }
        public DateTime created_date_time { get; set; }
        public DateTime? lu_updated { get; set; }
        public Guid? lu_user_id { get; set; }
        public int active_flag { get; set; } 
        public string group_type_name { get; set; }
        public List<ItemGroupModel> children { get; set; }
        public string type { get; set; }
        public short level { get; set; }
        public string label { get; set; }
        public bool expanded { get; set; }
        public List<int> hide_levels { get; set; }
        public bool root_flag { get; set; }
        public bool last_flag { get; set; }
    } 
    public partial class ItemGroupPrintModel
    {
        public string item_group_code_parent { get; set; }
        public string item_group_code { get; set; }
        public string item_group_name { get; set; }
        public int? sort_order { get; set; }
    }
}
