﻿using AutoMapper;
using Domain.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services;
public class ServiceManager : IServiceManager
{
    // Keynote: add lazy for all active repos and services
    private readonly Lazy<IAuthService> authService;
    private readonly Lazy<ICourseService> courseService;
    private readonly Lazy<IModuleService> moduleService;
    private readonly Lazy<IActivityService> activityService;

    public IAuthService AuthService => authService.Value;
    public ICourseService CourseService => courseService.Value;
    public IModuleService ModuleService => moduleService.Value;
    public IActivityService ActivityService => activityService.Value;

    public ServiceManager(Lazy<IAuthService> authService, IUnitOfWork uow, IMapper mapper)
    {
        this.authService = authService;
        courseService = new Lazy<ICourseService>(() => new CourseService(uow, mapper));
        moduleService = new Lazy<IModuleService>(() => new ModuleService(uow, mapper));
        activityService = new Lazy<IActivityService>(() => new ActivityService(uow, mapper));
    }
}
