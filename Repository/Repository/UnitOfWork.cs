﻿using Microsoft.EntityFrameworkCore;
using Repository.Data.Entity;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private PawFundDbContext _dbContext;
        private IEventRepository _event;
        private IUserRepository _user;
        private IShelterRepository _shelter;

        public UnitOfWork(PawFundDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        public IEventRepository Events
        {
            get
            {
                if (_event == null)
                {
                    _event = new EventRepository(_dbContext);
                }
                return _event;
            }
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}