using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace UwpConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string content, title;
            if (args.Length == 2)
            {
                title = args[0];
                content = args[1];
            }
            else
            {
                Console.Write("\nPlease enter the content for you notification: ");
                content = Console.ReadLine();
                Console.Write("Please enter the title for your notification: ");
                title = Console.ReadLine();
            }

            Console.Write("\nSending notification...");

            ToastNotifier notifier = ToastNotificationManager.CreateToastNotifier();
            XmlDocument toastXDoc = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            XmlNodeList toastNodes = toastXDoc.GetElementsByTagName("text");
            toastNodes.Item(0).AppendChild(toastXDoc.CreateTextNode(title));
            toastNodes.Item(1).AppendChild(toastXDoc.CreateTextNode(content));
            IXmlNode toastNode = toastXDoc.SelectSingleNode("/toast");
            XmlElement audioElem = toastXDoc.CreateElement("audio");
            audioElem.SetAttribute("src", "ms-winsoundevent:Notification.Reminder");

            ToastNotification toast = new ToastNotification(toastXDoc);
            notifier.Show(toast);

            Console.WriteLine("Notification sent!");
            Console.ReadKey();
        }
    }
}
