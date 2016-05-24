using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Net;
using System.Net.Mail;

namespace RSS2Email
{
    internal class Sender
    {
        private const string URL = "https://lenta.ru/rss/news";

        static DateTime date = DateTime.MinValue;
        public void SendData()
        {
            string title;
            string description;
            XElement xelem = XElement.Load(URL);
            IEnumerable<string> dates = xelem.Element("channel").Elements("item").Select(x => x.Element("pubDate").Value);

            if (date == DateTime.MinValue)
            {
                date = DateTime.Parse(dates.FirstOrDefault());
                title = xelem.Element("channel").Elements("item").FirstOrDefault().Element("title").Value;
                description = xelem.Element("channel").Elements("item").FirstOrDefault().Element("description").Value;
                SendEmail(title, description);
            }
            else
            {
                IEnumerable<XElement> resultElem = xelem.Element("channel").Elements("item").Where(x => StringToDate(x.Element("pubDate").Value) > date);
                foreach (XElement elem in resultElem)
                {
                    title = elem.Element("title").Value;
                    description = elem.Element("description").Value;
                    SendEmail(title, description);
                }
                date = DateTime.Parse(xelem.Element("channel").Elements("item").FirstOrDefault().Element("pubDate").Value);
            }
        }
        private void SendEmail(string title, string description)
        {
            SmtpClient Smtp = new SmtpClient("smtp.mail.ru", 587);
            Smtp.Credentials = new NetworkCredential("rss2emailnsu@mail.ru", "nsudotnet2016");
            Smtp.EnableSsl = true;

            MailMessage Message = new MailMessage();
            Message.From = new MailAddress("rss2emailnsu@mail.ru");
            Message.To.Add(new MailAddress("ffloxee@mail.ru"));
            Message.Subject = title;
            Message.Body = description;

            Smtp.Send(Message);
            Message.Dispose();
            Smtp.Dispose();
        }
        private DateTime StringToDate(string dateString)
        {
            return DateTime.Parse(dateString);
        }
        private bool Filter(XElement x)
        {
            Console.WriteLine(StringToDate(x.Element("pubDate").Value) > date);
            Console.WriteLine(x.Element("pubDate").Value);
            return StringToDate(x.Element("pubDate").Value) > date;
        }

    }
}
