using eddo.csa.environments.Settings;
using eddo.csa.environments.Validations;
using FluentValidation.Results;

namespace eddo.csa.tests.Environments.Validators
{
    internal class SettingsValidation_Tests : TestBase
    {
        #region Fields
        #endregion Fields


        #region Constructors & Destructors
        public SettingsValidation_Tests()
        { }
        #endregion Constructors & Destructors


        #region Properties
        //protected override Action<ContainerBuilder> OverrideRegister
        //{
        //    get
        //    {
        //        var mocked = new EnvironmentSettings { MSBuild = "Hola" };

        //        return ( builder ) => builder.Register<EnvironmentSettings>( ctx => mocked ).SingleInstance();
        //    }
        //}
        #endregion Properties


        #region Methods
        [SetUp]
        public void Setup()
        { }


        [Test]
        public void WhenEnvironmentSettings_MSBuild_IsNull_ReturnsValidationError_Test()
        {
            // Arrange
            //
            EnvironmentSettingsValidator validator = new EnvironmentSettingsValidator();

            var data = new EnvironmentSettings { MSBuild = null };

            // Act
            //
            var result = validator.Validate( data );

            // Assert
            //
            Assert.That( result.Errors,
                            Has.Exactly( 1 ).Matches<ValidationFailure>( x => x.PropertyName == "MSBuild" && x.ErrorCode == "MSBuildNull" ) );

            // End
            //
            Assert.Pass();
        }


        [Test]
        public void WhenEnvironmentSettings_MSBuild_IsEmpty_ReturnsValidationError_Test()
        {
            // Arrange
            //
            EnvironmentSettingsValidator validator = new EnvironmentSettingsValidator();

            var data = new EnvironmentSettings { MSBuild = string.Empty };

            // Act
            //
            var result = validator.Validate( data );

            // Assert
            //
            Assert.That( result.Errors,
                            Has.Exactly( 1 ).Matches<ValidationFailure>( x => x.PropertyName == "MSBuild" && x.ErrorCode == "MSBuildEmpty" ) );

            // End
            //
            Assert.Pass();
        }


        [Test]
        public void WhenEnvironmentSettings_MSBuild_IsInvalidPath_ReturnsValidationError_Test()
        {
            // Arrange
            //
            EnvironmentSettingsValidator validator = new EnvironmentSettingsValidator();

            var data = new EnvironmentSettings { MSBuild = @"C:\Windows\notExistingFile.txt" };

            // Act
            //
            var result = validator.Validate( data );

            // Assert
            //
            Assert.That( result.Errors,
                            Has.Exactly( 1 ).Matches<ValidationFailure>( x => x.PropertyName == "MSBuild" && x.ErrorCode == "MSBuildInvalidPath" ) );

            // End
            //
            Assert.Pass();
        }
        #endregion Methods
    }
}
