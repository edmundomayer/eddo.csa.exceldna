using eddo.csa.exceldna.Interfaces;

namespace eddo.csa.exceldna.Settings
{

    public class ContainerOptions : IContainerOptions
    {
        #region Properties
        // According to "Options pattern in ASP.NET Core":

        /*
         * 1. An options class must be non-abstract with a public parameterless constructor. 
         * 
         * 2. All public read-write properties of the type are bound to the configuration section.  
         * So properties Name and Index, below, will be bound.
         * 
         * 3. Fields, such as Title, below, are not bound to the configuration section.
         * 
         */
        public readonly string Title = "Container";
        public string Name { get; set; } = string.Empty;
        public int Index { get; set; } = -1;
        #endregion Properties
    }
}
