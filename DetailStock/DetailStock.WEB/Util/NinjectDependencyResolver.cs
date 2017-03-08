using DetailStock.BLL.Interfaces;
using DetailStock.BLL.Services;
using Ninject;
using StockmanStock.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DetailStock.WEB.Util
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IDetailService>().To<DetailService>();
            kernel.Bind<IStockmanService>().To<StockmanService>();
        }
    }
}
