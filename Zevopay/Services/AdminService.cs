using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Numerics;
using Zevopay.Contracts;
using Zevopay.Data;
using Zevopay.Data.Entity;
using Zevopay.Models;

namespace Zevopay.Services
{
    public class AdminService : IAdminService
    {
        #region GlobalVariable
        private readonly IDapperDbContext _dapperDbContext;
        private readonly string _connectionString;
        private readonly string AdminFundCreditDebit_SP = "AdminFundCreditDebit";
        private readonly string Surcharge_SP = "SP_Surcharge";
        #endregion GlobalVariable End

        #region Constructor
        public AdminService(IDapperDbContext dapperDbContext, IConfiguration configuration)
        {
            _dapperDbContext = dapperDbContext;
            _connectionString = configuration.GetConnectionString("ZevopayDb") ?? throw new ArgumentNullException("Connection string");
        }
        #endregion Constructor End

        #region FundManage
        public async Task<ResponseModel> FundManageAsync(FundManageModel model)
        {
            return await SqlMapper.QueryFirstAsync<ResponseModel>(new SqlConnection(_connectionString), AdminFundCreditDebit_SP,
                new
                {
                    Action = 1,
                    ReceiverMemberID = model.MemberId,
                    Factor = model.Factor,
                    Description = model.Description,
                    Amount = model.Amount
                }, commandType: CommandType.StoredProcedure);

        }
        #endregion FundManage End

        #region GetWalletTransactions
        public async Task<IEnumerable<WalletTransactions>> GetWalletTransactionsAsync()
        {
            return await SqlMapper.QueryAsync<WalletTransactions>(new SqlConnection(_connectionString), "SP_CreditDebitTransaction",
            new
            {
                Action = 2,
            }, commandType: CommandType.StoredProcedure);


        }
        #endregion GetWalletTransactions End



        public async Task<IEnumerable<WalletTransactions>> GetCeditDebitTransactions()
        {
         var data = await SqlMapper.QueryAsync<WalletTransactions>(new SqlConnection(_connectionString), "SP_CreditDebitTransaction",
         new
         {
             Action = 1,
         }, commandType: CommandType.StoredProcedure);
            return data;
        }


        #region SurchargeList
        public async Task<IEnumerable<Surcharge>> GetSurchagesAsync()
        {
            return await SqlMapper.QueryAsync<Surcharge>(new SqlConnection(_connectionString), Surcharge_SP,
            new
            {
                Action = 1,
            }, commandType: CommandType.StoredProcedure);
        }

        public async Task<ResponseModel> AddSurchargeAsync(Surcharge model)
        {
            return await SqlMapper.QueryFirstAsync<ResponseModel>(new SqlConnection(_connectionString), Surcharge_SP,
               new
               {
                   Action = 2,
                   TransactionType = model.TransactionType,
                   RangeFrom = model.RangeFrom,
                   RangeTo = model.RangeTo,
                   IsFlat = model.IsFlat,
                   SurchargeAmount = model.SurchargeAmount
               }, commandType: CommandType.StoredProcedure);
        }
        #endregion SurchargeList End



        public async Task<WalletTransactions> GetBalanceByUser(string zevoId)
        {
            var query = $"select Balance  from WalletBalance where Id={zevoId}";
            var data = await _dapperDbContext.QueryFirstOrDefaultAsync<WalletTransactions>(query);
            return data;
        }
    }
}
