using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MidiMarket.Startup))]
namespace MidiMarket
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
