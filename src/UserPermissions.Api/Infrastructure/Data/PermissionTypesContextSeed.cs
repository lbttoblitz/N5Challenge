using System.Reflection;
using System.Globalization;
using CsvHelper;
using UserPermissions.Api.Domain.Entities;

namespace UserPermissions.Api.Infrastructure.Data
{
    public class PermissionTypesContextSeed
    {
        public static async Task SeedAsync(PermissionContext context)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (!context.PermissionTypes.Any())
            {
                using (var reader = new StreamReader($"{path}/Infrastructure/Data/Csvs/permission-types.csv"))
                using (var csvCategories = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var permissionTypes = csvCategories.GetRecords<PermissionType>();
                    context.PermissionTypes.AddRange(permissionTypes);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
