using ExcelDna.Integration;
using System.Linq.Expressions;

namespace eddo.csa.exceldna.Helpers
{
    public static class RangeHelper
    {
        #region Methods
        public static object[,] ReadFromNamedRange( string rangeName )
        {
            return ReadFromNamedRange<object>( rangeName );
        }

        public static T[,] ReadFromNamedRange<T>( string rangeName )
            where T : class, new()
        {
            if( string.IsNullOrEmpty( rangeName ) || !ExcelHelper.TableExists( rangeName ) )
                throw new ArgumentException( string.Format( "[ReadFromNamedRange<T>] - Invalid RangeName provided '{0}'", rangeName ), rangeName );

            dynamic Excel;

            Excel = ExcelDnaUtil.Application;

            var input = ( T[,] ) Excel.Range( rangeName ).Value2;

            int rows = input.GetUpperBound( 0 );
            int cols = input.GetUpperBound( 1 );

            T[,] result = new T[ rows, cols ];

            for( int i = 0; i < rows; i++ )
            {
                for( int j = 0; j < cols; j++ )
                {
                    result[ i, j ] = input[ i + 1, j + 1 ];
                }
            }

            return result;
        }

        public static bool WriteToNamedRange<T>( string rangeName, IEnumerable<T> data, Expression<Func<T, object>> propertiesSelector )
        {
            if( string.IsNullOrEmpty( rangeName ) || !ExcelHelper.TableExists( rangeName ) )
                throw new ArgumentException( string.Format( "[WriteToNamedRange<T>] - Invalid RangeName provided '{0}'", rangeName ), rangeName );

            if( data == null )
                throw new ArgumentException( string.Format( "[WriteToNamedRange<T>] - Invalid Data provided" ) );

            var processedData = data.ToMatrix( propertiesSelector: propertiesSelector/*, includeHeaders: false*/ );

            return RangeHelper.WriteToNamedRange( rangeName, processedData );
        }

        public static bool WriteToNamedRange( string rangeName, object[,] data )
        {
            if( string.IsNullOrEmpty( rangeName ) || !ExcelHelper.TableExists( rangeName ) )
                throw new ArgumentException( string.Format( "Invalid RangeName provided '{0}'", rangeName ), rangeName );

            if( data == null )
                throw new ArgumentException( string.Format( "[WriteToNamedRange] - Invalid Data provided" ) );

            dynamic Excel;

            Excel = ExcelDnaUtil.Application;

            var rows = data.GetLength( 0 );
            var cols = data.GetLength( 1 );

            //if( RangeHelper.NamedRangeIsTable( rangeName ) )
            //{
            var activeSheet = Excel.ActiveSheet;
            var table = activeSheet.ListObjects[ rangeName ];
            var hasHeaders = table.ShowHeaders;

            if( table.DataBodyRange != null )
                table.DataBodyRange.ClearContents();

            var startCell = table.Range.Cells[ 1, 1 ];
            var newRange = activeSheet.Range
                [
                    startCell,
                    activeSheet.Cells
                    [
                        startCell.Row + ( rows == 0 ? 1 : rows ),
                        startCell.Column + ( cols == 0 ? table.ListColumns.Count - 1 : cols - 1 )
                    ]
                ];

            table.Resize( newRange );

            if( table.ListRows.Count == 0 )
                table.ListRows.Add();

            if( rows > 0 )
                table.DataBodyRange.Value2 = data;
            //}
            //else
            //{
            //    // Clear range before resize
            //    Excel.Range[ rangeName ].CurrentRegion = "";

            //    // Resize range
            //    var rangeNewSize = Excel.Range[ rangeName ].Resize[ rows, cols ];

            //    // Redefine RangeName
            //    Excel.Names.Add( Name: rangeName, RefersTo: rangeNewSize );
            //}

            return true;
        }

        //public static bool NamedRangeIsTable( string rangeName )
        //{
        //    if( string.IsNullOrEmpty( rangeName ) || !RangeHelper.NamedRangeExists( rangeName ) )
        //        throw new ArgumentException( string.Format( "Invalid RangeName provided '{0}'", rangeName ), rangeName );

        //    dynamic Excel;

        //    Excel = ExcelDnaUtil.Application;

        //    //var definedName = Excel.Names.Item( rangeName ).NameLocal;

        //    //return definedName.ToString().StartsWith( "=Table" );

        //    foreach( var sheet in Excel.Worksheets )
        //    {
        //        foreach( var table in sheet.ListObjects )
        //        {
        //            if( table.Name == rangeName )
        //                return true;
        //        }
        //    }

        //    return false;
        //}


        //public static bool NamedRangeExists( string rangeName )
        //{
        //    bool isNamedRange = false;
        //    bool isTable = false;

        //    dynamic Excel;

        //    Excel = ExcelDnaUtil.Application;

        //    foreach( var sheet in Excel.Worksheets )
        //    {
        //        foreach( var table in sheet.ListObjects )
        //        {
        //            if( table.Name == rangeName )
        //                isTable = true;
        //        }
        //    }

        //    foreach( var name in Excel.Names )
        //    {
        //        if( name == rangeName )
        //            isNamedRange = true;
        //    }

        //    return isNamedRange || isTable;
        //}
        #endregion Methods
    }
}
