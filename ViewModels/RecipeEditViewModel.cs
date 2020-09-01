using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinner.ViewModels
{
    public class RecipeEditViewModel : RecipeViewModel
    {
        public string EncryptedId { get; set; }
        public int Id { get; set; }
        public string ExistingPhotoPath { get; set; }
    }
}
