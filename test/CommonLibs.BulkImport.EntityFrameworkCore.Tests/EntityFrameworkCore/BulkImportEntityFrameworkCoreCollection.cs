using Xunit;

namespace CommonLibs.BulkImport.EntityFrameworkCore;

[CollectionDefinition(BulkImportTestConsts.CollectionDefinitionName)]
public class BulkImportEntityFrameworkCoreCollection : ICollectionFixture<BulkImportEntityFrameworkCoreFixture>
{

}
