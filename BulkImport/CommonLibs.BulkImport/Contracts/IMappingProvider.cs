using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibs.BulkImport.Application.Contracts
{
    public interface IMappingProvider<T>
    {
        Dictionary<string, Func<T, object>> GetMappings();
    }
}
