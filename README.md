# PrinterActivityLogger

PrinterActivityLogger is a simple .NET console application that uses Windows Management Instrumentation (WMI) to monitor print jobs on your local machine in real time. When run, it detects new print jobs, tracks their status changes, and prints informative messages to the console.

## Features
- Detects creation of print jobs via WMI  
- Tracks job statuses (Queuing, Printing, Completed, Failed)  
- Logs status changes and current state to the console  

## Requirements
- Windows OS  
- .NET Framework 4.x or later (or .NET Core/5+)  
- Administrator privileges (required for WMI queries)  

## Installation
1. Clone the repository or download the source code:  
   ```bash
   git clone https://github.com/yourusername/PrinterActivityLogger.git

Build the project using Visual Studio or the dotnet CLI:
```bash
cd PrinterActivityLogger

dotnet build
 ```

Run the application after building:
```bash
dotnet run --project PrinterActivityLogger
 ```

You will see:
```bash
Monitoring print jobs on your local machine...
Monitoring print requests...
Print Request: Document.pdf, Status: 3
Print job 'Document.pdf' is Queuing...
Print job 'Document.pdf' changed from 3 to 7
Print job 'Document.pdf' is Printing...
Print job 'Document.pdf' changed from 7 to 8
Print job 'Document.pdf' is Completed.
 ```

________________________________________________________________________________________________________________________

# :incoming_envelope: Contact Information :incoming_envelope:

For any questions or further information, please don't hesitate to contact me :pray:

Email: merttopcu.dev@gmail.com

LinkedIn: https://www.linkedin.com/in/mert-topcu/

Happy Coding ❤️
