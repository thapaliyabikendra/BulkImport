using Xunit;

namespace CommonLibs.BulkImport.Sample.EntityFrameworkCore;

[CollectionDefinition(SampleTestConsts.CollectionDefinitionName)]
public class SampleEntityFrameworkCoreCollection : ICollectionFixture<SampleEntityFrameworkCoreFixture>
{

}
