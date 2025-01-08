namespace eddo.csa.git.Model
{
    public abstract class FileBase
    {
        #region Fields
        private string _fullFileName;
        #endregion Fields


        #region Constructors & Destructors
        public FileBase( string fullFileName )
        {
            FullFileName = fullFileName;
        }
        #endregion Constructors & Destructors


        #region Properties
        public string FileName => string.IsNullOrEmpty( Path.GetDirectoryName( _fullFileName ) ) ? null : Path.GetDirectoryName( _fullFileName );
        public string FilePath => string.IsNullOrEmpty( Path.GetFileName( _fullFileName ) ) ? null : Path.GetFileName( _fullFileName );


        public string FullFileName { get => _fullFileName; set => _fullFileName = string.IsNullOrEmpty( value ) ? null : value.Trim(); }
        #endregion Properties
    }
}