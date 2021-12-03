using Dapper_Structure.Abstracts.Messages.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_Structure.Abstracts.Contracts.Base
{
    public abstract class RepositoryBase<TEntity> where TEntity : class
    {
        private readonly string connectionString;

        public RepositoryBase(string ConnectionString)
        {
            connectionString = ConnectionString;
        }
        public abstract Task<IResponseMessage<TEntity>> Add(TEntity obj);
        public abstract Task<IResponseMessage<TEntity>> GetById(int? id);
        public abstract Task<IResponseMessage<TEntity>> GetAll(QueryParameters request);
        public abstract Task<IResponseMessage<TEntity>> Update(TEntity obj);
        public abstract Task<IResponseMessage<TEntity>> Remove(TEntity obj);
        public abstract Task<IResponseMessage<TEntity>> Remove(int? id);

        //public abstract void Dispose();
    }
}
