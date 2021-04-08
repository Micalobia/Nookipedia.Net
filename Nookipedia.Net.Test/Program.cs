using System;

namespace Nookipedia.Net.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            using NookipediaClient client = new(Environment.GetEnvironmentVariable("NOOKIPEDIA_TOKEN"));
            // This is here just to have something to run against, it isn't a formal testing thing.
        }
    }
}
