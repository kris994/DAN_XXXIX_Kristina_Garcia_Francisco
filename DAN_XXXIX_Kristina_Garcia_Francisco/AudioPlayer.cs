using System;
using System.Threading;

namespace DAN_XXXIX_Kristina_Garcia_Francisco
{
    class AudioPlayer
    {
        /// <summary>
        /// Delegate used to send notifications for when audio player stoped playing.
        /// </summary>
        public delegate void Notification();
        /// <summary>
        /// Event that gets triggered when user closed the audio player
        /// </summary>
        public event Notification OnNotification;
        public static bool playing = true;

        /// <summary>
        /// Checks if there is any goiven value to trigger the event
        /// </summary>
        internal void Notify()
        {
            if (OnNotification != null)
            {
                OnNotification();
            }
        }

        public void AutioPlayerActivity()
        {
            Song s = new Song();
            s = s.ReadSong();
            if (s == null)
            {
                return;
            }

            PlaySong(s);
            StartAgain();
        }

        public void PlaySong(Song s)
        {
            Advertisement adv = new Advertisement();
            // Get duration to convert to milliseconds
            string duration = s.Duration;
            string[] time = duration.Split(':');
            int hours = 0;
            int minutes = 0;
            int seconds = 0;
            foreach (var item in time)
            {
                hours = int.Parse(time[0]);
                minutes = int.Parse(time[1]);
                seconds = int.Parse(time[2]);
            }

            int milliseconds = seconds * 1000 + minutes * 60000 + hours * 3600000;

            Thread playAdv = new Thread(() => adv.StartAdvertisement(milliseconds, 200));
            playAdv.Start();

            Program.waitAdv.Set();
            while (milliseconds > 0)
            {           
                Thread.Sleep(1000);
                milliseconds = milliseconds - 1000;
                if (milliseconds > 999)
                {
                    Console.WriteLine("Song is still ongoing for: {0} seconds.", milliseconds / 1000);
                }
            }

            Console.WriteLine("Song finished.");
        }

        public void StartAgain()
        {
            Validation val = new Validation();
            Song s = new Song();

            Console.Write("\nWould you like to play another song? (Yes/No): ");            
            string answer = val.YesNo();
            Console.WriteLine();

            if (answer == "yes")
            {
                Thread player = new Thread(AutioPlayerActivity);
                player.Start();
                player.Join();
            }
            else
            {
                OnNotification = s.SongStopped;
            }
        }
    }
}
