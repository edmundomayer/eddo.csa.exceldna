using eddo.csa.environments.Helpers;
using eddo.csa.environments.Interfaces;
using eddo.csa.environments.Model;
using eddo.csa.environments.Settings;
using FluentValidation;
using FluentValidation.Results;
using System.Xml;

namespace eddo.csa.environments.Services
{
    internal class EnvironmentService : IEnvironmentService
    {
        #region Fields
        private EnvironmentSettings _settings;
        private IValidator<EnvironmentSettings> _validator;
        #endregion Fields


        #region Constructors & Destructors
        public EnvironmentService( EnvironmentSettings settings,
                                    IValidator<EnvironmentSettings> validator )
        {
            _settings = settings;
            _validator = validator;
        }
        #endregion Constructors & Destructors


        #region Methods
        public void GeneratePublisFile( string templatePath, string destinationPath, string publishUrl, string buildConfiguration )
        {
            XmlDocument doc = new XmlDocument();

            if( File.Exists( templatePath ) )
                doc.Load( templatePath );
            else
                doc.LoadXml( Resource.PublishXmlFile );


            XmlNode root = doc.DocumentElement;

            XmlNode node = root.SelectSingleNode( "PropertyGroup/PublishUrl" );
            node.InnerText = publishUrl;


            node = root.SelectSingleNode( "PropertyGroup/LastUsedBuildConfiguration" );
            node.InnerText = buildConfiguration;

            PrepareFolder( Path.GetDirectoryName( destinationPath ) );

            doc.Save( destinationPath );
        }

        public void PrepareFolder( string path )
        {
            var configDir = new DirectoryInfo( path );

            if( !configDir.Exists )
                configDir.Create();
        }

        public string GetProjectType( string projectPath )
        {
            XmlDocument doc = new XmlDocument();

            doc.Load( projectPath );

            XmlNamespaceManager ns = new XmlNamespaceManager( doc.NameTable );
            ns.AddNamespace( "msbld", "http://schemas.microsoft.com/developer/msbuild/2003" );
            XmlNode node = doc.SelectSingleNode( "//msbld:OutputType", ns );

            return node.InnerText.ToLower();
        }

        public string GetOutputFolder( string projectPath, string PublishType )
        {
            XmlDocument doc = new XmlDocument();

            doc.Load( projectPath );

            XmlNamespaceManager ns = new XmlNamespaceManager( doc.NameTable );
            ns.AddNamespace( "msbld", "http://schemas.microsoft.com/developer/msbuild/2003" );
            XmlNode node = doc.SelectSingleNode( "//msbld:OutputPath", ns );

            if( node.InnerText.Contains( PublishType ) )
            {
                return node.InnerText;
            }

            return node.InnerText.Contains( "Debug" ) ? node.InnerText.Replace( "Debug", "Release" ) : node.InnerText.Replace( "Release", "Debug" );
        }
        #endregion Methods


        #region Implements Interface IEnvironmentService
        public void PrepareFolders( params string[] branchNames )
        {
            EnvironmentSettings settings = ( EnvironmentSettings ) _settings;

            foreach( var branch in settings.Branches.Where( x => branchNames == null
                                                                    || branchNames.Length == 0
                                                                    || branchNames.Select( p => p.ToLower().Trim() ).ToList().Contains( x.BranchName.ToLower() ) )
                                                    .OrderBy( x => x.BranchName ) )
            {
                foreach( var section in branch.Sections.OrderBy( x => x.SectionName ).ToArray() )
                {
                    foreach( var configuration in section.Configurations.OrderBy( x => x.Order ).ToArray() )
                    {
                        IConfigurationInterpreter configurationInterpreter = new ConfigurationInterpreter( environmentSetting: settings,
                                                                                                            branch: branch,
                                                                                                            section: section,
                                                                                                            configuration: configuration );
                        PrepareFolder( configurationInterpreter.ConfigurationFolder );
                    }
                }
            }
        }

