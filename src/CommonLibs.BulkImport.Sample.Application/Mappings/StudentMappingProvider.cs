using CommonLibs.BulkImport.Application.Contracts;
using CommonLibs.BulkImport.Sample.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibs.BulkImport.Sample.Mappings
{
    public class StudentMappingProvider : IMappingProvider<StudentDto>
    {
        public Dictionary<string, Func<StudentDto, object>> GetMappings()
        {
            return new Dictionary<string, Func<StudentDto, object>>()
            {
                { "Name", item => item.Name },
                { "Age", item => item.Surname },
                { "Email", item => item.Grade }
            };
        }
    }
}
