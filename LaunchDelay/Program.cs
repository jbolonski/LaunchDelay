using McMaster.Extensions.CommandLineUtils;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LaunchDelay
{
    [Command(Name = "LaunchDelay", Description = "This will delay the launch of an application by time or based on another process to be started.")]
    [HelpOption("-?")]
    class Program
    {
        [Argument(0, Description = "Application to Launch")]
        private string Application { get; } = "";

        [Option("-d|--delay <DELAY>", Description = "Delay in seconds")]
        private int DelayTime { get; } = 0;

        [Option("-p|--process <PROCESS_NAME>", Description = "Process to wait for")]
        private string ProcessTrigger { get; } = "";

        [Option("-t|--timeout <PROCESS_WAIT_TIMEOUT>", Description = "Timeout if process doesn't launch after this in seconds")]
        private int ProcessTimeOut { get; } = 1800;

        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private void OnExecute()
        {
            if (Application != "")
            {
                DelayForSeconds(DelayTime);
                if (DelayForProcess(ProcessTrigger, ProcessTimeOut))
                {
                    Process.Start(Application);
                }
            } else
            {
                Console.WriteLine("ERROR: Can't Launch. No Application specified.");
            }
        }

        private static void DelayForSeconds(int delayTimeInSeconds)
        {
            var d = Task.Run(async delegate { await Task.Delay(delayTimeInSeconds * 1000); });
            d.Wait();            
        }

        private static bool DelayForProcess(string processNameToWaitFor, int ProcessTimeOut)
        {
            var expireTime = DateTime.Now.AddSeconds(ProcessTimeOut);
            bool matched = false;
            if (processNameToWaitFor != "")
            {                
                while (!matched && DateTime.Now<expireTime )
                {
                    Process[] matchedProcesses = Process.GetProcessesByName(processNameToWaitFor);
                    if (matchedProcesses.Length>0)
                    {
                        matched = true;
                    }

                    DelayForSeconds(2);
                }
            } else
            {
                matched = true;
            }

            return matched;
        }

    }
}
