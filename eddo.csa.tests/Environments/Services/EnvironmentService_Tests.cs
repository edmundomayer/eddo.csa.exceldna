using eddo.csa.environments.Interfaces;
using eddo.csa.environments.Validations;
using eddo.csa.git.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace eddo.csa.tests.Environments.Services
{
    internal class EnvironmentService_Tests : TestBase
    {
        #region Fields
        #endregion Fields


        #region Constructors & Destructors
        public EnvironmentService_Tests()
        { }
        #endregion Constructors & Destructors


        #region Methods
        [SetUp]
        public void Setup()
        { }

        [Test]
        public void Prepare_Script_HOTFIX_Test()
        {
            var service = base.GetHost().Services.GetRequiredService<IEnvironmentService>();

            var batchCommands = service.PrepareCommands( "HOTFIX" );

            foreach( var command in batchCommands )
                Console.WriteLine( command );

            Assert.Pass();
        }

        [Test]
        public void Prepare_Script_EVO1_Test()
        {
            var service = base.GetHost().Services.GetRequiredService<IEnvironmentService>();

            var batchCommands = service.PrepareCommands( "EVO1" );

            foreach( var command in batchCommands )
                Console.WriteLine( command );

            Assert.Pass();
        }

        [Test]
        public void Prepare_Script_EVO2_Test()
        {
            var service = base.GetHost().Services.GetRequiredService<IEnvironmentService>();

            var batchCommands = service.PrepareCommands( "EVO2" );

            foreach( var command in batchCommands )
                Console.WriteLine( command );

            Assert.Pass();
        }
        #endregion Methods
    }
}
