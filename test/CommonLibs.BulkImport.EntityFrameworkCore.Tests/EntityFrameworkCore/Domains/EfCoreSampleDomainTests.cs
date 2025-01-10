using CommonLibs.BulkImport.Samples;
using Xunit;

namespace CommonLibs.BulkImport.EntityFrameworkCore.Domains;

[Collection(BulkImportTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<BulkImportEntityFrameworkCoreTestModule>
{

}
