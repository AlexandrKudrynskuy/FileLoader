using Bll;
using Dll.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;


namespace FileLoader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider provider { get;set;}

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureService(services);
            provider=services.BuildServiceProvider();
        }

        private void ConfigureService(ServiceCollection _services)
        {

            _services.AddDbContext<FileLoaderContext>(options => { options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FileLoader;Integrated Security=True;"); }) ;
            _services.AddTransient<LoaderFileRepository>();
            _services.AddTransient<LoaderFileService>();
            _services.AddTransient<MainWindow>();


        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var wind = provider.GetServices<MainWindow>();
        }
    }
}
