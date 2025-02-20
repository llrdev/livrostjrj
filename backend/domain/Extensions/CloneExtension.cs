using Newtonsoft.Json;

namespace Domain.Extensions
{
    public static class CloneExtension
    {
        public static T Clone<T>(this T source)
        {
            var serialized = JsonConvert.SerializeObject(source, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}
