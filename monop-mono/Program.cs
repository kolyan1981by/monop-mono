using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLogic;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Reflection;

namespace Monop.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

      

        private static void Run(bool isXml = false)
        {
          	var g = GameHelper.CreateNew(2, 30, "en-US", 1, false);


			Simulator.AddPlayers(g, 2, new[] { "kolyan", "bot1" });
			Simulator.InitWithData(g, Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
			g.StartGame();

			while (!g.IsFinished)
            {
                Thread.Sleep(300);

                System.Console.WriteLine("r{0} {1} {2}", g.RoundNumber, g.Curr.Id, g.Curr.Money);

            }

            ShowInfo(g);

        }

        private static void ShowInfo(Game g)
        {
            var res = Simulator.GetResult(g);

            string filename = Path.Combine(Path.GetTempPath(), "log.htm");
            File.WriteAllText(filename, DateTime.Now + "<br />");
            //show log
            File.AppendAllText(filename, "winner=" + res[0] + "<br />");
            File.AppendAllText(filename, res[1] + "<br />");
            File.AppendAllText(filename, res[2] + "<br />");

            File.AppendAllLines(filename, Simulator.GetLog(g, 30).Select(x => x + "<br />"));

            Process.Start(filename);
        }

       
    }
}
