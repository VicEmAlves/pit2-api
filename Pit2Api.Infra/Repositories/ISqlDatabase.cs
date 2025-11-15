using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pit2Api.Infra.Repositories
{
    public interface ISqlDatabase
    {
        public Task<IEnumerable<T>> QueryManyAsync<T>(string sql, object? parameters = null);
        public Task<int> ExecuteAsync(string sql, object? parameters = null);
        public Task<T> QueryOneAsync<T>(string sql, object? parameters = null);
    }
}
