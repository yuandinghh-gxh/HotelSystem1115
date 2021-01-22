using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.Threading;
using System.IO;

namespace HotelSystem1115
{
    class Soundpaly
    {
        public static void Sound(string s)
        {
            if (s == "0")
            {
                SoundPlayer sp = new SoundPlayer("sound/0.wav");
                try
                {
                    sp.Play();
                }
                catch (FileNotFoundException ex)
                {
                    throw ex;
                }
            }
            else if (s == "1")
            {
                SoundPlayer sp = new SoundPlayer("sound/1.wav");
                try
                {
                    sp.Play();
                }
                catch (FileNotFoundException ex)
                {
                    throw ex;
                }
            }
            else if (s == "2")
            {
                SoundPlayer sp = new SoundPlayer("sound/2.wav");
                try
                {
                    sp.Play();
                }
                catch (FileNotFoundException ex)
                {
                    throw ex;
                }
            }
            else if (s == "3")
            {
                SoundPlayer sp = new SoundPlayer("sound/3.wav");
                try
                {
                    sp.Play();
                }
                catch (FileNotFoundException ex)
                {
                    throw ex;
                }
            }
            else if (s == "4")
            {
                SoundPlayer sp = new SoundPlayer("sound/4.wav");
                try
                {
                    sp.Play();
                }
                catch (FileNotFoundException ex)
                {
                    throw ex;
                }
            }
            else if (s == "5")
            {
                SoundPlayer sp = new SoundPlayer("sound/5.wav");
                try
                {
                    sp.Play();
                }
                catch (FileNotFoundException ex)
                {
                    throw ex;
                }
            }
            else if (s == "6")
            {
                SoundPlayer sp = new SoundPlayer("sound/6.wav");
                try
                {
                    sp.Play();
                }
                catch (FileNotFoundException ex)
                {
                    throw ex;
                }
            }
            else if (s == "7")
            {
                SoundPlayer sp = new SoundPlayer("sound/7.wav");
                try
                {
                    sp.Play();
                }
                catch (FileNotFoundException ex)
                {
                    throw ex;
                }
            }
            else if (s == "8")
            {
                SoundPlayer sp = new SoundPlayer("sound/8.wav");
                try
                {
                    sp.Play();
                }
                catch (FileNotFoundException ex)
                {
                    throw ex;
                }
            }
            else if (s == "9")
            {
                SoundPlayer sp = new SoundPlayer("sound/9.wav");
                try
                {
                    sp.Play();
                }
                catch (FileNotFoundException ex)
                {
                    throw ex;
                }
            }
            else if (s == ".")
            {
                SoundPlayer sp = new SoundPlayer("sound/dian.wav");
                try
                {
                    sp.Play();
                }
                catch (FileNotFoundException ex)
                {
                    throw ex;
                }
            }
            Thread.Sleep(400);
        }
    }
}
