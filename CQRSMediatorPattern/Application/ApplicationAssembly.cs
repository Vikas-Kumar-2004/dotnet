using System.Reflection;

namespace CQRSMediatorPattern.Application
{
    public class ApplicationAssembly
    {
        public static readonly Assembly Instance = typeof(ApplicationAssembly).Assembly;
    }
}
