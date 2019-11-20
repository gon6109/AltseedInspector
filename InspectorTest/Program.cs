using System;

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
            window.Inspector.AddProperty(new AltseedInspector.Property("Test", new TestModel()));
            window.Show();

            while (!isExit && asd.Engine.DoEvents())
            {
                asd.Engine.Update();
            }

            asd.Engine.Terminate();
        }
    }
}
