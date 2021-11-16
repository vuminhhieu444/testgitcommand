using Library.Cms.DataAccessLayer;
using Library.Cms.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Cms.BusinessLogicLayer
{
    public partial class HomeBusiness : IHomeBusiness
    {
        private IHomeRepository _res;
        public HomeBusiness(IHomeRepository homeRes)
        {
            _res = homeRes;
        }
        public HomemModel GetHome(char lang)
        {
            return _res.GetHome(lang);
        }
        
    }
}
