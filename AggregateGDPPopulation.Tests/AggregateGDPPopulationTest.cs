using System;
using Xunit;
using AggregateGDPPopulation;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace AggregateGDPPopulation.Tests
{
    public class UnitTest1
    {

        [Fact]
        async public void Test1()
        {  
            await Class1.AggregateAsync();
            var s = Class1.ReadFileAsync("../../../../AggregateGDPPopulation/output/output.json");
            var str = Class1.ReadFileAsync("../../../expected-output.json");
            
            string actual = await s;
            string expected=await str;
        Assert.Equal(actual, expected);
         }
}
 }