﻿using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace ASP_NET_Core_MVC.Infrastructure.Conventions
{
    public class CustomControllerConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            //controller.ControllerName
        }
    }
}
