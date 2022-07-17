using MarvUpload.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvUpload.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MarvContext _marvContext;

        public UnitOfWork(MarvContext marvContext)
        {
            _marvContext = marvContext;
        }


        private IRepository<Person> personRepository;
        public IRepository<Person> PersonRepository => personRepository ?? new Repository<Person>(_marvContext);

        public async Task<bool> Complete() => await _marvContext.SaveChangesAsync() > 0;

        public void Dispose()
        {
            _marvContext.Dispose();
        }

    }
}
