using ExcelDna.Integration;
using ExcelDna.Integration.CustomUI;

namespace eddo.csa.exceldna.ToolBox
{
    internal class ModalMessageWindow
    {
        public void OnbtnPressed( IRibbonControl control )
        {
            dynamic xlApp = ExcelDnaUtil.Application;

            int excelHwnd = xlApp.Application.Hwnd;

            new Thread( () =>
            {
                var excelWindowThatIsTheOwner = new NativeWindow();

                excelWindowThatIsTheOwner.AssignHandle( new IntPtr( excelHwnd ) );

                // Show modal dialog (here: a message box)
                MessageBox.Show( owner: excelWindowThatIsTheOwner,
                                text: "I am a modal MessageBox.\r\nNow bring another application to the foreground and then try to bring excel back via the windows taskbar..." );
            } ).Start();
        }
    }
}
