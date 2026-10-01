using FlaUI.Core;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using FlaUI.UIA3;

namespace Assignment1_Part {
   internal class LayoutProcess {
      private readonly Application _application;
      private readonly UIA3Automation _automation;
      public LayoutProcess(Application application,UIA3Automation automation) {
         _application = application;
         _automation = automation;
      }
      public void SendToMachine() {
         var window = _application?.GetMainWindow(_automation);
         Thread.Sleep(2000);
         Keyboard.Type("jj");
         Thread.Sleep(1500);
         var firstJob = window?.FindAllDescendants().
            Where(element =>
                 !string.IsNullOrWhiteSpace(element.Name) &&
                 element.Name.StartsWith("Job-"))
            .OrderBy(element => element.BoundingRectangle.Top)
            .ThenBy(element => element.BoundingRectangle.Left)
            .FirstOrDefault(); 
         firstJob?.DoubleClick();
         Thread.Sleep(2000);
         var firstLayout = window?.FindAllDescendants().                   
            Where(element =>
                 !string.IsNullOrWhiteSpace(element.Name) &&
                 element.Name.StartsWith("P"))
            .OrderBy(element => element.BoundingRectangle.Left)
            .ThenBy(element => element.BoundingRectangle.Top)
            .FirstOrDefault();
         firstLayout?.Click();
         Thread.Sleep(500);
         Keyboard.Press(VirtualKeyShort.CONTROL);
         Keyboard.Press(VirtualKeyShort.KEY_A);
         Keyboard.Release(VirtualKeyShort.CONTROL);
         Thread.Sleep(1000);
         firstLayout?.RightClick();
         Thread.Sleep(500);
         var sendToMachine = _automation.GetDesktop().FindFirstDescendant(cf => cf.ByName("Send to Machine..."));
         sendToMachine?.Click();
         var yesButton = _automation.GetDesktop().FindFirstDescendant(cf => cf.ByName("Yes"));
         yesButton?.Click();
         Thread.Sleep(500);
         var desktop = _automation.GetDesktop();
         var recallFromMachine = desktop.FindFirstDescendant(
             cf => cf.ByName("Recall from Machine..."));
         var checkMark = firstLayout?.FindFirstDescendant(
             cf => cf.ByName("✓"));
         var confirmationMessage = desktop.FindFirstDescendant(
             cf => cf.ByName("Program outputs exported to the target folders."));
         if (recallFromMachine != null && checkMark != null && confirmationMessage != null)
            Console.WriteLine("All layouts were sent to the machine successfully.");
      }
   }
}