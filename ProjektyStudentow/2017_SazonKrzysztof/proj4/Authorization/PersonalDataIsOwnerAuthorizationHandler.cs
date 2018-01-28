using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proj4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace proj4.Authorization {
    public class PersonalDataIsOwnerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Listener> {
        UserManager<ApplicationUser> _userManger;

        public PersonalDataIsOwnerAuthorizationHandler(UserManager<ApplicationUser> userManager) {
            _userManger = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Listener resource) {
            if (context.User == null || resource == null) {
                return Task.FromResult(0);
            }

            if (requirement.Name != Constants.CreateOperationName &&
               requirement.Name != Constants.UpdateOperationName &&
               requirement.Name != Constants.ReadOperationName &&
               requirement.Name != Constants.DeleteOperationName) {

                return Task.FromResult(0);
            }

            if (resource.OwnerID == _userManger.GetUserId(context.User)) {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}

