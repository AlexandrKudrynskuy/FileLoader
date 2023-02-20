using Dll.Context;
using Domain.Model;
using FileLoader.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class LoaderFileService
    {
        private readonly LoaderFileRepository loaderFileRepository;
        public LoaderFileService(LoaderFileRepository _loaderFileRepository)
        {
            loaderFileRepository = _loaderFileRepository;
        }

        public void Create(LoaderFile loaderFile) => loaderFileRepository.Create(loaderFile);
        public void Delete(int id) => loaderFileRepository.Delete(id);
        public void Update(int id, LoaderFile loaderFile) => loaderFileRepository.Update(id, loaderFile);
        public LoaderFile GetValue(int id) => loaderFileRepository.GetValue(id);
        public List<LoaderFile> GetFromCondition(Expression<Func<LoaderFile, bool>> condition) => loaderFileRepository.GetFromCondition(condition).ToList();

    

    }
}
