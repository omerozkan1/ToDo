﻿using Microsoft.Extensions.DependencyInjection;
using OmerOzkan.ToDo.Business.Concrete;
using OmerOzkan.ToDo.Business.CustomLogger;
using OmerOzkan.ToDo.Business.Interfaces;
using OmerOzkan.ToDo.DataAccess.Interfaces;
using OmerOzkan.ToDo.DataAccess.Repositories;

namespace OmerOzkan.ToDo.Business.Containers.MicrosoftIoC
{
    public static class CustomIoCExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            // Business
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IDutyService, DutyService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IUrgencyService, UrgencyService>();

            services.AddTransient<ICustomLogger, NLogLogger>();

            // DataAccess
            services.AddScoped(typeof(IGenericDal<>), typeof(GenericDal<>));

            services.AddScoped<IAppUserDal, AppUserDal>();
            services.AddScoped<IDutyDal, DutyDal>();
            services.AddScoped<INotificationDal, NotificationDal>();
            services.AddScoped<IReportDal, ReportDal>();           
            services.AddScoped<IUrgencyDal, UrgencyDal>();

        }
    }
}
