﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSpace.Web.Areas.Admin.Models.ApplicationUserViewModels
{
    public class ApplicationUserViewModel
    {

        public string Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string ProfilePictureUrl { get; set; }

        public bool isAllowed { get; set; }

        public bool isAdmin { get; set; }

    }
}
