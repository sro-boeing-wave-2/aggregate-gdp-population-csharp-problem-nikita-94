using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AggregateGDPPopulation
{
    public class CountryGDPPop
    {
        public float GDP_2012 = 0;
        public float POPULATION_2012 = 0;
    }
    public class Class1
    {
        public static void Aggregate()
        {
            Dictionary<string, CountryGDPPop> result = new Dictionary<string, CountryGDPPop>();
            try
            {
                string filename = @"../../../../AggregateGDPPopulation/data/datafile.csv";
                string[] contents = File.ReadAllText(filename).Replace("\"", string.Empty).Split('\n');
                // Dictionary<String, String> countryContinentMapper;
                string[] headerRows = contents[0].Split(',');
                Dictionary<string,string> mapper = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(@"../../../../AggregateGDPPopulation/continent.json"));
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
            File.WriteAllText(@"../../../../AggregateGDPPopulation/output/output.json", json);

        }
    }
   

}
