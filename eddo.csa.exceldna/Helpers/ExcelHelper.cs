using ExcelDna.Integration;

namespace eddo.csa.exceldna.Helpers
{
    internal class ExcelHelper
    {
        public static bool TableExists( string tableName )
        {
            dynamic Excel;

            Excel = ExcelDnaUtil.Application;

            foreach( var sheet in Excel.Worksheets )
            {
                foreach( var table in sheet.ListObjects )
                {
                    if( table.Name == tableName )
                        return true;
                }
            }

            return false;
        }

        public static dynamic GetTable( string tableName )
        {
            dynamic Excel;

            Excel = ExcelDnaUtil.Application;

            foreach( var sheet in Excel.Worksheets )
            {
                foreach( var table in sheet.ListObjects )
                {
                    if( table.Name == tableName )
                        return table;
                }
            }

            return false;
        }

        public static bool WorksheetExists( string worksheetName )
        {
            dynamic Excel;

            Excel = ExcelDnaUtil.Application;

            foreach( var sheet in Excel.Worksheets )
            {
                if( sheet.Name == worksheetName )
                    return true;
            }

            return false;
        }

        public static dynamic CreateWorksheet( string worksheetName )
        {
            dynamic result = null;
            dynamic Excel;

            Excel = ExcelDnaUtil.Application;


            var lastWorkSheet = Excel.Worksheets.Item[ Excel.Worksheets.Count ];


            if( !WorksheetExists( worksheetName ) )
            {
                result = Excel.Worksheets.Add( After: lastWorkSheet );
                result.Name = worksheetName;
            }

            //lastWorkSheet = Excel.Worksheets.Item[ Excel.Worksheets.Count ].Activate;

            //return lastWorkSheet;

            return result;
        }

        public static dynamic CreateTable( string worksheetName, string tableName )
        {
            dynamic Excel;

            Excel = ExcelDnaUtil.Application;

            if( !WorksheetExists( worksheetName ) )
            {
                /*dynamic newWorksheet = */
                CreateWorksheet( worksheetName ).Activate();
                //newWorksheet.Activate;
            }

            //Excel.ActiveWorksheet = worksheetName;

            //if( !TableExists( tableName ) )
            //    result = CreateTable( tableName );

            return CreateTable( tableName ); ;
        }

        public static dynamic CreateTable( string tableName )
        {
            dynamic result = null;
            dynamic Excel;

            Excel = ExcelDnaUtil.Application;

            if( !TableExists( tableName ) )
            {
                var activeSheet = Excel.ActiveSheet;

                dynamic tableRange = activeSheet.Range( "A1" );

                result = activeSheet.ListObjects.Add( Source: tableRange, XlListObjectHasHeaders: 1/*, TableStyleName: tableName*/ );
                result.Name = tableName;
            }
            else
                result = GetTable( tableName );

            return result;
        }
    }
}
