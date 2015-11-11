using ODataToSPHackathon.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODataToSPHackathon.Repositories
{
    public class PartnerDbContext : DbContext
    {
        static PartnerDbContext()
        {
            System.Data.Entity.Database.SetInitializer<PartnerDbContext>(null);
        }

        public PartnerDbContext()
            : base("Name=PartnerDbContext")
        {
        }

        public PartnerDbContext(string connectionString) : base(connectionString)
        {
        }

        public PartnerDbContext(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model) : base(connectionString, model)
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }


        public List<EnterprisesModel> ProcEnterprisesRetrieveAll(bool? dbIsBuilt)
        {
            int procResult;
            return ProcEnterprisesRetrieveAll(dbIsBuilt, out procResult);
        }

        public List<EnterprisesModel> ProcEnterprisesRetrieveAll(bool? dbIsBuilt, out int procResult)
        {
            var dbIsBuiltParam = new SqlParameter { ParameterName = "@DBIsBuilt", SqlDbType = SqlDbType.Bit, Direction = ParameterDirection.Input, Value = dbIsBuilt.GetValueOrDefault() };
            if (!dbIsBuilt.HasValue)
                dbIsBuiltParam.Value = DBNull.Value;

            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

            var procResultData = Database.SqlQuery<EnterprisesModel>("EXEC @procResult = [dbo].[proc_Enterprises_RetrieveAll] @DBIsBuilt", dbIsBuiltParam, procResultParam).ToList();

            procResult = (int)procResultParam.Value;
            return procResultData;
        }

        public List<EnterprisesModel> ProcPgdEnterprisesRetrieveAllMonitoredFromUserPartner(int? accountId, string whereClause, string sort, string whereRows, bool? count)
        {
            var accountIdParam = new SqlParameter { ParameterName = "@AccountID", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = accountId.GetValueOrDefault() };
            if (!accountId.HasValue)
                accountIdParam.Value = DBNull.Value;

            var whereClauseParam = new SqlParameter { ParameterName = "@WhereClause", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = whereClause, Size = 1000 };
            if (whereClauseParam.Value == null)
                whereClauseParam.Value = DBNull.Value;

            var sortParam = new SqlParameter { ParameterName = "@Sort", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = sort, Size = 255 };
            if (sortParam.Value == null)
                sortParam.Value = DBNull.Value;

            var whereRowsParam = new SqlParameter { ParameterName = "@WhereRows", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = whereRows, Size = 100 };
            if (whereRowsParam.Value == null)
                whereRowsParam.Value = DBNull.Value;

            var countParam = new SqlParameter { ParameterName = "@Count", SqlDbType = SqlDbType.Bit, Direction = ParameterDirection.Input, Value = count.GetValueOrDefault() };
            if (!count.HasValue)
                countParam.Value = DBNull.Value;

            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

            var result = Database.SqlQuery<EnterprisesModel>("EXEC @procResult = [dbo].[proc_Pgd_Enterprises_RetrieveAllMonitoredFromUserPartner] @AccountID, @WhereClause, @Sort, @WhereRows, @Count", accountIdParam, whereClauseParam, sortParam, whereRowsParam, countParam, procResultParam).ToList();

            return result;
        }

        public System.Data.Entity.DbSet<ODataToSPHackathon.Models.EnterprisesModel> ProcEnterprisesRetrieveAllReturnModels { get; set; }
    }
}
