using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AggregateGDPPopulation
{
    public class CountryGDPPop
    {
        public float GDP_2012 = 0;
        public float POPULATION_2012 = 0;
    }
    public class Class1
    {
       async public static Task AggregateAsync()
        {
            Dictionary<string,CountryGDPPop> result = new Dictionary<string, CountryGDPPop>();
            try
            {
                string filepath = @"../../../../AggregateGDPPopulation/data/datafile.csv";
                string mapperPath = @"../../../../AggregateGDPPopulation/continent.json";
                //string outputPath = @"../../../../AggregateGDPPopulation/output/output.json";
                Task<string> fileData = ReadFileAsync(filepath);
                Task<string> mapperData = ReadFileAsync(mapperPath);
                await Task.WhenAll(fileData, mapperData);    
                string fData = fileData.Result;
                string mData = mapperData.Result;
                string[] contents = fData.Replace("\"", string.Empty).Split('\n');
                // Dictionary<String, String> countryContinentMapper;
                string[] headerRows = contents[0].Split(',');
                Dictionary<string,string> mapper = JsonConvert.DeserializeObject<Dictionary<string, string>>(mData);
                int indexOfCountry = Array.IndexOf(headerRows, "Country Name");
                int indexOfPopulation = Array.IndexOf(headerRows, "Population (Millions) 2012");
                int indexOfGDP = Array.IndexOf(headerRows, "GDP Billions (USD) 2012");
                for (int i = 1; i < contents.Length; i++)
                {
                    string[] row = contents[i].Replace("\"", "").Split(',');
                    string nameOfContinent = mapper[row[indexOfCountry]];
                    if (!result.ContainsKey(nameOfContinent)) {
                        result.Add(nameOfContinent, new CountryGDPPop());

                    }
                    result[nameOfContinent].GDP_2012 += float.Parse(row[indexOfGDP]);
                    result[nameOfContinent].POPULATION_2012 += float.Parse(row[indexOfPopulation]);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            string json = JsonConvert.SerializeObject(result,Formatting.Indented);
            WriteDataAsync(@"../../../../AggregateGDPPopulation/output/output.json", json);

        }
        public static async Task<string> ReadFileAsync(string filepath)
        {
            string fileData = "";
            using (StreamReader streamReader = new StreamReader(filepath))
            {
                fileData = await streamReader.ReadToEndAsync();
            }
            return fileData;
        }
        public static async void WriteDataAsync(string filepath, string contents)
        {
            using (StreamWriter streamwriter = new StreamWriter(filepath))
            {
                await streamwriter.WriteAsync(contents);
            };

        }
    }
   

}
