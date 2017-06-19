using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using FirstAspNetCore_Model;
using static Dapper.SqlMapper;

namespace FirstAspNetCore_Dao
{
    public class BaseRepository
    {
        public IDbConnection Connection
        {
            get { return Transaction.Connection; }
        }

        public string StoredProcedureName { get; private set; }

        public IDbTransaction Transaction { get; private set; }

        public BaseRepository(IDbTransaction transaction, string storedProcedureName)
        {
            Transaction = transaction;
            StoredProcedureName = storedProcedureName;
        }

        public IEnumerable<T> Query<T>(dynamic param = null, dynamic outParam = null)
        {
            CombineParameters(ref param, outParam);

            try
            {
                return Connection.Query<T>(StoredProcedureName, param: (object)param, transaction: Transaction, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                //LoggerHelper.Error(ex);
                return default(IEnumerable<T>);
            }
        }
        
        public void QueryMultiple(Action<GridReader> action, dynamic param = null, dynamic outParam = null)
        {
            CombineParameters(ref param, outParam);

            try
            {
                var rows = SqlMapper.QueryMultiple(Connection, StoredProcedureName, param, Transaction, commandType: CommandType.StoredProcedure);
                action.Invoke(rows);
            }
            catch
            {
                return;
            }
        }

        public int Execute(dynamic param = null, dynamic outParam = null)
        {
            int result = -1;
            CombineParameters(ref param, outParam);

            try
            {
                result = Connection.Execute(StoredProcedureName, param: (object)param, transaction: Transaction, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                return BaseConstant.EXCEPTION_NUMBER;
            }

            return result;
        }


        public void CombineParameters(ref dynamic param, dynamic outParam = null)
        {
            if (outParam != null)
            {
                if (param != null)
                {
                    param = new DynamicParameters(param);
                    ((DynamicParameters)param).AddDynamicParams(outParam);
                }
                else param = outParam;
            }
        }

        public DynamicParameters CreateInputParameters<H>(H header, string activity = null)
        {
            RequestHeaderModel reqHeader = header as RequestHeaderModel;
            var param = new DynamicParameters();
            param.Add(BaseConstant.ACTIVITY, activity, DbType.String, ParameterDirection.Input, 50);
            param.Add(BaseConstant.USER_LOGIN_ID, reqHeader.UserLoginId, DbType.Int32, ParameterDirection.Input);
            param.Add(BaseConstant.USER_TYPE_ID, reqHeader.UserTypeLoginId, DbType.Int32, ParameterDirection.Input);
            param.Add(BaseConstant.LANGUAGE_ID, reqHeader.Language, DbType.String, ParameterDirection.Input, 5);

            return param;
        }

        public DynamicParameters CreateOutputParameters()
        {
            var param = new DynamicParameters();
            param.Add(BaseConstant.RETURN_MESS, string.Empty, DbType.String, ParameterDirection.InputOutput, 255);
            param.Add(BaseConstant.RETURN_VALUE, string.Empty, DbType.String, ParameterDirection.InputOutput, 255);

            return param;
        }

        public void AddPagingParameters(ref DynamicParameters param, int offset, int limit)
        {
            if (param != null)
            {
                var pagingParam = new DynamicParameters();
                pagingParam.Add(BaseConstant.OFFSET, offset);
                pagingParam.Add(BaseConstant.LIMIT, limit);

                param = new DynamicParameters(param);
                param.AddDynamicParams(pagingParam);
            }
        }

        public void AddOrderByParameters(ref DynamicParameters param, string orderBy, SortingType orderType)
        {
            if (param != null)
            {
                var orderParam = new DynamicParameters();
                orderParam.Add(BaseConstant.ORDER_BY, orderBy);
                orderParam.Add(BaseConstant.ORDER_TYPE, orderType);

                param = new DynamicParameters(param);
                ((DynamicParameters)param).AddDynamicParams(orderParam);
            }
        }
    }
}
