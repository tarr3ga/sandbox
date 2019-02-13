using System;
using System.Diagnostics;
using System.Windows.Automation;


namespace GetChromeTabs
{
    class Program
    {
        static void Main(string[] args)
        {
            Process[] procsChrome = Process.GetProcessesByName("chrome");

            if(procsChrome.Length <= 0)
            {
                Console.WriteLine("Chrome is not running");
            }
            else
            {
                foreach(Process proc in procsChrome)
                {
                    //Console.WriteLine(proc.Id.ToString());

                    if (proc.MainWindowHandle == IntPtr.Zero)
                    {
                        continue;
                    }

                    AutomationElement root = AutomationElement.FromHandle(proc.MainWindowHandle);

                    Condition condNewTab = new PropertyCondition(AutomationElement.NameProperty, "Address and search bar");

                    AutomationElement elmNewTab = root.FindFirst(TreeScope.Descendants, condNewTab);

                    TreeWalker treeWalker = TreeWalker.ControlViewWalker;

                    AutomationElement elmTabStrip = treeWalker.GetParent(elmNewTab);

                    Condition condTabItem = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem);

                    foreach (AutomationElement tabitem in elmTabStrip.FindAll(TreeScope.Children, condTabItem))
                    {
                        Console.WriteLine(tabitem.Current.Name);
                    }
                }
            }

            Console.Read();
        }
    }
}
