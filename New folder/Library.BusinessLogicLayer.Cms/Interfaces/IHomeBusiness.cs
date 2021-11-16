using Library.Cms.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Cms.BusinessLogicLayer
{
    public partial interface IHomeBusiness
    {
        HomemModel GetHome(char lang);
    }
}
