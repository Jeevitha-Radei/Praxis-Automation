using FlaUI.Core;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using FlaUI.UIA3;

namespace Assignment1_Part {
   internal class AssignMaterial{
      private readonly Application _application;
      private readonly UIA3Automation _automation;
      public AssignMaterial(Application application, UIA3Automation automation) {
         _application = application;
         _automation = automation;
      }
      public void Assign(string partName, string materialName) {
         var window = _application.GetMainWindow(_automation);
         Thread.Sleep(2000);
         var part = window?.FindFirstDescendant(cf => cf.ByName(partName));
         if (part == null) return;
         part.RightClick();
         Thread.Sleep(1000);
         var assignMaterial = window?.FindFirstDescendant(cf => cf.ByName("Assign Material..."));
         if (assignMaterial == null) {
            var settings = window?.FindFirstDescendant(cf => cf.ByAutomationId("_Settings"));
            settings?.Click();
            Thread.Sleep(500);
            assignMaterial = window?.FindFirstDescendant(cf => cf.ByName("Assign Material..."));
         }
         if (assignMaterial == null) return;
         assignMaterial.Click();
         Thread.Sleep(1500);
         SelectMaterial(materialName);
      }

      private void SelectMaterial(string materialName) {
         var desktop = _automation.GetDesktop();
         var material = desktop.FindFirstDescendant(cf => cf.ByName(materialName));
         if (material == null) {
            var allMaterials = desktop.FindFirstDescendant(cf => cf.ByAutomationId(
                    "_MaterialPickerDlgAllRawMaterials"));
            if (allMaterials == null) return;
            allMaterials.Click();
            Thread.Sleep(1000);
            material = desktop.FindFirstDescendant(cf => cf.ByName(materialName));
         }
         if (material != null) {
            material.Click();
            Thread.Sleep(1000);
            Keyboard.Press(VirtualKeyShort.ESC);
            Thread.Sleep(1000);
         }
      }
   }
}