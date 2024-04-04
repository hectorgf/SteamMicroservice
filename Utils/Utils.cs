namespace SteamMicroservice.Utils
{
    public static class Utils
    {
        public static async IAsyncEnumerable<T> ConvertToAsyncEnumerable<T>(this IEnumerable<T> enumerable)
        {
            foreach (var elemento in enumerable)
            {
                yield return elemento;
                await Task.Yield();
            }
        }
    }
}
