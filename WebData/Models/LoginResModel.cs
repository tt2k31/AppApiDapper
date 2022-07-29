using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebData.Models
{
    public class LoginResModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string token { get; set; }
    }
}
