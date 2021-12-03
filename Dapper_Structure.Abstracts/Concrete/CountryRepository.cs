using Dapper;
using Dapper_Structure.Abstracts.Contracts;
using Dapper_Structure.Abstracts.Contracts.Base;
using Dapper_Structure.Abstracts.Messages;
using Dapper_Structure.Abstracts.Messages.Base;
using Dapper_Structure.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_Structure.Abstracts.Concrete
{
    public class CountryRepository : RepositoryBase<CountryVM>, ICountryRepository

    {
        private readonly string connectionString;
        private readonly IDapperManager dapperManager;

        public CountryRepository(string ConnectionString, IDapperManager dapperManager) : base(ConnectionString)
        {
            connectionString = ConnectionString;
            this.dapperManager = dapperManager;
        }
        public override async Task<IResponseMessage<CountryVM>> Add(CountryVM obj)
        {
            {
                try
                {
                    // 1- Add Guest to chat table with connectionid and begin date
                    string SQL = "SP_Countrys_Add";

                    var dbParams = new DynamicParameters();

                    //dbParams.Add("VendorId", obj.VendorId, DbType.Int64);
                    //dbParams.Add("Email", obj.Email, DbType.String);
                    int x;
                    var NewChatID = await Task.FromResult(dapperManager.Execute(SQL, dbParams, CommandType.StoredProcedure));
                    if (NewChatID == null || !int.TryParse(NewChatID.ToString(), out x) || (int)NewChatID <= 0) { return new ResponseMessageBase<CountryVM>() { MessageType = MessageTypes.Error, ResponseData = null }; }

                    //obj.id = (int)NewChatID;
                    return new ResponseMessageBase<CountryVM>() { MessageType = MessageTypes.Success, ResponseData = new[] { obj }, exception = null };

                }
                catch (Exception ex)
                {
                    return new ResponseMessageBase<CountryVM>() { MessageType = MessageTypes.Error, ResponseData = null, exception = ex };
                }
            }
        }


        public async Task<IResponseMessage<CountryVM>> Search(CountryRequest request)
        {
            string sql = "SP_Countrys_SelectByCode";

            var dbPara = new DynamicParameters();
            //dbPara.Add("From_Date", request.FromDate_Date, DbType.DateTime);
            //dbPara.Add("To_Date", request.ToDate_Date, DbType.DateTime);
            //dbPara.Add("@PageNumber", request.PageNumber, DbType.Int32);
            //dbPara.Add("@PageSize", request.PageSize, DbType.Int32);
            //dbPara.Add("@Emp_Code", request.Emp_Code, DbType.Int32);

            var settings = await Task.FromResult(dapperManager.GetAll<CountryVM>(sql, dbPara, commandType: CommandType.StoredProcedure));

            return new ResponseMessageBase<CountryVM>() { MessageType = MessageTypes.Success, ResponseData = settings };

        }
        public async override Task<IResponseMessage<CountryVM>> GetAll(QueryParameters request)
        {
            string sql = "SP_Countrys_SelectAll";

            var dbPara = new DynamicParameters();

            var settings = await Task.FromResult(dapperManager.GetAll<CountryVM>(sql, dbPara, commandType: CommandType.StoredProcedure));

            return new ResponseMessageBase<CountryVM>() { MessageType = MessageTypes.Success, ResponseData = settings };

        }

        public async override Task<IResponseMessage<CountryVM>> GetById(int? id)
        {
            var dbParams = new DynamicParameters();
            dbParams.Add("@id", id, DbType.Int32);

            string sql = "SP_Countrys_SelectById";
            var Country = await Task.FromResult(dapperManager.Get<CountryVM>(sql, dbParams, CommandType.StoredProcedure));
            return new ResponseMessageBase<CountryVM>() { MessageType = MessageTypes.Success, ResponseData = new[] { Country } };
        }


        public IDbConnection GetOpenConnection()
        {
            throw new NotImplementedException();
        }

        public async override Task<IResponseMessage<CountryVM>> Remove(CountryVM model)
        {

            if (model.CountryId == 0) { return new ResponseMessageBase<CountryVM>() { MessageType = MessageTypes.NotFound, ResponseData = null }; }
            var dbParams = new DynamicParameters();
            dbParams.Add("@Id", model.CountryId, DbType.Int32);
            string sql = "SP_Countrys_delete";
            var Country = await Task.FromResult(dapperManager.Execute(sql, dbParams, CommandType.StoredProcedure));
            return new ResponseMessageBase<CountryVM>() { MessageType = MessageTypes.Success, ResponseData = null };
        }

        public async override Task<IResponseMessage<CountryVM>> Remove(int? id)
        {

            if (id == null || id <= 0) { return new ResponseMessageBase<CountryVM>() { MessageType = MessageTypes.NotFound, ResponseData = null }; }
            var dbParams = new DynamicParameters();
            dbParams.Add("@ProductId", id, DbType.Int32);
            string sql = "SP_Countrys_delete";
            var Country = await Task.FromResult(dapperManager.Execute(sql, dbParams, CommandType.StoredProcedure));
            return new ResponseMessageBase<CountryVM>() { MessageType = MessageTypes.Success, ResponseData = null };
        }
        public async override Task<IResponseMessage<CountryVM>> Update(CountryVM obj)
        {
            string SQL = "SP_Countrys_Update";
            var dbPara = new DynamicParameters();
            //dbPara.Add("Email", obj.Email, DbType.String);
 
            try
            {
                var newID = await Task.FromResult(dapperManager.Update<int>(SQL, dbPara, CommandType.StoredProcedure));

                if (newID > 0)
                {
                    return new ResponseMessageBase<CountryVM>() { MessageType = MessageTypes.Success, ResponseData = new[] { obj } };
                }

                return new ResponseMessageBase<CountryVM>() { MessageType = MessageTypes.Error, ResponseData = null };
            }
            catch (Exception ex)
            { return new ResponseMessageBase<CountryVM>() { MessageType = MessageTypes.Error, ResponseData = null, exception = ex }; }
        }
    }
}
