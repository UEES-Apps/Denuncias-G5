using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace G5.Denuncias.BE.Infraestructure.Extentions
{
    public static class SessionExtensions
    {
        public static T? ReadFromSession<T>(this ISession session, string key)
        {
            var bytes = session.Get(key);
            if (bytes == null) return default;


            var json = System.Text.Encoding.UTF8.GetString(bytes);
            return JsonSerializer.Deserialize<T>(json);
        }


        public static void WriteToSession<T>(this ISession session, string key, T value)
        {
            var json = JsonSerializer.Serialize(value);
            var bytes = System.Text.Encoding.UTF8.GetBytes(json);
            session.Set(key, bytes);
        }
    }
}
