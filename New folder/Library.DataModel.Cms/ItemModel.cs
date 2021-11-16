using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Cms.DataModel
{
    public partial class ItemModel
    {
        public Guid item_id { get; set; }
        public Guid item_group_id { get; set; }
        public string image_url { get; set; }
        public string item_title { get; set; }
        public string item_title_e { get; set; }
        public string path_menu_e { get; set; }
        public string path_menu_l { get; set; }
        public string item_title_l { get; set; }
        public string item_sub_title { get; set; }
        public string group_type_rcd { get; set; }
        public string item_sub_title_e { get; set; }
        public string item_sub_title_l { get; set; }
        public string item_detail { get; set; }
        public string item_detail_e { get; set; }
        public string item_detail_l { get; set; }
        public int hit_counts { get; set; }
        public string item_type_rcd { get; set; }
        public string item_status_rcd { get; set; }
        public string created_by_user_name { get; set; }
        public string published_by_user_name { get; set; }
        public DateTime? published_date_time { get; set; }
        public Guid published_by_user_id { get; set; }
        public string item_group_name { get; set; }
        public string item_status_name { get; set; }
        public string item_type_name { get; set; }
        public Guid created_by_user_id { get; set; }
        public DateTime created_date_time { get; set; }
        public DateTime lu_updated { get; set; }
        public Guid? lu_user_id { get; set; }
        public int active_flag { get; set; }
        public List<ItemModel> listjson_item { get; set; }
        public List<ItemGroupModel> listjson_item_group { get; set; }
    } 
}
