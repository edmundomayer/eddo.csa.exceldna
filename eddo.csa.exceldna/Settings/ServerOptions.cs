using eddo.csa.exceldna.Interfaces;

namespace eddo.csa.exceldna.Settings
{

    public class ServerOptions : IServerOptions
    {
        #region Properties
        public string Name { get; set; } = string.Empty;
        public string OS { get; set; } = string.Empty;

        // This property does not have a corresponding value in appsettings.json. 
        // Not a problem though, as the property is initialized to true.
        public bool HasInitializer { get; set; } = true;

        // This property does not have a corresponding value in appsettings.json.
        // Not a problem though, as it will be set to the default value for the 
        // type (ie false).
        public bool NoInitializer { get; set; }
        #endregion Properties
    }
}
