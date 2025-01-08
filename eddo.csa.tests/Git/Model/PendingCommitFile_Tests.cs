//using eddo.csa.git.Model;

//namespace eddo.csa.tests.Git.Model
//{
//    public class PendingCommitFile_Tests
//    {
//        #region Fields
//        private readonly record struct PendingCommitFileSample( string setFullFileName, string getFullFileName, string getFileName, string getFilePath )
//        {
//            public string SetFullFileName => setFullFileName;
//            public string GetFullFileName => getFullFileName;
//            public string GetFileName => getFileName;
//            public string GetFilePath => getFilePath;
//        }

//        private PendingCommitFileSample[] _sampleData = new PendingCommitFileSample[]
//        {
//            new PendingCommitFileSample( null, null, null, null ),
//            new PendingCommitFileSample( string.Empty, null, null, null ),
//            new PendingCommitFileSample( @"  sample.txt  ", @"sample.txt", null, @"sample.txt" ),
//            new PendingCommitFileSample( @"  c:\temp\sample.txt  ", @"c:\temp\sample.txt", @"c:\temp", @"sample.txt" ),
//        };
//        #endregion Fields


//        #region Constructors & Destructors
//        public PendingCommitFile_Tests()
//        { }
//        #endregion Constructors & Destructors


//        #region Methods
//        [SetUp]
//        public void Setup()
//        { }

//        [Test]
//        public void PendingCommitFile_WhenInvalidValuesReceived_Test()
//        {
//            // Act
//            //
//            foreach( var sample in _sampleData )
//            {
//                var test = new PendingCommitFile( sample.SetFullFileName );

//                Assert.That( test.FullFileName, Is.EqualTo( sample.GetFullFileName ) );
//                Assert.That( test.FileName, Is.EqualTo( sample.GetFileName ) );
//                Assert.That( test.FilePath, Is.EqualTo( sample.GetFilePath ) );
//            }

//            Assert.Pass();
//        }
//        #endregion Methods
//    }
//}