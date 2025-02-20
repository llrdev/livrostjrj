using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace Webapi.Extensions
{
    public static class ModelStateExtensions
    {
        public static IDictionary<string, IEnumerable<string>> GetFailures(this ModelStateDictionary modelState)
        {
            var failures = new Dictionary<string, IEnumerable<string>>();
            modelState.ToList().ForEach(i =>
            {
                if (i.Value.Errors.Any())
                    failures.Add(i.Key, i.Value.Errors.Select(x => x.ErrorMessage));
            });
            return failures;
        }

    }
}