        public List<string> PrepareCommands( params string[] branchNames )
        {
            List<string> result = new List<string>();
            EnvironmentSettings settings = ( EnvironmentSettings ) _settings;

            foreach( var branch in settings.Branches.Where( x => branchNames == null
                                                                    || branchNames.Length == 0
                                                                    || branchNames.Select( p => p.ToLower().Trim() ).ToList().Contains( x.BranchName.ToLower() ) )
                                                    .OrderBy( x => x.BranchName ) )
            {
                IBranchInterpreter branchInterpreter = new BranchInterpreter( environmentSetting: settings,
                                                                                branch: branch );

                result.Plus( FileContentHelper.SetEchoOn() )
                                                .NewLine()
                                                .AddBranchTitle( string.Format( "Starting with Branch: [{0}]", branchInterpreter.BranchNameTitle ) );


                foreach( var section in branch.Sections.OrderBy( x => x.SectionName ).ToArray() )
                {
                    foreach( var configuration in section.Configurations.OrderBy( x => x.Order ).ToArray() )
                    {
                        IConfigurationInterpreter configurationInterpreter = new ConfigurationInterpreter( environmentSetting: settings,
                                                                                                branch: branch,
                                                                                                section: section,
                                                                                                configuration: configuration );

                        result.NewLine()
                                .AddConfigurationTitle( string.Format( "Starting with Configuration: [{0}]", configurationInterpreter.ConfigurationTitle ) );

                        if( configurationInterpreter.ClearBeforeBuild )
                            result.NewLine()
                                    .PlusRem( "Cleaning previous Compilation." )
                                    .Plus( string.Format( "\"{0}\" \"{1}\" /t:Clean /property:Configuration={2} /property:Platform=x64", configurationInterpreter.MSBuildCommand, configurationInterpreter.ProjectFile, branch.PublishType ) );

                        if( configurationInterpreter.Build )
                        {
                            var projectType = GetProjectType( configurationInterpreter.ProjectFile ).ToLower();

                            switch( projectType )
                            {
                                case "exe":
                                case "winexe":
                                    PrepareFolder( configurationInterpreter.ConfigurationFolder );

                                    // Generate Publish file without Template
                                    //
                                    result.NewLine()
                                            .PlusRem( "Cleaning  Publish folder." )
                                            .Plus( string.Format( "rmdir \"{0}\"", configurationInterpreter.PublishDirectory ) )
                                            .NewLine()
                                            .PlusRem( "Compile & Publish." )
                                            .Plus( string.Format( "\"{0}\" \"{1}\" /t:Rebuild /property:Configuration={2} /property:Platform=x64", configurationInterpreter.MSBuildCommand, configurationInterpreter.ProjectFile, branch.PublishType ) )

                                            .NewLine()
                                            .Plus( string.Format( "xcopy /I /s /y  \"{0}\" \"{1}\"",
                                                                    Path.Combine( configurationInterpreter.ProjectDirectory, GetOutputFolder( configurationInterpreter.ProjectFile, branch.PublishType ) ),
                                                                    configurationInterpreter.PublishDirectory ) );
                                    break;

                                case "library":
                                    result.NewLine()
                                            .PlusRem( string.Format( "Creating Publish file at: {0}", configurationInterpreter.ConfigurationPublishFile ) );

                                    // Generate Publish file with Template
                                    //
                                    GeneratePublisFile( configurationInterpreter.ConfigurationPublishTemplate,
                                                        configurationInterpreter.ConfigurationPublishFile,
                                                        configurationInterpreter.PublishDirectory,
                                                        branch.PublishType );

                                    result.NewLine()
                                            .PlusRem( "Cleaning Publish folder." )
                                            .Plus( string.Format( "rmdir \"{0}\"", configurationInterpreter.PublishDirectory ) )
                                            .NewLine()
                                            .PlusRem( "Compile & Publish." )
                                            .Plus( string.Format( "\"{0}\" \"{1}\" /t:Build /p:DeployOnBuild=true /p:PublishProfile=\"{2}\"",
                                                        configurationInterpreter.MSBuildCommand,
                                                        configurationInterpreter.ProjectFile,
                                                        configurationInterpreter.ConfigurationPublishFile ) );
                                    break;

                                default:
                                    throw new ArgumentException( string.Format( "The Project: '{0}' has an Invalid Project type '{1}' that cannot be used for publish/deployment. Change the project type to a valid one and try again and try again.", configurationInterpreter.ProjectFile, projectType ) );
                            }

                            if( configurationInterpreter.HasAdditionalConfigurationCopy )
                            {
                                // Copy additional Config files
                                //
                                result.NewLine()
                                        .PlusRem( "Copy additional files." );

                                foreach( var additionalConfigurationItem in configuration.AdditionalConfigurationCopy )
                                {
                                    result.Plus( string.Format( "copy \"{0}\" \"{1}\""
                                                                , Path.Combine( configurationInterpreter.ConfigurationFolder, additionalConfigurationItem.Source )
                                                                , Path.Combine( configurationInterpreter.PublishDirectory, additionalConfigurationItem.Destination ) ) );
                                }

                            }

                            result.NewLine( 3 );
                        }

                        PrepareFolder( Path.GetDirectoryName( configurationInterpreter.ResultCommandFile ) );

                        using( StreamWriter outputFile = new StreamWriter( string.Format( configurationInterpreter.ResultCommandFile, DateTime.Now.ToString( "yyMMddHHmm - " ) ) ) )
                        {
                            foreach( string line in result )
                                outputFile.WriteLine( line );
                        }
                    }
                }
            }

            return result;
        }

        bool IEnvironmentService.ValidateSettings( out ValidationResult validationResult )
        {
            validationResult = _validator.Validate( _settings );

            return validationResult.IsValid;
        }
        #endregion Implements Interface IEnvironmentService
    }
}
