using Microsoft.Extensions.DependencyInjection;

namespace AppApiDapper.Installers
{
    public interface IInstaller
    {
        public void InstallerService(IServiceCollection services, IConfiguration configuration);
    }
}
