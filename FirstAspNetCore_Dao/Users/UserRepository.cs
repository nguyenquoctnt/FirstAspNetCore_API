using FirstAspNetCore_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FirstAspNetCore_Dao
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IDbTransaction transaction, string storedProcedureName) : base(transaction, "sp_User")
        {
        }

        public IEnumerable<UserModel> Find(RequestHeaderModel header, int pageIndex, int limit, string orderBy, SortingType sortType, out int totalRow)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> Find(RequestHeaderModel header, UserModel condition, int pageIndex, int limit, string orderBy, SortingType sortType, out int totalRow)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> Find<T1>(RequestHeaderModel header, T1 condition, int pageIndex, int limit, string orderBy, SortingType sortType, out int totalRow)
        {
            throw new NotImplementedException();
        }

        public UserModel Find(RequestHeaderModel header, int id)
        {
            throw new NotImplementedException();
        }

        public UserModel Find(RequestHeaderModel header, UserModel condition)
        {
            throw new NotImplementedException();
        }

        public UserModel Find<T1>(RequestHeaderModel header, T1 condition)
        {
            throw new NotImplementedException();
        }

        public string Add(RequestHeaderModel header, UserModel item)
        {
            var param = CreateInputParameters(header, BaseConstant.INSERT);
            param.Add("UserId", item.UserId);
            param.Add("Email", item.Email);
            param.Add("Password", item.Password);
            param.Add("FullName", item.FullName);
            

            var outParam = CreateOutputParameters();
            int effectedRow = Execute(param, outParam);
            if (effectedRow != BaseConstant.EXCEPTION_NUMBER)
            {
                string message = outParam.Get<string>(BaseConstant.RETURN_MESS);
                if (!string.IsNullOrEmpty(message))
                    return message;

                int.TryParse(outParam.Get<string>(BaseConstant.RETURN_VALUE), out int userId);
                item.UserId = userId;

                return message;
            }

            return BaseConstant.INVALID_REQUEST;
        }

        public string Update(RequestHeaderModel header, UserModel item)
        {
            throw new NotImplementedException();
        }

        public string Remove(RequestHeaderModel header, int id)
        {
            throw new NotImplementedException();
        }

        public string Remove(RequestHeaderModel header, string idList)
        {
            throw new NotImplementedException();
        }

        public string Remove(RequestHeaderModel header, UserModel item)
        {
            throw new NotImplementedException();
        }
    }
}
