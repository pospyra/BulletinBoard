﻿using AdBoard.Infrastructure.Identity;
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
using Otiva.AppServeces.Service.Subcategory;
using Otiva.AppServeces.Service.User;
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

            services.AddAutoMapper(typeof(UserMapProfile), typeof(AdMapProfile),
                typeof(CategoryMapProfile), typeof(SubcategoryMapProfile), typeof(ReviewMapProfile), typeof(MessageMapProfile));

            services.AddScoped<IIdentityUserService, IdentityUserService>();

            services.AddTransient<IAdService, AdService>();
            services.AddTransient<IAdRepository, AdRepository>();

            services.AddTransient<IPhotoService, PhotoService>();
            services.AddTransient<IPhotoAdsRepository, PhotoAdsRepository>();
            services.AddTransient<IPhotoUsersRepository, PhotoUsersRepository>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<ISubcategoryService, SubcategoryService>();
            services.AddTransient<ISubcategoryRepository, SubcategoryRepository>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            services.AddTransient<ISelectedAdsService, SelectedAdsService>();
            services.AddTransient<ISelectedAdsRepository, SelectedAdsRepository>();

            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<IReviewRepository, ReviewRepository>();

            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IMessageRepository, MessageRepository>();

            services.AddScoped<IClaimAccessor, HttpContextClaimsAccessor>();;
     
            services.AddIdentity<Domain.User.IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<OtivaContext>()
    .AddDefaultTokenProviders();

            return services;
        }
    }
}
