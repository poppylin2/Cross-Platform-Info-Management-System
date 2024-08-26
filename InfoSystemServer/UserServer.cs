using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//=========================================================================================
//
// Template description: Uses the BasicFramework and network framework to implement a server template, including basic creation operations.
//
//=========================================================================================



namespace 软件系统服务端模版
{
    /// <summary>
    /// Server class that stores static parameters for system operation.
    /// </summary>
    public class UserServer
    {
        /// <summary>
        /// Version number, announcement, whether login is allowed, and login restriction storage
        /// </summary>
        public static ServerSettings ServerSettings { get; set; } = new ServerSettings();

        /// <summary>
        /// Storage object for all account information. The specific account class can be extended based on UserAccount.
        /// </summary>
        public static ServerAccounts<UserAccount> ServerAccounts { get; set; } = new ServerAccounts<UserAccount>(
            new List<UserAccount>()
            {
                // eg: add a default super admin
                new UserAccount()
                {
                    UserName="admin",
                    Password="123456",
                    Factory="Headquarters",
                    RegisterTime=DateTime.Now,
                    LastLoginTime=DateTime.Now,
                    LoginEnable=true,
                    Grade=AccountGrade.SuperAdministrator,
                    ForbidMessage="This account has been disabled",
                    LoginFrequency=0,
                    LastLoginIpAddress="",
                }
            }
        );

        /// <summary>
        /// Role info management
        /// </summary>
        public static RoleAssign ServerRoles { get; set; } = new RoleAssign();

    }

    /// <summary>
    /// An extended user account example, which can replace the server and client account classes.
    /// It can also be directly extended in the source code.
    /// </summary>
    public class UserAccountEx : UserAccount
    {
        /// <summary>
        /// Age
        /// </summary>
        public int Age { get; set; } = 0;
        /// <summary>
        /// Home Address
        /// </summary>
        public string HomeAddress { get; set; } = "";
    }
}
