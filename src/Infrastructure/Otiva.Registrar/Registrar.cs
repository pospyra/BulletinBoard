using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Otiva.AppServeces.IRepository;
using Otiva.AppServeces.MapProfile;
using Otiva.AppServeces.Service.Ad;
using Otiva.AppServeces.Service.Category;
using Otiva.AppServeces.Service.Message;
using Otiva.AppServeces.Service.Review;
using Otiva.AppServeces.Service.SelectedAds;
using Otiva.AppServeces.Service.Subcategory;
using Otiva.AppServeces.Service.User;
using Otiva.DataAccess.DataBase;
using Otiva.DataAccess.Repository;
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

            services.AddAutoMapper(typeof(UserMapProfile), typeof(AdMapProfile),
                typeof(CategoryMapProfile), typeof(SubcategoryMapProfile), typeof(ReviewMapProfile), typeof(MessageMapProfile));

            services.AddTransient<IAdService, AdService>();
            services.AddTransient<IAdRepository, AdRepository>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<ISubcategoryService, SubcategoryService>();
            services.AddTransient<ISubcategoryRepository, SubcategoryRepository>();

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            services.AddTransient<ISelectedAdsService, SelectedAdsService>();
            services.AddTransient<ISelectedAdsRepository, SelectedAdsRepository>();


            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<IReviewRepository, ReviewRepository>();


            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IMessageRepository, MessageRepository>();

            return services;
        }
    }
}
