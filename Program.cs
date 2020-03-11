using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio;
using NAudio.Wave;
using System.Xml.Serialization;
using System.IO;
namespace Beat_Detector
{

    class Program
    {


        [STAThread]
        static void Main(string[] args)
        {


            string filename = "";
        
                filename = args.Length == 0 ? MusicReader.prompt() :  args[0];
            var dest = args.Length < 2 ? MusicReader.SaveInFolder() : args[1];
            dest += Path.GetFileName(filename) + ".xml";
                Console.WriteLine("File Name = " + Path.GetFileName(filename));
                Console.WriteLine("Directed to = " + dest);
               // Console.WriteLine("Frequency Based BPM");

                Sample2Beat workit = new Sample2Beat();
                List<float> bpm = workit.MusicProcesser(filename);//MusicReader.BPM(filename);
            foreach (float f in bpm)
            {
                Console.WriteLine("BPM: " + f);
            }

            Console.WriteLine("\nGenerating Frequency Chain");
              //  List<float> chain = MusicReader.SignalChain(filename);

                
                BattleSheet bs = new BattleSheet();
                bs.NameOfSong = filename;
                bs.BPM = bpm;
                bs.SignalChain = workit.Signals;//MusicReader.Signals;
                bs.BeatTimeStamp = workit.BeatTimeStamps;//MusicReader.BeatTimeStamps;
                XmlSerializer write = new XmlSerializer(typeof(BattleSheet));
                
                System.IO.StreamWriter file = new System.IO.StreamWriter(dest);
                write.Serialize(file, bs);
                file.Close();
                
             
            

        }
    }
}
