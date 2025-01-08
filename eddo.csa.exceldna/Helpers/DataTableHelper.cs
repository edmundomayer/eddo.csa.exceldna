using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace eddo.csa.exceldna.Helpers
{
    public static class DataTableHelper
    {
        // List<T> to DataTable
        public static DataTable ToDataTable<TSource>( this IList<TSource> data )
        {
            DataTable dataTable = new DataTable( typeof( TSource ).Name );

            PropertyInfo[] props = typeof( TSource ).GetProperties( BindingFlags.Public | BindingFlags.Instance );

            foreach( PropertyInfo prop in props )
            {
                dataTable.Columns.Add( prop.Name, Nullable.GetUnderlyingType( prop.PropertyType ) ?? prop.PropertyType );
            }

            foreach( TSource item in data )
            {
                var values = new object[ props.Length ];

                for( int i = 0; i < props.Length; i++ )
                {
                    values[ i ] = props[ i ].GetValue( item, null );
                }

                dataTable.Rows.Add( values );
            }
            return dataTable;
        }


        // DataTable to List<T>
        //
        public static List<TSource> ToList<TSource>( this DataTable dataTable ) where TSource : new()
        {
            var dataList = new List<TSource>();

            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;

            var objFieldNames = ( from PropertyInfo aProp in typeof( TSource ).GetProperties( flags )
                                  select new
                                  {
                                      Name = aProp.Name,
                                      Type = Nullable.GetUnderlyingType( aProp.PropertyType ) ?? aProp.PropertyType
                                  } ).ToList();

            var dataTblFieldNames = ( from DataColumn aHeader in dataTable.Columns
                                      select new
                                      {
                                          Name = aHeader.ColumnName,
                                          Type = aHeader.DataType
                                      } ).ToList();

            var commonFields = objFieldNames.Intersect( dataTblFieldNames ).ToList();

            foreach( DataRow dataRow in dataTable.AsEnumerable().ToList() )
            {
                var aTSource = new TSource();

                foreach( var aField in commonFields )
                {
                    PropertyInfo propertyInfos = aTSource.GetType().GetProperty( aField.Name );

                    var value = ( dataRow[ aField.Name ] == DBNull.Value ) ? null : dataRow[ aField.Name ]; //if database field is nullable

                    propertyInfos.SetValue( aTSource, value, null );
                }

                dataList.Add( aTSource );
            }

            return dataList;
        }


        // List<T> to object[,]
        //
        public static object[,] ToMatrix<TSource>( this IEnumerable<TSource> source, Expression<Func<TSource, object>> propertiesSelector = null/*, bool includeHeaders = false*/ )
        {
            PropertyInfo[]? props = typeof( TSource ).GetProperties( BindingFlags.Public | BindingFlags.Instance );

            if( propertiesSelector != null )
            {
                var extractedPropertyInfoFromExpression = ReferencePropertyHelper.GetReferencedProperties( propertiesSelector ).ToList();

                props = extractedPropertyInfoFromExpression.Select( x => props.FirstOrDefault( p => p.Name == x.Name ) )
                    .ToArray();
            }

            var rows = source.Count();
            var columns = props.Length;

            object[,] result = new object[ rows /*+ ( includeHeaders ? 1 : 0 )*/, columns ];

            //if( includeHeaders )
            //{
            //    int currentColumn = 0;

            //    foreach( PropertyInfo prop in props )
            //    {
            //        result[ 0, currentColumn++ ] = prop.Name;
            //    }
            //}

            var currentRow = 0;

            foreach( TSource item in source )
            {
                for( int currentColumn = 0; currentColumn < props.Length; currentColumn++ )
                {
                    result[ currentRow /*+ ( includeHeaders ? 1 : 0 )*/, currentColumn ] = props[ currentColumn ].GetValue( item, null );
                }

                currentRow++;
            }

            return result;
        }




        // Matrix to List<T>
        //
        public static IEnumerable<TSource> ToList<TSource>( this object[,] source, bool hasHeaders = false )
            where TSource : new()
        {
            var result = new List<TSource>();
            object[] matrixHeader = null;

            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;

            var objFieldNames = ( from PropertyInfo aProp in typeof( TSource ).GetProperties( flags )
                                  select new
                                  {
                                      Name = aProp.Name,
                                      Type = Nullable.GetUnderlyingType( aProp.PropertyType ) ?? aProp.PropertyType
                                  } ).ToList();

            if( hasHeaders )
                matrixHeader = source.GetRow( 0 );

            for( var row = !hasHeaders ? 0 : 1; row < source.GetLength( 0 ); row++ )
            {
                var aTSource = new TSource();
                var currentColumn = 0;

                foreach( var aField in objFieldNames )
                {
                    PropertyInfo propertyInfos = aTSource.GetType().GetProperty( aField.Name );

                    if( hasHeaders )
                        currentColumn = Array.FindIndex( matrixHeader, row => row.ToString() == aField.Name );

                    if( currentColumn != -1 && currentColumn < source.GetLength( 1 ) )
                    {
                        var value = source[ row, currentColumn++ ];

                        propertyInfos.SetValue( aTSource, value, null );
                    }
                }

                result.Add( aTSource );
            }

            return result;
        }


        public static T[] GetColumn<T>( this T[,] matrix, int columnNumber )
        {
            return Enumerable.Range( 0, matrix.GetLength( 0 ) )
                    .Select( x => matrix[ x, columnNumber ] )
                    .ToArray();
        }

        public static T[] GetRow<T>( this T[,] matrix, int rowNumber )
        {
            return Enumerable.Range( 0, matrix.GetLength( 1 ) )
                    .Select( x => matrix[ rowNumber, x ] )
                    .ToArray();
        }
    }
}
