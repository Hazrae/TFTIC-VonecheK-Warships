using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWarships_Web.Infrastructure
{
    public interface ISessionManager
    {
        public int Id { get; set; }
        public string Login { get; set; }    
        public void Abandon();
    }
}
