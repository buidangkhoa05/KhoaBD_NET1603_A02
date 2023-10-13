

using Application.Repository;
using PRN221.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangeAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RolebackTransactionAsync();

        #region Repository

        public IGenericRepository<Customer> Customer { get; }
        public IGenericRepository<Manufacturer> Manufacturer { get; }
        public IGenericRepository<Supplier> Supplier { get; }
        public IGenericRepository<CarInformation> CarInformation { get; }
        public IGenericRepository<RentingTransaction> RentingTrans { get; }
        #endregion Repository
    }
}
