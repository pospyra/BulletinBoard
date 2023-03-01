using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Otiva.AppServeces.MapProfile;
using Otiva.DataAccess.DataBase;
using Otiva.Infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Registrar
{
    public static class Registrar
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IDbContextOptionsConfigurator<OtivaContext>, OtivaContextConfiguration>();

            services.AddDbContext<OtivaContext>((Action<IServiceProvider, DbContextOptionsBuilder>)
                ((sp, dbOptions) => sp.GetRequiredService<IDbContextOptionsConfigurator<OtivaContext>>()
                .Configure((DbContextOptionsBuilder<OtivaContext>)dbOptions)));

            services.AddScoped((Func<IServiceProvider, DbContext>)(sp => sp.GetRequiredService<OtivaContext>()));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));


            //services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));


            services.AddAutoMapper(typeof(UserMapProfile), typeof(AdMapProfile),
                typeof(CategoryMapProfile), typeof(SubcategoryMapProfile));

            return services;
        }
    }
}
