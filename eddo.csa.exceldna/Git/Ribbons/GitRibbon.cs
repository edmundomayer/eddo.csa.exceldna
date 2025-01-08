using eddo.csa.exceldna.Git.Interfaces;
using eddo.csa.exceldna.Git.Resources;
using eddo.csa.exceldna.Helpers;
using eddo.csa.exceldna.hosting;
using eddo.csa.exceldna.Panels;
using eddo.csa.git.Interfaces;
using eddo.csa.git.Model;
using ExcelDna.Integration;
using ExcelDna.Integration.CustomUI;
using System.Runtime.InteropServices;

namespace eddo.csa.exceldna.Git.Ribbons
{
    [ComVisible( true )]
    public class GitRibbon : HostedExcelRibbon
    {
        #region Fields
        private static IRibbonUI _ribbonUi;
        private IGitService _gitService;
        private IGitPanel _gitPanel;
        #endregion Fields


        #region Constructors & Destructors
        public GitRibbon( IGitService gitService, IGitPanel gitPanel )
        {
            _gitService = gitService;
            _gitPanel = gitPanel;
        }
        #endregion Constructors & Destructors


        #region Methods
        public override object LoadImage( string imageId )
        {
            string imageResourceName;

            switch( imageId )
            {
                case "LoadGitSettings":
                    imageResourceName = "alert_warn";
                    break;
                case "LoadLocalChanges":
                    imageResourceName = "alert_error";
                    break;
                default:
                    return null;
            };

            if( imageResourceName == null )
                return null;

            using( var ms = new MemoryStream( GitResource.ResourceManager.GetObject( imageResourceName ) as byte[] ) )
            {
                return System.Drawing.Image.FromStream( ms );
            }
        }
        #endregion Methods


        #region Events
        [ExcelFunction( IsMacroType = true )]
        public void OnbtnLoadGitSettingsPressed( IRibbonControl control )
        {
            try
            {
                var pendingCommitFiles = _gitService
                        .GetPendingCommitFilesByBranchName( b => b.Alias.ToLower() == "hotfix", f => f.Type.ToLower() != "ignored" )
                        .ToList();

                RangeHelper.WriteToNamedRange<PendingCommitFile>( "myData", pendingCommitFiles, x => new List<object> { x.FullFileName, x.Type } );
            }
            catch( ArgumentException _error )
            {
                MessageBox.Show( _error.Message, "Git Interface", MessageBoxButtons.OK, MessageBoxIcon.Warning );
            }
            catch( Exception _error )
            {
                throw _error;
            }
        }

        [ExcelFunction( IsMacroType = true )]
        public void OnbtnLoadLocalChangesPressed( IRibbonControl control )
        {
            CTPManager.ShowCTP( _gitPanel as GitPanel, "Git Settings pane" );
        }

        public bool OnbtnLoadLocalChangesGetEnabled( IRibbonControl control )
        {
            return true;
        }

        // ComboBox
        //
        public int GetItemCount( IRibbonControl control )
        {
            return _gitService.GetBrancheAliases().Count();
        }

        public string GetItemID( IRibbonControl control, int index )
        {
            return "wB" + index;
        }

        public string GetItemLabel( IRibbonControl control, int index )
        {
            string label = "Unknow Branch";

            string[] branchAliases = _gitService.GetBrancheAliases().ToArray();

            if( branchAliases != null || branchAliases.Length > 0 )
                label = branchAliases[ index ];

            return label;
        }

        public void SaveChoice( IRibbonControl control, string selectedId, int selectedIndex )
        {
            MessageBox.Show( "My Dropdown Selected on control " + control.Id + " with selection " + selectedId + " at index " + selectedIndex );
        }
        #endregion Events


        #region Implements Abstract Class ExcelRibbon
        public override string GetCustomUI( string uiName )
        {
            return @"
<customUI xmlns='http://schemas.microsoft.com/office/2009/07/customui' loadImage='LoadImage'>
 <ribbon>
  <tabs>
   <tab id='tab1' label='FiBa CSA'>
    <group id='git2' label='Settings'>
     <button id='btnLoadGitSettings' label='Load Git settings' image='LoadGitSettings' size='large' onAction='OnbtnLoadGitSettingsPressed'/>
    </group>
    <group id='git1' label='Git'>
     <dropDown id='cmbBranches'                         
                        label='Current Branch' 
                        screentip='Select a branch to work with' 
                        supertip='The branches listed have been obtained from the configuration file git-settings.json' 
                        getItemCount='GetItemCount' 
                        getItemID='GetItemID' 
                        getItemLabel='GetItemLabel' 
                        onAction='SaveChoice' />
     <button id='btnLoadLocalChanges' 
                    label='Load local changes' 
                    screentip='Load Local Changes from Git' 
                    supertip='Click to load all local changes from selected branch' 
                    getEnabled='OnbtnLoadLocalChangesGetEnabled'
                    onAction='OnbtnLoadLocalChangesPressed' 
                    image='LoadLocalChanges'/>
    </group>
   </tab>
  </tabs>
 </ribbon>
</customUI>
";
        }
        #endregion Implements Abstract Class ExcelRibbon
    }
}
