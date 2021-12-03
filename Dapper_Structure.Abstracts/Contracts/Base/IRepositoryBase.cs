using Dapper_Structure.Abstracts.Messages.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_Structure.Abstracts.Contracts.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : class //IRepositoryBase<T> //: IDisposable where TEntity : class
    {
        IDbConnection GetOpenConnection();
        Task<IResponseMessage<TEntity>> Add(TEntity obj);
        Task<IResponseMessage<TEntity>> GetById(int? id);
        Task<IResponseMessage<TEntity>> GetAll(QueryParameters request);
        Task<IResponseMessage<TEntity>> Update(TEntity obj);
        Task<IResponseMessage<TEntity>> Remove(TEntity obj);
        Task<IResponseMessage<TEntity>> Remove(int? id);
    }
}
