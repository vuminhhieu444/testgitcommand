using Library.Cms.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Cms.DataAccessLayer
{
    public partial interface IHomeRepository
    {
        HomemModel GetHome(char lang);         
    }
}
