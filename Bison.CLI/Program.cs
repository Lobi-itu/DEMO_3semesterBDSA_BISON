using System;
using System.IO;
using System.Reflection.Metadata;
using CsvHelper;
using System.Globalization;

public class Program
{

    static void Main (string[] args)
    {
        try
        {   
            if(args[0] == "read")
            {
                var reader = new StreamReader("bison_observe_cli_db.csv");
                var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                
                var records = csv.GetRecords<Cheep>();
                Console.WriteLine(records);

                
                foreach (var r in records)
                {
                    DateTime time = DateTimeOffset.FromUnixTimeSeconds(r.Timestamp).DateTime;
                    Console.WriteLine(r.Author + " @ " + time + ": " + r.Observation.Trim('\"'));
                }
            } 
            
            else if(args[0] == "observe")
            {
                string path = "bison_observe_cli_db.csv";
                using (StreamWriter writer = File.AppendText(path))
                {
    
                    long LocalTime = DateTimeOffset.Now.ToUnixTimeSeconds() + 7200; //+7200 is to make the time match our time-zone
                    
                    writer.WriteLine(Environment.UserName + ",\"" +  args[1] + "\"," + LocalTime);
                    
                    writer.Close();
                }
                
            }

        } catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        } finally
        {
            //Do nothing
        }   
    }
}
 