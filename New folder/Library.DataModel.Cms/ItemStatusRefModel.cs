using System;

namespace Library.Cms.DataModel
{
    public class ItemStatusRefModel
    {
        public string item_status_rcd { get; set; }
        public string item_status_name { get; set; }
        public string item_status_name_e { get; set; }
        public string item_status_name_l { get; set; }
        public short? seq_num { get; set; }
        public string created_by_user_id { get; set; }
        public DateTime created_date_time { get; set; }
        public DateTime lu_updated { get; set; }
        public string lu_user_id { get; set; }
        public int active_flag { get; set; }
    }
}
