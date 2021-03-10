using Hangfire;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgency.API.Helpers
{
    public static class Helpers
    {
        
        public async static Task<(byte[] filecontent, string filename)> SaveAsAsync(string data)
        {
            try
            {
                
                string Root = Directory.GetCurrentDirectory();
                string DirectoryName = Path.Combine(Root, "Exports", DateTime.Now.ToString("MM.yyyy-mm"));
                string FileName = DateTime.Now.ToString("dd.MM.yyyy-hhmmss") + ".txt";
                FileName = Path.Combine(DirectoryName, FileName);

                if (!Directory.Exists(DirectoryName))
                {
                    Directory.CreateDirectory(DirectoryName);
                }

                using (StreamWriter sw = new StreamWriter(FileName, true, System.Text.Encoding.Default))
                {
                    await sw.WriteLineAsync(data);
                }

                RecurringJob.AddOrUpdate(() => Directory.Delete(DirectoryName,true), Cron.Minutely);

                return (File.ReadAllBytes(FileName), FileName);

            } catch(IOException ex)
            {
                throw;
            }


        }
    }
}
