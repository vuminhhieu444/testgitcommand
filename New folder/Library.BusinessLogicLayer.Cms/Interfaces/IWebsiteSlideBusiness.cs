using System;
using System.Collections.Generic;
using Library.Cms.DataModel; 

namespace Library.Cms.BusinessLogicLayer {
    public partial interface IWebsiteSlideBusiness { 
		bool Create(WebsiteSlideModel model);
bool Update(WebsiteSlideModel model );
List<WebsiteSlideModel> Search(int pageIndex, int pageSize, char lang,out long total  , string slide_title);
WebsiteSlideModel GetById(Guid? id);
List<WebsiteSlideModel> Delete(string json_list_id, Guid updated_by);

    }
}