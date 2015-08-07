using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Chapter13Example02
{
    public delegate void MessageHaddler(string messageText);

    class Connection
    {
        public event MessageHaddler MessageArrived;
        private Timer pollTimer;

        public Connection()
        {
            pollTimer = new Timer(100);
            pollTimer.Elapsed += new ElapsedEventHandler(CheckForMessage);
        }

        public void Connect()
        {
            pollTimer.Start();
        }

        public void Disconnect()
        {
            pollTimer.Stop();
        }

        private static Random random = new Random();

        private void CheckForMessage(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Checking for new massages.");
            if ((random.Next(9) == 0) && (MessageArrived != null))
            {
                MessageArrived("Hello Mum!");
            }
        }
    }
}
