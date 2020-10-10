using System;
using System.IO;

namespace Phasmophobia_Save_Decoder
{

    public static class Decoder
    {
        private const string Salt = "CHANGE ME TO YOUR OWN RANDOM STRING";
        
        private static int Main(string[] args)
        {
            string filePath;
            
            if (args.Length < 1)
            {
                filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                           @"Low\Kinetic Games\Phasmophobia\saveData.txt";
                
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("The game's save file location couldn't be automatically determined, please provide a path to it as a commandline argument.");
                    return 1;
                }
                
                Console.WriteLine("Found the game's save file: " + filePath);
            }
            else
            {
                filePath = args[0];
                
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("File not found, check the path provided.");
                    return 1;
                }
            }
            
            string data = File.ReadAllText(filePath);
            Console.WriteLine("Successfully opened save file, creating a backup.");
            
            File.WriteAllText(filePath + ".bak", data);
            Console.WriteLine("Backup created, decoding the save file.");
            
            File.WriteAllText(filePath, Decode(data));
            Console.WriteLine("File decoded and saved successfully.");
            
            return 0;
        }

        private static string Decode(string data)
        {
            string decoded = "";
            
            for (int i = 0; i < data.Length; i++)
                decoded += (char) (data[i] ^ Salt[i % Salt.Length]);
            
            return decoded; 
        }
    }
}