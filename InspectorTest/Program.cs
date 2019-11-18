using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectorTest
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            asd.Engine.Initialize("Inspector Test", 640, 480, new asd.EngineOption());

            var window = new MainWindow();
            bool isExit = false;
            window.Closed += (object sender, EventArgs e) =>
            {
                isExit = true;
            };

            window.Show();

            while (!isExit && asd.Engine.DoEvents())
            {
                asd.Engine.Update();
            }

            asd.Engine.Terminate();
        }
    }
}
