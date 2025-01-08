namespace eddo.csa.environments.Helpers
{
    internal static class ValidationExtensions
    {
        public static bool PathIsFile( this string path )
        {
            if( File.Exists( path ) )
            {
                FileAttributes attr = File.GetAttributes( path );

                return ( ( attr & FileAttributes.Archive ) == FileAttributes.Archive );
            }

            return false;
        }

        public static bool PathIsDirectory( this string path )
        {
            if( File.Exists( path ) )
            {
                FileAttributes attr = File.GetAttributes( path );

                return ( ( attr & FileAttributes.Directory ) == FileAttributes.Directory );
            }

            return false;
        }
    }
}
