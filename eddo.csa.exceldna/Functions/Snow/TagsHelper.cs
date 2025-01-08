using eddo.csa.exceldna.Helpers;
using ExcelDna.Integration;

namespace eddo.csa.exceldna.Functions.Snow
{
    public class TagsHelper
    {
        #region Excel Public Functions
        [ExcelFunction( Description = "Split Tags string into array" )]
        public static object SplitTags( string tags )
        {
            if( tags == null || tags.Length == 0 )
                return null;

            var result = tags.Split( ',' ).Where( x => x != null ).Select( x => x.Trim() ).Where( x => x != string.Empty ).ToArray();

            return result;
        }

        [ExcelFunction( Description = "Confirm if a criteria Tag is contained in Tags" )]
        public bool HasTag( string tags, string criteria )
        {
            if( tags == null || tags.Length == 0 || criteria == null || criteria.Length == 0 )
                return false;

            var result = ( string[] ) SplitTags( tags );

            return result.Select( x => x.ToLower() ).Any( x => x == criteria.ToLower() );
        }

        [ExcelFunction( Description = "Confirm if a criteria Tag is contained in Tags and returns 0 when false and 1 when true" )]
        public int HasTagNumericResult( string tags, string criteria )
        {
            {
                var result = HasTag( tags, criteria ) ? 1 : 0;

                return result;
            }
        }

        [ExcelFunction( Description = "Confirm if a criteria Tag is contained in Tags" )]
        public static object[,] Except( object[] range1, object[] range2 )
        {
            if( range1 == null || range1.Length == 0 || range2 == null || range2.Length == 0 )
                return null;

            var first = range1.Where( x => x != null ).Select( x => x.ToString().Trim().ToLower() ).ToArray();
            var second = range2.Where( x => x != null ).Select( x => x.ToString().Trim().ToLower() ).ToArray();

            var except = first.Except( second ).Select( x => new List<string> { x } ).ToArray();

            var result = except.ToMatrix();

            return result;
        }

        [ExcelFunction( Description = "Confirm if a criteria Tag is contained in Tags" )]
        public object[,] Duplicates( object[] range1 )
        {
            if( range1 == null || range1.Length == 0 )
                return null;

            var first = range1.Where( x => x != null ).Select( x => x.ToString().Trim() ).ToArray();

            var duplicates = first.GroupBy( x => x )
                .Select( x => new { number = x.Key, duplicates = x.Count() } )
                .Where( x => x.duplicates > 1 )
                .Select( x => new List<string> { x.number } )
                .ToArray();

            var result = duplicates.ToMatrix();

            return result;
        }
        #endregion Excel Public Functions
    }
}
