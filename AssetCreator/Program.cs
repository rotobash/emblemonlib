#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
#if MONOMAC
using MonoMac.AppKit;
using MonoMac.Foundation;
#elif __IOS__ || __TVOS__
using Foundation;
using UIKit;
#endif
#endregion

using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssetCreator
{
#if __IOS__ || __TVOS__
    [Register("AppDelegate")]
    class Program : UIApplicationDelegate
#else
    static class Program
#endif
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
#if !MONOMAC && !__IOS__ && !__TVOS__
        [STAThread]
#endif
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CreatorSelect());
        }

#if __IOS__ || __TVOS__
        public override void FinishedLaunching(UIApplication app)
        {
            RunGame();
        }
#endif
    }

#if MONOMAC
    class AppDelegate : NSApplicationDelegate
    {
        public override void FinishedLaunching (MonoMac.Foundation.NSObject notification)
        {
            AppDomain.CurrentDomain.AssemblyResolve += (object sender, ResolveEventArgs a) =>  {
                if (a.Name.StartsWith("MonoMac")) {
                    return typeof(MonoMac.AppKit.AppKitFramework).Assembly;
                }
                return null;
            };
            Program.RunGame();
        }

        public override bool ApplicationShouldTerminateAfterLastWindowClosed (NSApplication sender)
        {
            return true;
        }
    }  
#endif
}
