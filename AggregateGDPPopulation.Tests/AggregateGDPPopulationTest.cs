using System;
using Xunit;
using AggregateGDPPopulation;
using System.IO;
using Newtonsoft.Json.Linq;

namespace AggregateGDPPopulation.Tests
{
    public class UnitTest1
    {

        [Fact]
        public void Test1()
        {  
             Class1.Aggregate();
            string s = File.ReadAllText("../../../../AggregateGDPPopulation/output/output.json");
            string str = File.ReadAllText("../../../expected-output.json");
        Assert.Equal(s, str);
         }
}
 }