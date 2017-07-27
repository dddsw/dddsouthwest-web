using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Public.Page;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace DDDSouthWest.Tests.PageTests
{
    [Story(
        AsA = "As a page visitor",
        IWant = "I want to view a page",
        SoThat = "So I can see page information")]
    public class PageShouldReturnContent
    {
        private GetPage.Handler _handler;
        private GetPage.Reponse _response;
        
        private const string Filename = "about";
        
    	[Given("Given the page handler exits")]
    	void GivenTheHandlerExists()
	    {
	        var services = new ServiceCollection();

	        services.AddTransient<GetPage>();

		    var res = services.BuildServiceProvider();
	        
		    
	            
	            
	        res.Bui
	        _handler = new GetPage.Handler();
    	}
        
        async Task WhenTheUserRequestsAPage()
        {
            _response = await _handler.Handle(new GetPage.Query {Filename = Filename});
        }
        
        void ThenThePageShouldBeLoaded()
        {
            _response.Filename.ShouldBe(Filename);
            _response.BodyContent.ShouldNotBeNullOrEmpty();
        }
        
        [Fact]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}