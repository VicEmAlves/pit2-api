using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pit2Api.Model
{
    public record Config
    {
        public string ConnectionString { get; set; }

        public int MaxSessionsPerUser { get; set; }
    }
}
