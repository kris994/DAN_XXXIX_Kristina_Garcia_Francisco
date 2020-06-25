using System;
using System.Threading;

namespace DAN_XXXIX_Kristina_Garcia_Francisco
{
    class Advertisement
    {
        public string Name { get; set; }
        private Random rng = new Random();

        public Advertisement()
        {

        }

        public Advertisement(string name)
        {
            Name = name;
        }

        public void StartAdvertisement(int time, int sleepTime)
        {
            Program.waitAdv.WaitOne();
            int i = 0;

            // Because it starts 200 sec later
            while (time > sleepTime)
            {
                if (AudioPlayer.playing == false)
                {
                    Program.waitAdv.Set();
                    break;
                }

                Thread.Sleep(sleepTime);
                time = time - sleepTime;
                i = rng.Next(0, 5);
                Console.WriteLine("\t\t\t\t{0}", Program.allAds[i].Name);
            }

            Program.waitAdv.Set();
        }    
    }
}
