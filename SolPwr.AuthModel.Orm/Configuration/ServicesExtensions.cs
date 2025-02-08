using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnionDlx.SolPwr.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.Configuration
{
    public static class ServicesAuthExtensions
    {
        /// <summary>
        /// Will add the needed boilerplate to IoC, without the need to introduce a dependency to EF in the main app
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuthPersistence(this IServiceCollection coll, string connString)
        {
            coll.AddDbContext<AuthIdentityContext>(options => options.UseSqlServer(connString));
            return coll;
        }
    }
}
