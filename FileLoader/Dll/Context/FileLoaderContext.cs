using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Dll.Context
{
    public class FileLoaderContext:DbContext
    {
        public FileLoaderContext(DbContextOptions<FileLoaderContext> dbContextOptions): base(dbContextOptions) 
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<LoaderFile>().HasData(new LoaderFile { Id = 1, Status = false, Reference = "https://st.depositphotos.com/1719930/2223/i/600/depositphotos_22230607-stock-photo-isolated-check-pictogram-inside-a.jpg"});

            //modelBuilder.Entity<LoaderFile>().HasData(new LoaderFile { Id = 2, Status = false, Reference = "https://png.pngtree.com/png-vector/20191028/ourmid/pngtree-address-icon-isolated-on-abstract-background-png-image_1901927.jpg" });
            //modelBuilder.Entity<LoaderFile>().HasData(new LoaderFile { Id = 3, Status = false, Reference = "https://auc.org.ua/sites/default/files/field/image/55b78abfae09aa223c26035b5694b452.png" });
            //modelBuilder.Entity<LoaderFile>().HasData(new LoaderFile { Id = 4, Status = false, Reference = "https://www.study.ru/uploads/server/kGOGi9dqxLDCIRFU.jpg" });

            //modelBuilder.Entity<LoaderFile>().HasData(new LoaderFile { Id=5, Status=false, Reference= "https://ftp.jaist.ac.jp/pub/pkgsrc/distfiles/7kaa-2.15.4p1.tar.xz" });
            //modelBuilder.Entity<LoaderFile>().HasData(new LoaderFile { Id = 6, Status = false, Reference = "https://ftp.jaist.ac.jp/pub/pkgsrc/distfiles/sauerbraten_2020_12_27_linux.tar.bz2" });
            //modelBuilder.Entity<LoaderFile>().HasData(new LoaderFile { Id = 7, Status = false, Reference = "https://ftp.jaist.ac.jp/pub/pkgsrc/distfiles/firefox-91.6.0esr.source.tar.xz" });
            //modelBuilder.Entity<LoaderFile>().HasData(new LoaderFile { Id = 8, Status = false, Reference = "https://ftp.jaist.ac.jp/pub/pkgsrc/distfiles/qtwebengine-everywhere-opensource-src-5.15.6.tar.xz" });

            modelBuilder.Entity<LoaderFile>().Property(x => x.PathToFile).IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<LoaderFile> FileLoaders { get; set; }
    }
}
