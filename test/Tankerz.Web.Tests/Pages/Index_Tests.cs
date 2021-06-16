using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Tankerz.Pages
{
    public class Index_Tests : TankerzWebTestBase
    {
        [Fact]
        public async Task Welcome_Page()
        {
            var response = await GetResponseAsStringAsync("/");
            response.ShouldNotBeNull();
        }
    }
}
