global using System.Collections.Generic;
global using System.Linq;

using WebAPIParking;

Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
    .Build().Run();