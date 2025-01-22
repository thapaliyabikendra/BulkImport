namespace CommonLibs.BulkImport.Application.Attributes;
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class UniqueIdentifierAttribute : Attribute
{
    // You can store any relevant data in the attribute, like a description or any metadata
    public string Description { get; set; }

    public UniqueIdentifierAttribute()
    {
    }

    public UniqueIdentifierAttribute(string description)
    {
        Description = description;
    }
}