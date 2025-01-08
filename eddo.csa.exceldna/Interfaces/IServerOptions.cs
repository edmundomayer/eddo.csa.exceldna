namespace eddo.csa.exceldna.Interfaces
{
    public interface IServerOptions
    {
        string Name { get; set; }
        string OS { get; set; }
        bool HasInitializer { get; set; }
        bool NoInitializer { get; set; }
    }
}
