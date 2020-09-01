using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinner.ViewModels
{
    public class ChangeAvatarViewModel
    {
        public string UserId { get; set; }
        public string ExistingAvatarPhotoPath { get; set; }
        public IFormFile Photo { get; set; }
    }
}
