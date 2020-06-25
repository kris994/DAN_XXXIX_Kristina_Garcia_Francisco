using System;
using System.Threading;

namespace DAN_XXXIX_Kristina_Garcia_Francisco
{
    /// <summary>
    /// The audio player class that controls how the player works
    /// </summary>
    class AudioPlayer
    {
        /// <summary>
        /// Delegate used to send notifications when audio player stoped playing.
        /// </summary>
        public delegate void Notification();
        /// <summary>
        /// Event that gets triggered when user closed the audio player
        /// </summary>
        public event Notification OnNotification;
        /// <summary>
        /// Checks if the song has been canceled
        /// </summary>
        public static bool canceled = false;

        /// <summary>
        /// Checks if there is any given value to trigger the event
        /// </summary>
        internal void Notify()
        {
            if (OnNotification != null)
            {
                OnNotification();
            }
        }

        /// <summary>
        /// Follows all audio player activities that a thread needs to do
        /// </summary>
        public void AutioPlayerActivity()
        {           
            Song s = new Song();
            s = s.ReadSong();

            // If a song was not chosen, return to main menu
            if (s == null)
            {
                return;
            }
            // Set canceled to false and start a song
            canceled = false;
            PlaySong(s);            
        }

        /// <summary>
        /// Plays the chosen song
        /// </summary>
        /// <param name="s">the song that user has chosen</param>
        public void PlaySong(Song s)
        {
            Advertisement adv = new Advertisement();

            // Get song duration to convert it to milliseconds
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

            // Create the advertisement thread
            Thread playAdv = new Thread(() => adv.StartAdvertisement(milliseconds, 200));
            playAdv.Start();

            // Start the thread right before the song starts playing
            Program.waitAdv.Set();
            while (milliseconds > 0)
            {
                // Check if a key was pressed, if yes stop the song from playing
                if (Console.KeyAvailable)
                {
                    canceled = true;
                    break;
                }

                Thread.Sleep(1000);
                milliseconds = milliseconds - 1000;
                if (milliseconds > 999)
                {
                    Console.WriteLine("Song is still ongoing for: {0} seconds.", milliseconds / 1000);
                }
            }

            // If the song was not canceled, ask the user if he wants to continue
            if (canceled == false)
            {
                Program.waitAdv.WaitOne();
                Console.WriteLine("Song finished.");
                StartAgain();
            }
            else
            {
                // Wait for the advertisement to finish to avoid lingering text
                Program.waitAdv.WaitOne();
                // Notify about the player stopping
                OnNotification = s.SongStopped;
            }                    
        }

        /// <summary>
        /// Restart a song after the last one finished
        /// </summary>
        public void StartAgain()
        {
            Validation val = new Validation();
            Song s = new Song();

            Console.Write("\nWould you like to play another song? (Yes/No): ");            
            string answer = val.YesNo();
            Console.WriteLine();

            if (answer == "yes")
            {
                Console.Clear();
                Thread player = new Thread(AutioPlayerActivity);              
                player.Start();
                player.Join();
            }
            // Close the audio player if user selected No
            else
            {
                OnNotification = s.SongStopped;
            }
        }
    }
}
