using System;
using System.Threading;

namespace DAN_XXXIX_Kristina_Garcia_Francisco
{
    /// <summary>
    /// Class that handles advertisements that happen during a song
    /// </summary>
    class Advertisement
    {
        #region Properties
        public string Name { get; set; }
        private Random rng = new Random();
        #endregion

        #region Constructor
        public Advertisement()
        {

        }

        public Advertisement(string name)
        {
            Name = name;
        }
        #endregion

        /// <summary>
        /// Starts the advertisements in a song
        /// </summary>
        /// <param name="time">total time of a song</param>
        /// <param name="sleepTime">how often to repeat an advertisement</param>
        public void StartAdvertisement(int time, int sleepTime)
        {
            // Wait the song to start first
            Program.waitAdv.WaitOne();
            int i = 0;

            // First advertisement starts one sleepTime later after the song started
            // Stop the advertisement one sleepTime before time expires
            while (time > sleepTime)
            {
                // If a song has been canceled, cancel the advertisement too
                if (AudioPlayer.canceled == true)
                {
                    // Write the last message before the audio player closes to avoid lingering advertisements
                    Program.waitAdv.Set();
                    break;
                }

                Thread.Sleep(sleepTime);
                time = time - sleepTime;
                i = rng.Next(0, 5);
                Console.WriteLine("\t\t\t\t{0}", Program.allAds[i].Name);
            }

            // Write the last message before the song finishes to avoid lingering advertisements
            Program.waitAdv.Set();
        }    
    }
}
