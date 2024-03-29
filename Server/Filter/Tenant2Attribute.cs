﻿using ExampleApp.Context.Context;
using ExampleApp.Server.Helpers;
using ExampleApp.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Security.Claims;

namespace ExampleApp.Server.Filter
{
    public class Tenant2Attribute : ActionFilterAttribute
    {

        private readonly EmployeeContext _employeeContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public Tenant2Attribute(EmployeeContext employeeContext, UserManager<ApplicationUser> userManager)
        {
            _employeeContext = employeeContext;
            _userManager = userManager;
        }

        public override void OnActionExecuting(ActionExecutingContext actionExecutingContext)
        {
            var User = actionExecutingContext.HttpContext.User;
            var userId = User.GetLoggedInUserId<string>(); 
            var userName = User.GetLoggedInUserName();
            var userEmail = User.GetLoggedInUserEmail();

            var fullAddress = actionExecutingContext.HttpContext?.Request?.Headers?["Host"].ToString()?.Split('.');
            if (fullAddress.Length < 2)
            {
                actionExecutingContext.Result = new StatusCodeResult(404); base.OnActionExecuting(actionExecutingContext);
            }
            else
            {
                var subdomain = fullAddress[0];
                var dbcontext = _employeeContext.Create();
                var tenant = dbcontext.Tenants.FirstOrDefault(t => t.Host.ToLower() == subdomain.ToLower());
                if (tenant != null)
                {
                    actionExecutingContext.RouteData.Values.Add("tenant", tenant);
                    base.OnActionExecuting(actionExecutingContext);
                }
                else
                {
                    actionExecutingContext.Result = new StatusCodeResult(404); base.OnActionExecuting(actionExecutingContext);
                }
            }
        }
    }
}
