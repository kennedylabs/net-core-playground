using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsWebsite.Infrastructure.DataAccess
{
    public interface IUnitOfWork
    {
        Task PersistChangesAsync();
    }
}
