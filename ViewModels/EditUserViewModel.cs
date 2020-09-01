using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dinner.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            
        }
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public List<string> Claims { get; set; }

        public IList<string> Roles { get; set; }


    }
}
