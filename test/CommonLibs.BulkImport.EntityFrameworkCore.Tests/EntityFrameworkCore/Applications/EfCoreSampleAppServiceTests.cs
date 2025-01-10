using CommonLibs.BulkImport.Samples;
using Xunit;

namespace CommonLibs.BulkImport.EntityFrameworkCore.Applications;

[Collection(BulkImportTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<BulkImportEntityFrameworkCoreTestModule>
{

}
