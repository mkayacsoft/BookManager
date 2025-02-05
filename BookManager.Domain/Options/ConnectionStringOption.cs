using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Domain.Options
{
    public class ConnectionStringOption
    {
        public const string Key = "ConnectionStrings";

        public string DefaultConnection { get; set; } = null!;
    }
}
