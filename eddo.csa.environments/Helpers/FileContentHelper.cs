namespace eddo.csa.environments.Helpers
{
    internal static class FileContentHelper
    {
        #region Fields
        public static char BRANCH_SEPARATOR = '*';
        public static int BRANCH_SEPARATOR_LENGHT = 79;

        public static char CONFIGURATION_SEPARATOR = '-';
        public static int CONFIGURATION_SEPARATOR_LENGHT = 79;
        #endregion Fields


        #region Methods
        public static string Separator( char @char, int repeat ) => SetRem( new string( @char, repeat ) );

        public static string BranchSeparator() => Separator( BRANCH_SEPARATOR, BRANCH_SEPARATOR_LENGHT );
        public static string BranchSeparator( int repeat ) => Separator( BRANCH_SEPARATOR, repeat );
        public static string BranchSeparator( int repeat, string text ) => string.Format( "{0} {1}", BranchSeparator( repeat ), text );

        public static string ConfigurationSeparator() => Separator( CONFIGURATION_SEPARATOR, CONFIGURATION_SEPARATOR_LENGHT );
        public static string ConfigurationSeparator( int repeat ) => Separator( CONFIGURATION_SEPARATOR, repeat );
        public static string ConfigurationSeparator( int repeat, string text ) => string.Format( "{0} {1}", ConfigurationSeparator( repeat ), text );
        #endregion Methods


        #region Extensions
        public static List<string> Plus( this List<string> source, string text )
        {
            source.Add( text );

            return source;
        }
        public static List<string> Plus( this List<string> source, params string[] texts ) => source.Plus( string.Join( string.Empty, texts ) );

        public static List<string> PlusRem( this List<string> source, string text ) => source.Plus( SetRem( text ) );

        public static List<string> NewLine( this List<string> source ) => source.Plus( string.Empty );
        public static List<string> NewLine( this List<string> source, int repeat )
        {
            for( int i = 0; i < repeat; i++ )
            {
                source.NewLine();
            }

            return source;
        }


        public static List<string> AddBranchTitle( this List<string> source, string text )
        {
            return source.Plus( BranchSeparator() )
                            .Plus( BranchSeparator( 1 ) )
                            .Plus( SetEcho( text ) )
                            .Plus( BranchSeparator( 1 ) )
                            .Plus( BranchSeparator() );
        }


        public static List<string> AddConfigurationTitle( this List<string> source, string text )
        {
            return source.Plus( ConfigurationSeparator() )
                            .Plus( ConfigurationSeparator( 1 ) )
                            .Plus( SetEcho( text ) )
                            .Plus( ConfigurationSeparator( 1 ) )
                            .Plus( ConfigurationSeparator() );
        }

        public static string SetEchoOff() => SetEcho( "off" );
        public static string SetEchoOn() => SetEcho( "on" );
        public static string SetEcho( string echo ) => string.Format( "echo {0}", echo );
        public static string SetRem( string rem ) => string.Format( "rem {0}", rem );
        #endregion Extensions
    }
}
