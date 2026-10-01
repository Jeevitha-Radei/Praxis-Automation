using FlaUI.Core;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using FlaUI.UIA3;

namespace Assignment1_Part {
   internal class JobCreation {
      private readonly Application _application;
      private readonly UIA3Automation _automation;
      public JobCreation(Application application, UIA3Automation automation) {
         _application = application;
         _automation = automation;
      }
      public void CreateJob(string[] selectPart) {
         var window = _application.GetMainWindow(_automation);
         Thread.Sleep(500);
         Keyboard.Press(VirtualKeyShort.CONTROL);
         foreach (var partName in selectPart) {
            var part = window?.FindFirstDescendant(cf => cf.ByName(partName));
            if (part != null) {
               part.Click();
               Thread.Sleep(500);
            }
         }
         Keyboard.Release(VirtualKeyShort.CONTROL);
         Thread.Sleep(1000);
         var selectedPart = window?.FindFirstDescendant(cf => cf.ByName(selectPart[0]));
         selectedPart?.RightClick();
         Thread.Sleep(500);
         var newMenu = window?.FindFirstDescendant(cf => cf.ByName("New..."));
         newMenu?.Click();
         Thread.Sleep(500);
         var jobMenu = window?.FindFirstDescendant(cf => cf.ByName("Job"));
         jobMenu?.Click();
         Thread.Sleep(1000);
         var jobDialog = window?.ModalWindows.FirstOrDefault();
         if (jobDialog == null) {
            Console.WriteLine("Job dialog not found.");
            return;
         }
         Thread.Sleep(1000);
         Keyboard.Press(VirtualKeyShort.F5);
         Thread.Sleep(1000);
         Thread.Sleep(1000);
         var qty = jobDialog.FindFirstDescendant(cf => cf.ByName("1"));
         if (qty != null) {
            qty.Click();
            Keyboard.Press(VirtualKeyShort.CONTROL);
            Keyboard.Type("A");
            Keyboard.Release(VirtualKeyShort.CONTROL);
            Keyboard.Type("10");
            Keyboard.Press(VirtualKeyShort.TAB);
            Thread.Sleep(500);
         }
         var okButton = jobDialog.FindFirstDescendant(cf => cf.ByName("OK"));
         okButton?.Click();
         Thread.Sleep(1000);
         Keyboard.Press(VirtualKeyShort.ESC);
         Thread.Sleep(1000);
      }
   }
}