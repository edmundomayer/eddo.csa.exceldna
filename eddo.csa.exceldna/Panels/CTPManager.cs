using ExcelDna.Integration.CustomUI;

namespace eddo.csa.exceldna.Panels
{
    internal class InternalTaskPane
    {
        #region Properties
        public CustomTaskPane CustomTaskPaneInstance { get; set; }
        public string Title { get; set; }
        #endregion Properties
    }

    internal class CTPManager
    {
        #region Fields
        // WARNING: This won't work well under Excel 2013. There you need a different policy, since a CTP is attached only to a single window (one workbook).
        //          So having a single variable here means you can only ever have one CTP in one of the Excel 2013 windows.
        //          Maybe have a map from workbook to CTP, or have a floating one or something...

        static Dictionary<string, InternalTaskPane> _ctpDictionary = new Dictionary<string, InternalTaskPane>();
        #endregion Fields


        #region Methods
        public static void ShowCTP<T>( string title )
            where T : UserControl, new()
        {
            var userControl = Activator.CreateInstance( typeof( T ) ) as T;

            ShowCTP( userControl, title );
        }

        public static void ShowCTP( UserControl userControl, string title )
        {
            CustomTaskPane ctp = null;

            if( !_ctpDictionary.ContainsKey( title ) )
            //if( ctp == null )
            {
                // Make a new one using ExcelDna.Integration.CustomUI.CustomTaskPaneFactory 
                ctp = CustomTaskPaneFactory.CreateCustomTaskPane( userControl, title );
                ctp.Visible = true;
                ctp.DockPosition = MsoCTPDockPosition.msoCTPDockPositionLeft;
                //ctp.DockPositionStateChange += ctp_DockPositionStateChange;
                ctp.VisibleStateChange += ctp_VisibleStateChange;

                _ctpDictionary.Add( title, new InternalTaskPane { /*CustomTaskPane = ctp,*/ Title = title, CustomTaskPaneInstance = ctp } );
            }
            else
            {
                ctp = _ctpDictionary[ title ]?.CustomTaskPaneInstance;
                // Just show it again
                ctp.Visible = true;
            }
        }

        public static void DeleteCTP( string title )
        {

            if( _ctpDictionary.ContainsKey( title ) )
            //if( ctp != null )
            {
                var ctp = _ctpDictionary[ title ]?.CustomTaskPaneInstance;

                // Could hide instead, by calling ctp.Visible = false;
                _ctpDictionary.Remove( title );
                ctp.Delete();
                ctp = null;

            }
        }
        #endregion Methods


        #region Events
        static void ctp_VisibleStateChange( CustomTaskPane CustomTaskPaneInst )
        {
            MessageBox.Show( "Visibility changed to " + CustomTaskPaneInst.Visible );
        }

        //static void ctp_DockPositionStateChange( CustomTaskPane CustomTaskPaneInst )
        //{
        //    var matchPanel = _ctpDictionary.FirstOrDefault( x => x.Value.CustomTaskPaneInstance == CustomTaskPaneInst );

        //    if( matchPanel.Value != null )
        //    {
        //        var ctp = matchPanel.Value.CustomTaskPaneInstance as CustomTaskPane;
        //        //( ( ContentControl ) ctp.ContentControl ).label1.Text = "Moved to " + CustomTaskPaneInst.DockPosition.ToString();
        //        ctp.ContentControl.label1.Text = "Moved to " + CustomTaskPaneInst.DockPosition.ToString();
        //    }
        //}

        //static KeyValuePair<string, InternalTaskPane> GetByInstance( CustomTaskPane CustomTaskPaneInst )
        //{
        //    return _ctpDictionary.FirstOrDefault( x => x.Value.CustomTaskPaneInstance == CustomTaskPaneInst );
        //}
        #endregion Events
    }
}
