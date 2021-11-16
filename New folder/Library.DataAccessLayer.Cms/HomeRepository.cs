using Library.Cms.DataModel;
using Library.Common.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Library.Cms.DataAccessLayer
{
    public partial class HomeRepository : IHomeRepository
    {
        private IDatabaseHelper _dbHelper;
        public HomeRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
         
        public HomemModel GetHome(char lang)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToMultipleList("dbo.website_get_hmoe", parameters);
                if (!string.IsNullOrEmpty(result.ErrorMessage) && result.ErrorCode != 0)
                    throw new Exception(result.ErrorMessage);
                var resultData = new HomemModel();
                resultData.listjson_item = result.MapToList<ItemModel>(0);
                resultData.listjson_item_group = result.MapToList<ItemGroupModel>(1);
                return resultData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
