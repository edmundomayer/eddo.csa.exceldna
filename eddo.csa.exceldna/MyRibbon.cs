﻿using ExcelDna.Integration.CustomUI;
using System.Runtime.InteropServices;

namespace eddo.csa.exceldna
{
    [ComVisible( true )]
    public class MyRibbon : ExcelRibbon
    {
        public override string GetCustomUI( string RibbonID )
        {
            return RibbonResources.Ribbon;
        }

        public override object? LoadImage( string imageId )
        {
            // This will return the image resource with the name specified in the image='xxxx' tag
            return RibbonResources.ResourceManager.GetObject( imageId );
        }

        public void OnButtonPressed( IRibbonControl control )
        {
            System.Windows.Forms.MessageBox.Show( "Hello!" );
        }
    }
}
