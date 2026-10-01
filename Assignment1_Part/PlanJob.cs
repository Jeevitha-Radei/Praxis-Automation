using FlaUI.Core;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using FlaUI.UIA3;

namespace Assignment1_Part {
   internal class PlanJob {
      private readonly Application _application;
      private readonly UIA3Automation _automation;
      public PlanJob(Application application, UIA3Automation automation) {
         _application = application;
         _automation = automation;
      }
      public void Plan() {
         var window = _application?.GetMainWindow(_automation);
         Keyboard.Type("j");
         Thread.Sleep(1000);
         var jobs = window?.FindAllDescendants()
             .Where(element =>
                 !string.IsNullOrWhiteSpace(element.Name) &&
                 element.Name.StartsWith("Job-"))
             .OrderBy(element => element.BoundingRectangle.Top)
             .ThenBy(element => element.BoundingRectangle.Left)
             .ToArray();
         var firstJob = jobs?.FirstOrDefault();
         firstJob?.RightClick();
         Thread.Sleep(1000);
         var planJob = window?.FindFirstDescendant(cf => cf.ByName("Plan Job..."));
         planJob?.Click();
         Thread.Sleep(500);
         var machine = window?.FindFirstDescendant(cf => cf.ByName("L49-3kW  (2/20) parts"));
         machine?.Click();
         Thread.Sleep(1000);
         Keyboard.Press(VirtualKeyShort.ESC);
         Thread.Sleep(2000);
      }
   }
}                                                                   