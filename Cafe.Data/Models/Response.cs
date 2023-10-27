using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace Cafe.Data.Models
{
    public class Response
    {
        [Required]
        public string Status { set; get; }
        public string Message { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Username { set; get; }
        public int Computerid { set; get; }
        public int Roleid { set; get; }
        public int Userid { set; get; }
        public string Token { set; get; }
        public string Password { set; get; }

    }

}
