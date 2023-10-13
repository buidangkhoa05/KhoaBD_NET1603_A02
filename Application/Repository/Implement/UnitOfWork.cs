
using Application.Repository;
using Domain.Models;
using PRN221.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private FucarRentingManagementContext _dbContext;
        private IDbContextTransaction _transaction;


        public UnitOfWork()
        {
            if (_dbContext == null)
            {
                _dbContext = new FucarRentingManagementContext();
            }
        }

        #region Repository 
        public IGenericRepository<Customer> Customer
        {
            get
            {
                return new GenericRepository<Customer>(_dbContext);
            }
        }

        public IGenericRepository<CarInformation> CarInformation
        {
            get
            {
                return new GenericRepository<CarInformation>(_dbContext);
            }
        }

        public IGenericRepository<Manufacturer> Manufacturer
        {
            get
            {
                return new GenericRepository<Manufacturer>(_dbContext);
            }
        }

        public IGenericRepository<Supplier> Supplier
        {
            get
            {
                return new GenericRepository<Supplier>(_dbContext);
            }
        }

        public IGenericRepository<RentingTransaction> RentingTrans
        {
            get
            {
                return new GenericRepository<RentingTransaction>(_dbContext);
            }
        }

        public IGenericRepository<RentingDetail> RentingDetail
        {
            get
            {
                return new GenericRepository<RentingDetail>(_dbContext);
            }
        }
       
        #endregion Repository


        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
            {
                _transaction = await _dbContext.Database.BeginTransactionAsync();
            }
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction is null)
                return;

            try
            {
                await _dbContext.SaveChangesAsync();
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
            catch
            {
                await RolebackTransactionAsync();
            }
        }

        public async Task RolebackTransactionAsync()
        {
            if (_transaction is null)
                return;

            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
