using System;
using System.Management;
using System.Threading;
using System.Collections.Generic;

namespace PrinterActivityLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Monitoring print jobs on your local machine...");

            try
            {
                // Monitor print job creation using WMI EventWatcher with status
                Console.WriteLine("Monitoring print requests...");
                WqlEventQuery eventQuery = new WqlEventQuery("SELECT * FROM __InstanceCreationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_PrintJob'");
                ManagementEventWatcher watcher = new ManagementEventWatcher(eventQuery);

                Dictionary<string, string> jobStatuses = new Dictionary<string, string>();  // To track jobStatus over time

                watcher.EventArrived += new EventArrivedEventHandler((sender, eventArgs) =>
                {
                    ManagementBaseObject printJob = (ManagementBaseObject)eventArgs.NewEvent["TargetInstance"];
                    string documentName = printJob["Document"].ToString();
                    string jobStatus = printJob["JobStatus"].ToString();  // Print job status
                    string jobId = printJob["JobId"].ToString();  // Unique identifier for tracking print jobs

                    // Track job status change over time
                    if (!jobStatuses.ContainsKey(jobId))
                    {
                        jobStatuses[jobId] = jobStatus;
                    }
                    else
                    {
                        string previousStatus = jobStatuses[jobId];

                        if (previousStatus != jobStatus)
                        {
                            Console.WriteLine($"Print job '{documentName}' changed from {previousStatus} to {jobStatus}");
                        }

                        jobStatuses[jobId] = jobStatus;  // Update status for this jobId
                    }

                    Console.WriteLine($"Print Request: {documentName}, Status: {jobStatus}");

                    // Determine if job is pending, printing, completed, or failed
                    if (jobStatus == "3")
                    {
                        Console.WriteLine($"Print job '{documentName}' is Queuing...");
                    }
                    else if (jobStatus == "7")
                    {
                        Console.WriteLine($"Print job '{documentName}' is Printing...");
                    }
                    else if (jobStatus == "8")
                    {
                        Console.WriteLine($"Print job '{documentName}' is Completed.");
                    }
                    else if (jobStatus == "9")
                    {
                        Console.WriteLine($"Print job '{documentName}' has Failed.");
                    }
                    else
                    {
                        Console.WriteLine($"Print job '{documentName}' status is unknown.");
                    }
                });

                watcher.Start();

                // Keep the application running to continue monitoring
                while (true)
                {
                    Thread.Sleep(5000);  // Poll every 5 seconds to keep monitoring
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
