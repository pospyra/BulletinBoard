using AdBoard.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Otiva.AppServeces.IRepository;
using Otiva.AppServeces.IRepository.Photos;
using Otiva.AppServeces.MapProfile;
using Otiva.AppServeces.Service.Ad;
using Otiva.AppServeces.Service.Category;
using Otiva.AppServeces.Service.IdentityService;
using Otiva.AppServeces.Service.Message;
using Otiva.AppServeces.Service.Photo;
using Otiva.AppServeces.Service.Review;
using Otiva.AppServeces.Service.SelectedAds;
using Otiva.AppServeces.Service.StatisticsAds;
using Otiva.AppServeces.Service.Subcategory;
using Otiva.AppServeces.Service.User;
using Otiva.AppServeces.TimeCheck;
using Otiva.DataAccess.DataBase;
using Otiva.DataAccess.Repository;
using Otiva.Domain.User;
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
            services.AddLogging();
            services.AddCors();
            services.AddMemoryCache();

            services.AddSingleton<IDbContextOptionsConfigurator<OtivaContext>, OtivaContextConfiguration>();

            services.AddDbContext<OtivaContext>((Action<IServiceProvider, DbContextOptionsBuilder>)
                ((sp, dbOptions) => sp.GetRequiredService<IDbContextOptionsConfigurator<OtivaContext>>()
                .Configure((DbContextOptionsBuilder<OtivaContext>)dbOptions)));

            services.AddScoped((Func<IServiceProvider, DbContext>)(sp => sp.GetRequiredService<OtivaContext>()));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddAutoMapper(
                typeof(UserMapProfile), typeof(AdMapProfile),
                typeof(CategoryMapProfile), typeof(SubcategoryMapProfile),
                typeof(ReviewMapProfile), typeof(MessageMapProfile),
                typeof(PhotoMapProfile), typeof(StatisticsMapProfile));

            services.AddScoped<IIdentityUserService, IdentityUserService>();

            services.AddScoped<IStatisticsAdsRepository, StatisticsAdsRepository>();
            services.AddScoped<IStatisticsAdsService, StatisticsAdsService>();

            services.AddScoped<IAdService, AdService>();
            services.AddScoped<IAdRepository, AdRepository>();

            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IPhotoAdsRepository, PhotoAdsRepository>();
            services.AddScoped<IPhotoUsersRepository, PhotoUsersRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<ISubcategoryService, SubcategoryService>();
            services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<ISelectedAdsService, SelectedAdsService>();
            services.AddScoped<ISelectedAdsRepository, SelectedAdsRepository>();

            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IReviewRepository, ReviewRepository>();

            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IMessageRepository, MessageRepository>();

            services.AddScoped<TimerService>();

            services.AddIdentity<Domain.User.IdentityUser, IdentityRole>()
.AddEntityFrameworkStores<OtivaContext>()
.AddDefaultTokenProviders();

            services.AddScoped<IClaimAccessor, HttpContextClaimsAccessor>();;

            void ConfigureServices(IServiceCollection services)
            {
                var timerService = services.BuildServiceProvider().GetService<TimerService>();
                timerService.Start();
            }
            ConfigureServices(services);

            return services;
        }
    }
}
