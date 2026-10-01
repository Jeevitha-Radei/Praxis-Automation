using FlaUI.Core;
using FlaUI.UIA3;

namespace Assignment1_Part {
   internal class Program {
      static void Main(string[] args) {
         using var app = Application.Launch(@"C:\Program Files\TRUMPF\TMM\Praxis\Bin\Prax.exe");
         using var automation = new UIA3Automation();
         var partImport = new PartImport(app, automation);
         partImport.ImportParts();
         var material = new AssignMaterial(app, automation);
         material.Assign("00.14", "ST0M0200");
         material.Assign("00.23", "AL0M0200");
         material.Assign("00.26", "SS0M0200");
         var job = new JobCreation(app, automation);
         job.CreateJob(new [] {"00.14","00.23"});
         var plan = new PlanJob(app, automation);
         plan.Plan();
         var layout = new LayoutProcess(app, automation);
         layout.SendToMachine();
      }
   }
}