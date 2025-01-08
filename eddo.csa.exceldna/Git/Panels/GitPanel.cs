using eddo.csa.exceldna.Git.Interfaces;
using eddo.csa.exceldna.Helpers;
using eddo.csa.exceldna.Settings;
using eddo.csa.git.Interfaces;
using eddo.csa.git.Model;
using ExcelDna.Integration;
using Microsoft.Extensions.Options;
using System.Runtime.InteropServices;

namespace eddo.csa.exceldna.Git
{
    [ComDefaultInterface( typeof( IGitPanel ) )]
    public partial class GitPanel : UserControl, IGitPanel
    {
        #region Fields
        private readonly IOptions<Dictionary<string, TableMappers>> _tableMapperDictionary;
        private readonly IGitService _gitService;
        private const string _CURRENT_MAPPER = "gitLocal";
        #endregion Fields


        #region Constructors & Destructors
        public GitPanel( IOptions<Dictionary<string, TableMappers>> tableMapperDictionary, IGitService gitService )
            : this()
        {
            _tableMapperDictionary = tableMapperDictionary;
            _gitService = gitService;
        }

        public GitPanel()
        {
            InitializeComponent();
        }
        #endregion Constructors & Destructors


        #region Methods
        private void InitializeControl()
        {
            this.lblBranches.Enabled = _gitService != null;
            this.cmbBranches.Enabled = _gitService != null;
            this.btnLoadPendingChanges.Enabled = _gitService != null;

            if( _gitService != null )
            {
                this.cmbBranches.Items.Clear();
                this.cmbBranches.Items.AddRange( _gitService.GetBrancheAliases().ToArray() );
            }
        }
        #endregion Methods


        #region Events
        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );

            InitializeControl();
        }

        [ExcelFunction( IsMacroType = true )]
        private void btnLoadPendingChanges_Click( object sender, EventArgs e )
        {
            if( _gitService == null )
                return;

            try
            {
                var pendingCommitFiles = _gitService.GetPendingCommitFilesByBranchName( branch => branch.Alias.ToLower() == this.cmbBranches.SelectedItem.ToString().ToLower() );

                //if( !ExcelHelper.WorksheetExists( _tableMapperDictionary.Value[ _CURRENT_MAPPER ].TableName ) )
                //    ExcelHelper.CreateWorksheet( _tableMapperDictionary.Value[ _CURRENT_MAPPER ].TableName );

                ExcelHelper.CreateTable( _tableMapperDictionary.Value[ _CURRENT_MAPPER ].WorksheetName,
                                            _tableMapperDictionary.Value[ _CURRENT_MAPPER ].TableName );


                RangeHelper.WriteToNamedRange<PendingCommitFile>( _tableMapperDictionary.Value[ _CURRENT_MAPPER ].TableName, pendingCommitFiles, propertiesSelector: x => new { x.Type, x.FileName, x.FilePath, x.FullFileName } );
            }
            catch( Exception _error )
            {
                MessageBox.Show( _error.Message.ToString() );
            }
        }
        #endregion Events
    }
}
