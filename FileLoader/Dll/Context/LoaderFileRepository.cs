using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dll.Context
{
    public class LoaderFileRepository : IRepository<LoaderFile>
    {
        private readonly FileLoaderContext context;

        public LoaderFileRepository(FileLoaderContext _context)
        {
            context = _context;
        }

        public void Create(LoaderFile obj)
        {
            context.FileLoaders.Add(obj);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = context.FileLoaders.First(x => x.Id == id);
            context.FileLoaders.Remove(obj);
            context.SaveChanges();
        }

        public IEnumerable<LoaderFile> GetFromCondition(Expression<Func<LoaderFile, bool>> condition) => context.FileLoaders.Where(condition).ToList();
        public LoaderFile GetValue(int id) => context.FileLoaders.First(x => x.Id == id);
        

        public async void Update(int id, LoaderFile obj)
        {
            var oldObj = await context.FileLoaders.FirstAsync(x=>x.Id>0);
            oldObj.Reference = obj.Reference;
            oldObj.Status = obj.Status;
            oldObj.PathToFile = obj.PathToFile;
            context.Entry(oldObj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChangesAsync();
            
        }
    }
}
