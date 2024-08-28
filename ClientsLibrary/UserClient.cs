using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HslCommunication.Enthernet;
using HslCommunication.BasicFramework;
using CommonLibrary;
using System.Net;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace ClientsLibrary
{


    /***********************************************************************************
     * 
     * Description: Used to store global client variable data, so data can be accessed 
     *              from any interface.
     *              The data placed here is specifically for shared access between 
     *              WinForm and WPF.
     * 
     * 
     ***********************************************************************************/


    /// <summary>
    /// A generic user client class that contains some static resources.
    /// </summary>
    public class UserClient
    {
        /// <summary>
        /// Log information that the client needs to store locally.
        /// </summary>
        public static JsonSettings JsonSettings = new JsonSettings();


        /// <summary>
        /// The current version of the software, used as the key basis for validating updates.
        /// </summary>
        public static SystemVersion CurrentVersion { get; } = new SystemVersion("1.0.0.180429");


        /// <summary>
        ///Server IP address, default is 127.0.0.1, for local debugging.
        /// Cloud server address: 117.48.203.204. Note: The cloud version is the latest;
        /// debugging with an outdated client version will fail.
        /// </summary>
        public static string ServerIp { get; } = "127.0.0.1"; // test the IP address of the cloud server


        /// <summary>
        /// Information about the system's branches.
        /// </summary>
        public static List<string> SystemFactories { get; set; } = new List<string>();




        /// <summary>
        /// An object for all version update information.
        /// </summary>
        public static List<VersionInfo> HistoryVersions { get; } = new List<VersionInfo>
        {
                // Write in all the historical version information, so it can be viewed in the update log interface.

                new VersionInfo()
                {
                    VersionNum = new SystemVersion("1.0.0"),
                    ReleaseDate = new DateTime(2018, 5, 1), // release date
                    UpdateDetails = new StringBuilder(
                        "1. The first version of the system is officially released."+Environment.NewLine+
                        "2. Provides multi-client simultaneous online functionality."+Environment.NewLine+
                        "3. Supports personal file management features. "),
                },
        };


        /// <summary>
        /// Set or get the system's announcement.
        /// </summary>
        public static string Announcement { get; set; } = "";
        /// <summary>
        /// The currently logged-in user account of the system.
        /// </summary>
        public static UserAccount UserAccount { get; set; } = new UserAccount();

        /// <summary>
        /// The server time, synchronized with the server every 10 seconds, 
        /// preventing clients from changing the local time and can be used for various time condition judgments.
        /// </summary>
        public static DateTime DateTimeServer { get; set; } = DateTime.Now;


        /// <summary>
        /// A network object class used for accessing server data; you must change this port parameter, otherwise, it will fail to run.
        /// </summary>
        public static NetSimplifyClient Net_simplify_client { get; set; } = new NetSimplifyClient(ServerIp, UserSystem.Port_Second_Net)
        {
            Token = UserSystem.KeyToken,
            ConnectTimeOut = 5000,
        };

        /// <summary>
        /// An object used to send instant, discardable data to the server using UDP.
        /// </summary>
        public static NetUdpClient Net_Udp_Client { get; set; } = new NetUdpClient(ServerIp, UserSystem.Port_Udp_Server)
        {
            Token = UserSystem.KeyToken,
        };


        /// <summary>
        /// Checks whether the current account has the role permissions.
        /// </summary>
        /// <param name="roleCode">角色名称</param>
        /// <returns></returns>
        public static bool CheckUserAccountRole(string roleCode)
        {
            JObject json = new JObject
            {
                { "Name", UserAccount.UserName },
                { "Role", roleCode }
            };
            HslCommunication.OperateResult<string> result = Net_simplify_client.ReadFromServer(CommonHeadCode.SimplifyHeadCode.检查角色权限,
                json.ToString());

            if(result.IsSuccess)
            {
                if(result.Content.ToUpper() == "TRUE")
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Avatar manager
        /// </summary>
        public static UserPortrait PortraitManager { get; set; }



        /// <summary>
        /// file upload and download operations 
        /// </summary>
        public static IntegrationFileClient Net_File_Client { get; set; } = new IntegrationFileClient()
        {
            Token = UserSystem.KeyToken,
            LogNet = LogNet,
            ServerIpEndPoint = new IPEndPoint(IPAddress.Parse(ServerIp), UserSystem.Port_Ultimate_File_Server)
        };

        /// <summary>
        /// Currently only used for uploading client update files.
        /// </summary>
        public static IntegrationFileClient Net_Update_Client { get; set; } = new IntegrationFileClient()
        {
            Token = UserSystem.KeyToken,
            LogNet = LogNet,
            ServerIpEndPoint = new IPEndPoint(IPAddress.Parse(ServerIp), UserSystem.Port_Advanced_File_Server)
        };


        /// <summary>
        /// Client's log recording object.
        /// </summary>
        public static HslCommunication.LogNet.ILogNet LogNet { get; set; }




        /// <summary>
        /// Used to handle unhandled exceptions on the client side and send them to the server for better error tracking via network components.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                // Send back to the server using the TCP method
                string info = HslCommunication.LogNet.LogNetManagment.GetSaveStringFromException(null, ex);
                Net_simplify_client.ReadFromServer(CommonHeadCode.SimplifyHeadCode.异常消息, info);
            }
        }
        

    }
}
