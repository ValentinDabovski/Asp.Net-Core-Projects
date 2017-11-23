namespace CameraBazaar.Services.Infrastructure.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class GetEnumFlagsExtensions
    {
        public static IEnumerable<Enum> GetFlags(this Enum e)
        {
            return Enum.GetValues(e.GetType()).Cast<Enum>().Where(e.HasFlag);
        }
    }
}
