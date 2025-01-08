using eddo.csa.exceldna.Helpers;
using eddo.csa.git.Model;
using System.Reflection;

namespace eddo.csa.tests.Exceldna
{
    internal class Helper_Tests
    {
        #region Fields
        private class SampleRecord
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string Alias { get; set; }
        }

        private List<SampleRecord> _sampleRecordList;
        private object[,] _sampleMatrix;
        private object[,] _sampleMatrixWithHeader;
        #endregion Fields


        #region Constructors & Destructors
        public Helper_Tests()
        {
            _sampleRecordList = new List<SampleRecord>
            {
                new SampleRecord{ Id = 1, Name = "List_01", DateOfBirth = new DateTime( 1970,05,01 ) },
                new SampleRecord{ Id = 2, Name = "List_02", DateOfBirth = new DateTime( 1970,05,02 ) },
                new SampleRecord{ Id = 3, Name = "List_03", DateOfBirth = new DateTime( 1970,05,03 ) },
                new SampleRecord{ Id = 4, Name = "List_04", DateOfBirth = new DateTime( 1970,05,04 ) },
                new SampleRecord{ Id = 5, Name = "List_05", DateOfBirth = new DateTime( 1970,05,05 ) },
            };

            _sampleMatrix = new object[ 5, 3 ]
                {
                    { 11, "Matrix_01", new DateTime( 2024,05,01 ) },
                    { 12, "Matrix_02", new DateTime( 2024,05,02 ) },
                    { 13, "Matrix_03", new DateTime( 2024,05,03 ) },
                    { 14, "Matrix_04", new DateTime( 2024,05,04 ) },
                    { 15, "Matrix_05", new DateTime( 2024,05,05 ) }
                };

            _sampleMatrixWithHeader = new object[ 6, 4 ]
                {
                    { "Alias",    "Name",      "Id",  "DateOfBirth" },
                    { "Alias_01", "Matrix_01", 11,    new DateTime( 2024,05,01 ) },
                    { "Alias_02", "Matrix_02", 12,    new DateTime( 2024,05,02 ) },
                    { "Alias_03", "Matrix_03", 13,    new DateTime( 2024,05,03 ) },
                    { "Alias_04", "Matrix_04", 14,    new DateTime( 2024,05,04 ) },
                    { "Alias_05", "Matrix_05", 15,    new DateTime( 2024,05,05 ) }
                };
        }
        #endregion Constructors & Destructors


        #region Methods
        [SetUp]
        public void Setup()
        { }

        private void DoSomething<TSource>( Func<TSource, object> propertiesSelector )
        {
            PropertyInfo[] props = typeof( TSource ).GetProperties( BindingFlags.Public | BindingFlags.Instance );


            props.Select( x => x.Name ).ToArray();


            for( int currentColumn = 0; currentColumn < props.Length; currentColumn++ )
            {

            }
        }

        [Test]
        public void PendingCommitFile_WhenInvalidValuesReceived_Test()
        {
            var t = _sampleRecordList.ToList();

            DoSomething<PendingCommitFile>( propertiesSelector: ( ( x ) => new { x.FilePath, x.FileName } ) );

            var uno = t.First();
            DoSomething<PendingCommitFile>( propertiesSelector: ( ( x ) => new { x.FilePath, x.FileName } ) );


            var r = t.ToMatrix();

            var k = t.ToMatrix( /*includeHeaders: true*/ );



            var s = _sampleMatrix;

            var j = s.ToList<SampleRecord>();


            var w = _sampleMatrixWithHeader;

            var z = w.ToList<SampleRecord>( /*hasHeaders: true*/ );


            Assert.Pass();
        }
        #endregion Methods
    }
}
