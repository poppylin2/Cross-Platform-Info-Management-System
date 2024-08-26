
# InfoHubX 
[![Visual Studio](https://img.shields.io/badge/Visual%20Studio-2022-red.svg)](https://www.visualstudio.com/zh-hans/) [![Language](https://img.shields.io/badge/Language-C%23%207.0-orange.svg)](https://blogs.msdn.microsoft.com/dotnet/2016/08/24/whats-new-in-csharp-7-0/) [![License](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/dathlin/ClientServerProject/blob/master/LICENSE)
## 🚀 Summary

InfoHubX is an advanced Client-Server development framework designed to streamline the creation of small-to-medium sized info systems. The framework is engineered to support both desktop (computer-side) and web-based (web-side) full-platform templates, offering flexibility and scalability across different environments. InfoHubX includes a comprehensive suite of reusable components and function codes to handle network communication, client version control, account management, password modifications, announcement management, and other common system tasks.

InfoHubX is particularly powerful due to its three client-side modes:

- WinForm Client: A robust traditional desktop client.
- WPF Client: A modern desktop client offering enhanced UI and interaction capabilities.
- ASP.NET MVC Client: A browser-based option providing access to system functionalities without requiring full client installation.
  
You can choose either the WinForm or WPF mode for the desktop clients, depending on your system's needs.
For certain functions (e.g., report viewing), you can integrate with the ASP.NET MVC client, allowing users to access reports via a web browser.
If the server is hosted in the cloud, this allows for anytime, anywhere interaction, ensuring that data flow is centralized and all account models are unified across platforms. This architecture allows for seamless interaction between cloud-based servers and client devices, enabling anytime, anywhere access to centralized data and unified account models. With InfoHubX, system administrators can offer powerful desktop features while providing lightweight web access for users who only need to view reports or perform basic tasks.

## 🧷 Core Functionalities
- Network Communication: It includes built-in network communication mechanisms that streamline client-server interactions.
- Client Management: Provides client version control, ensuring that the client-side software is kept up to date.
- Account Control: Simplified account management, including features like password modification and rights management for small and medium-sized systems.
- Server Configuration & Announcement Management: Facilitates server-side configuration and system-wide announcements.
- UI Components: Contains templates for common windows and interfaces across different platforms.

## 🦖 Features
<ul> 
<li>A simple account management feature that includes account registration, password modification, client login records, account logout, and accounts containing basic information.</li> 
<li>A simple client login control feature that allows manual control over which clients are permitted to log in, easily configured via a window.</li> 
<li>A simple announcement management feature that allows authorized accounts to modify announcements. Future versions will support announcement change records.</li> 
<li>A simple feedback feature that allows clients to provide feedback on software issues or bugs, facilitating easier fixes for developers.</li> <li>A simple tray notification feature that allows free control over message pop-ups during announcement changes or new messages.</li>
<li>A simple version log prompt window that automatically displays when a new version is available.</li> 
<li>A simple role manager feature that allows configuring an arbitrary number of account names for each role.</li> 
<li>Server configuration is saved in real time to prevent data loss due to sudden server shutdowns or power outages.</li> 
<li>A robust network communication framework that includes one-to-many controlled TCP networking (for server-client control and easy data broadcasting), synchronous data request networking, and UDP networking.</li> 
<li>A complete auto-update deployment mechanism, where all clients will automatically update with one click after the server deploys a new version.</li> 
<li>Clients provide developers with the ability to remotely update the server programs, making developer operations more convenient.</li> 
<li>Comprehensive logging functionality, with all network and file operations logged. All client exceptions will be sent to the server for logging. Clients can also easily view all logs, and you can easily add other information to the logs.</li> 
<li>A simple LAN chat feature for chatting among all online accounts, with some message caching.</li> 
<li>A file-sharing platform, as most software systems share certain specific files, allowing easy downloading, management, and uploading.</li> 
<li>Support for avatars for all accounts, with future support for multi-account synchronization.</li> 
<li>A simple development center allowing clients to monitor the server program's memory usage in real time.</li> 
<li>Clients provide a unified configuration center for setting various server parameters.</li>
<li>The WPF client version additionally provides a theme color setting feature.</li> 
</ul>

## 🎍 Environment
<ul>
<li>IDE: Visual Studio 2017
<ul>
<li>winform server：.NET Framework 3.5</li>
<li>winform client：.NET Framework 3.5</li>
<li>wpf client： .NET Framework 4.5</li>
<li>asp.net mvc server：.NET Framework 4.5</li>
</ul>
</li>
</ul>

## 🚗 Getting Started
<ul> <li>Rebuild the <b>CommonLibrary</b> project.</li> <li>Ensure that the <b>ServerIp</b> property of the <b>UserClient</b> class in the <b>UserClient.cs</b> file of the <b>ClientsLibrary</b> project is set to 127.0.0.1. If not, modify it.</li> <li>Rebuild the <b>ClientsLibrary</b> project.</li> <li>Rebuild and run the <b>ServerTemplate</b> executable.</li> <li>Select the client version you want to debug, for example, start the <b>InfoSystemClientWinform</b> project for WinForms.</li> <li>Log in using the default account: admin, password: 123456.</li> <li>You can now experience all the features.</li> </ul> 

## 🦄 Secondary Development
Based on this template, secondary development can be easily carried out. Some examples are as follows (feel free to contribute):
<ul> <li>A SCADA system for real-time monitoring and control, easily enabling one-to-many synchronous monitoring.</li> <li>A project management system for department personnel.</li> <li>An equipment management system for managing equipment records and archives.</li> <li>An ERP system for managing spare parts and inventory.</li> <li>Systems that require complex data interaction between multiple clients.</li> 
</ul> 
......and more！<br>
<br>
NOTE： When conducting secondary development, special attention should be given to modifying parameters in the `UserSystem.cs` file in the **CommonLibrary** project according to actual needs.


## 🤖 Disclaimer
<ul> <li>Uses the <a href="http://www.newtonsoft.com/json">json.net component</a>.</li> <li>The WPF template uses the open-source project <a href="https://github.com/ButchersBoy/MaterialDesignInXamlToolkit">Material Design in XAML Toolkit</a>.</li> <li>File-sharing functionality icons are from <a href="http://fileicons.chromefans.org/">free file icons</a>.</li> </ul> <pre>

#### System Login Design
<ol> <li>Status check: Detect the server's maintenance status. If the server is under maintenance, the reason for system login being unavailable is displayed.</li> <li>Account check: The server performs a thorough check on the login account, verifying whether the username exists, whether the password is correct, whether login is allowed, and recording login IP, time, and frequency.</li> <li>Version check: The server returns the latest version number. The client then decides whether to initiate an update based on its own needs.</li> <li>Parameter download: After passing the above checks, initialization data (e.g., announcement data) is sent to the client. You can also add your own data. The data is encapsulated in JSON, and the client can parse it according to the example.</li> <li>Once all checks pass, the client’s main interface program is launched. If any check or parameter download fails, login is denied, and the relevant error message is displayed.</li> </ol>

