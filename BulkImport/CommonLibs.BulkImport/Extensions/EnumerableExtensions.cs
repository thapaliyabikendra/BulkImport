using System.Linq.Expressions;
using System.Reflection;

namespace CommonLibs.BulkImport.Application.Extensions;
public static class EnumerableExtensions
{
    // This extension method allows you to filter a collection by any string property.
    public static IEnumerable<TDto> FilterByUniqueIdentifier2<TDto>(this IEnumerable<TDto> source, string fieldName)
    {
        // Define the parameter for the expression (e.g., s => s.DataIdentifier)
        ParameterExpression parameter = Expression.Parameter(typeof(TDto), "s");

        // Get the property to be filtered using reflection
        PropertyInfo property = typeof(TDto).GetProperty(fieldName);

        // Build the expression to check if the property is not null or whitespace
        Expression propertyAccess = Expression.Property(parameter, property);
        Expression checkNotNullOrWhitespace = Expression.Call(
            propertyAccess,
            typeof(string).GetMethod("IsNullOrWhiteSpace", new[] { typeof(string) })
        );

        // Create a lambda expression that returns `!IsNullOrWhiteSpace(property)`
        Expression negation = Expression.Not(checkNotNullOrWhitespace);
        Expression<Func<TDto, bool>> lambda = Expression.Lambda<Func<TDto, bool>>(negation, parameter);

        // Apply the filter dynamically using LINQ's Where method
        return source.Where(lambda.Compile());
    }
}