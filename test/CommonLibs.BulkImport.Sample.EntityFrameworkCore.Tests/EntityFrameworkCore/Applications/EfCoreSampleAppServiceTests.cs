using CommonLibs.BulkImport.Sample.Samples;
using Xunit;

namespace CommonLibs.BulkImport.Sample.EntityFrameworkCore.Applications;

[Collection(SampleTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<SampleEntityFrameworkCoreTestModule>
{

}
