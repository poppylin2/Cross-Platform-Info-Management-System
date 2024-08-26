using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// All Data Repositories
    /// </summary>
    public class SoftResources
    {
        /// <summary>
        /// All string Repositories
        /// </summary>
        public class StringResouce
        {
            public static string SoftName { get; } = "Welcome to InfoHubX";
            public static string SoftCopyRight { get; } = "©️Poppy";



            public const string AccountLoadFailed = "Failed to add account";
            public const string AccountDeleteSuccess = "Account deleted:";
            public const string AccountAddSuccess = "Account added:";
            public const string AccountModifyPassword = "Account password changed:";
            public const string AccountUploadPortrait = "Account portrait updated:";

        }
    }
}
