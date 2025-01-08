namespace eddo.csa.git.Model
{
    public class PendingCommitFile : FileBase
    {
        #region Constructors & Destructors
        public PendingCommitFile( string fullFileName )
            : this( fullFileName, null )
        { }

        public PendingCommitFile( string fullFileName, string type )
            : base( fullFileName )
        {
            Type = type;
        }

        #endregion Constructors & Destructors
        #region Properties
        public string Type { get; set; }
        #endregion Properties
    }
}
