using Dapper;
using Dapper_Structure.Abstracts.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_Structure.Abstracts.Concrete
{
    public class DapperManager : IDapperManager
    {
        private readonly string connectionstring;

        public DapperManager(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }

        public DbConnection GetConnection()
        {
            return new SqlConnection(connectionstring);
        }
        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(connectionstring);
        }


        public T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                var res = db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
                return res;
            }
        }

        public List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                return db.Query<T>(sp, parms, commandType: commandType).ToList();
            }
        }
      
        public object Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                var res = db.Execute(sp, parms, commandType: commandType);
                return res;
            }
        }

        public T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result = default(T);

            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                try
                {
                    if (db.State == ConnectionState.Closed) { db.Open(); }
                    using (var trans = db.BeginTransaction())
                    {
                        try
                        {
                            result = db.Query<T>(sp, parms, commandType: commandType, transaction: trans).FirstOrDefault();
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (db.State == ConnectionState.Open)
                        db.Close();
                }

            }

            return result;

        }

        public T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result = default(T);

            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                try
                {
                    if (db.State == ConnectionState.Closed) { db.Open(); }
                    using (var trans = db.BeginTransaction())
                    {
                        try
                        {
                            result = db.Query<T>(sp, parms, commandType: commandType, transaction: trans).FirstOrDefault();
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (db.State == ConnectionState.Open)
                        db.Close();
                }

            }

            return result;
        }


        public void Dispose()
        {

        }
    }
}
