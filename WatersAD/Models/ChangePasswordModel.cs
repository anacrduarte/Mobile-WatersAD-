using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatersAD.Models
{
    public class ChangePasswordModel
    {

        public string Email { get; set; } = null!;

    
     
        public string OldPassword { get; set; } = null!;


        public string NewPassword { get; set; } = null!;



        public string Confirm { get; set; } = null!;
    }
}
