using CommonLibs.BulkImport.Sample.Samples;
using Xunit;

namespace CommonLibs.BulkImport.Sample.EntityFrameworkCore.Domains;

[Collection(SampleTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<SampleEntityFrameworkCoreTestModule>
{

}
