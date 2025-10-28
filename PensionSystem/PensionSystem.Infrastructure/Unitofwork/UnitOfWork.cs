using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PensionSystem.Domain.interfaces;
using PensionSystem.Infrastructure.Data;
using PensionSystem.Infrastructure.Repositories;
using System;
using System.Collections;

namespace PensionSystem.Infrastructure.Unitofwork;

public class UnitOfWork : IUnitofWork
{
    public readonly PensionDbContext _dbContext;
    private Hashtable _repositories;
    public DatabaseFacade Database => _dbContext.Database;
    public IMemberRepository memberRepo { get; private set; }
    public IContributionRepository contributionRepo { get; private set; }
    public IEmployerRepository employerRepo { get; private set; }
    public IBenefitRepository benefitRepo { get; private set; }
    public ITransactionHistoryRepository transactionRepo { get; private set; }

    public UnitOfWork(PensionDbContext dbContext)
    {
        _dbContext = dbContext;
        memberRepo = new MemberRepository(dbContext);
        contributionRepo = new ContributionRepository(dbContext);
        employerRepo = new EmployerRepository(dbContext);
        benefitRepo = new BenefitRepository(dbContext);
        transactionRepo = new TransactionHistoryRepository(dbContext);
    }

    public IGenericRepository<TEntity> repository<TEntity>() where TEntity : class
    {
        if (_repositories == null) _repositories = new Hashtable();
        var Type = typeof(TEntity).Name;
        if (!_repositories.ContainsKey(Type))
        {
            var repositoryType = typeof(GenericRepository<TEntity>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dbContext);
            _repositories.Add(Type, repositoryInstance);
        }
        return (IGenericRepository<TEntity>)_repositories[Type];
    }

    public async Task<int> CompleteAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

}
