using System.Text.Json;

namespace BlazorAgGrid
{
    public static class IGetRowsParamsExtension
    {
        public static T? FileterModelToObject<T>(this IGetRowsParams input)
        {
            return JsonSerializer.Deserialize<T>(input.StringFilterModel);
        }
    }
}
