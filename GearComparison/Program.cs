using System;
using System.Windows.Forms;
using Olf.NetToolkit;

namespace GearComparison
{
   static class Program
   {
      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);

         /* OLFToolkitAttach toolKitConnection = new OLFToolkitAttach();
         toolKitConnection.ServerMode = true;
         toolKitConnection.Attach("GearComparison", 0); */

         Application.Run(new MainWin());
      }
   }
}
