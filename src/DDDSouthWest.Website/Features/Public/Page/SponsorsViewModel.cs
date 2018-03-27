using System.Collections.Generic;

namespace DDDSouthWest.Website.Features.Public.Page
{
    public class SponsorsViewModel
    {
        public List<SponsorDto> Sponsors { get; set; }

        public SponsorsViewModel()
        {
            Sponsors = new List<SponsorDto>
            {
                new SponsorDto
                {
                    Title = "Scott Logic",
                    Url = "http://www.scottlogic.com",
                    DisplayUrl = "www.scottlogic.com",
                    Description = "At Scott Logic we create intuitive software applications for clients in complex domains. Our experienced UK-based consultants challenge assumptions, yet work in partnership, to deliver truly innovative solutions.​​",
                    LogoPath = "/images/sponsors/scott_logic.gif"
                },
                new SponsorDto
                {
                    Title = "Landmark",
                    Url = "https://www.landmark.co.uk",
                    DisplayUrl = "www.landmark.co.uk",
                    Description = "Landmark offer an innovative and sociable team environment, encouraging input throughout the whole software development life cycle from idea through to deployment. Celebrating success, and learning quickly from failure Landmark Developers have an important voice in how products/services are constructed and in future decision making, their contribution and ideas are truly valued.",
                    LogoPath = "/images/sponsors/landmark.png"
                },
                new SponsorDto
                {
                    Title = "IO Associates",
                    Url = "http://www.ioassociates.co.uk",
                    DisplayUrl = "www.ioassociates.co.uk",
                    Description = "iO Associates​​​ source the highest calibre technology & digital professionals for market leading clients across the UK.​",
                    LogoPath = "/images/sponsors/io_associates.gif"
                },
                new SponsorDto
                {
                    Title = "Endjin",
                    Url = "https://endjin.com",
                    DisplayUrl = "endjin.com",
                    Description = "Endjin specialises in digital transformations using Azure, Data & AI. We help organisations of all sizes, across many industries to transform how they invest, envisage, build, deploy, test, manage and grow new digital offerings.",
                    LogoPath = "/images/sponsors/endjin.png"
                }
            };
        }
    }

    public class SponsorDto
    {
        public string Title { get; set; }

        public string Description { get; set; }
        
        public string Url { get; set; }
        
        public string DisplayUrl { get; set; }
        
        public string LogoPath { get; set; }
    }
}