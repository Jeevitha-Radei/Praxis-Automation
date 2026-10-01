using FlaUI.Core;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using FlaUI.UIA3;

namespace Assignment1_Part {
   internal class PartImport {
      private readonly Application _application;
      private readonly UIA3Automation _automation;
      public PartImport(Application application, UIA3Automation automation) {
         _application = application;
         _automation = automation;
      }
      public void ImportParts() {
         var window = _application.GetMainWindow(_automation);
         window?.Focus();
         Thread.Sleep(1000);
         Keyboard.Type("lp");
         Thread.Sleep(1000);
         Keyboard.Type("op");
         Thread.Sleep(2000);
         Keyboard.Type(@"C:\Program Files\TRUMPF\TMM\Praxis\Samples\Parts");
         Keyboard.Press(VirtualKeyShort.ENTER);
         Thread.Sleep(2000);
         Keyboard.Type("\"00.14.dxf\" \"00.23.dxf\" \"00.26.dxf\" \"00.03.dxf\"");
         Keyboard.Press(VirtualKeyShort.ENTER);
      }
   }
}