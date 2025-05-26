using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestProject
{
    public class DBFixure :IDisposable
    {
        public GadjetsStoreDBContext Context { get; private set; }
        public DBFixure()
        {
            var options = new DbContextOptionsBuilder<GadjetsStoreDBContext>()
                .UseSqlServer("Server = SRV2\\PUPILS; Database = GadjetsStoreDB_Test; Trusted_Connection = True; TrustServerCertificate = True")
                .Options;
            Context = new GadjetsStoreDBContext(options);
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}

