﻿using FoodApp.Api.FoodApp.Core.Enums;
using FoodApp.Core.Enums;

namespace FoodApp.Api.ViewModle.authVIewModel
{
    public class RoleFeatureViewModel
    {
        public Role role {  get; set; }

        public features  feature { get; set; }
     }


    public class RoleFeaturesModel
    {
        public Role role { get; set; }

        public List<features> features { get; set; } = new List<features>();
    }
}