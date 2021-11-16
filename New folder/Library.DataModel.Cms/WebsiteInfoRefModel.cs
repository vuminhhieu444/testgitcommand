using System;
using System.Collections.Generic;
namespace Library.Cms.DataModel 
{
	public partial class WebsiteInfoRefModel
	{
		public string web_info_rcd { get; set; }
		public string web_info_logo_l { get; set; }
		public string web_info_logo { get; set; }
		public string web_info_logo_e { get; set; }
		public string web_info_slogan_l { get; set; }
		public string web_info_slogan { get; set; }
		public string web_info_slogan_e { get; set; }
		public string web_info_faculty { get; set; }
		public string web_info_faculty_e { get; set; }
		public string web_info_faculty_l { get; set; }
		public string web_info_address { get; set; }
		public string web_info_email { get; set; }
		public string web_info_phone { get; set; }
		public string web_info_website { get; set; }
		public string web_info_facebook { get; set; }
		public string web_info_zalo { get; set; }
		public string web_info_youtube { get; set; }
		public int? sort_order { get; set; }
		public Guid created_by_user_id { get; set; }
		public DateTime created_date_time { get; set; }
		public DateTime? lu_updated { get; set; }
		public Guid? lu_user_id { get; set; }
		public int active_flag { get; set; }
	}
}