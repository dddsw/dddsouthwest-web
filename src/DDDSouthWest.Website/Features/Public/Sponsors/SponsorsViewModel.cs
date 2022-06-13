using System.Collections.Generic;

namespace DDDSouthWest.Website.Features.Public.Sponsors
{
    public class SponsorsViewModel
    {
        public List<SponsorDto> Sponsors { get; set; }

        public SponsorsViewModel()
        {
            Sponsors = new List<SponsorDto>
            {
                //new SponsorDto
                //{
                //    Title = "Scott Logic",
                //    Url = "http://www.scottlogic.com",
                //    DisplayUrl = "www.scottlogic.com",
                //    Description = "At Scott Logic we create intuitive software applications for clients in complex domains. Our experienced UK-based consultants challenge assumptions, yet work in partnership, to deliver truly innovative solutions.​​",
                //    LogoPath = "/images/sponsors/scott_logic.gif"
                //},
                //new SponsorDto
                //{
                //    Title = "Landmark",
                //    Url = "https://www.landmark.co.uk",
                //    DisplayUrl = "www.landmark.co.uk",
                //    Description = "Landmark offer an innovative and sociable team environment, encouraging input throughout the whole software development life cycle from idea through to deployment. Celebrating success, and learning quickly from failure Landmark Developers have an important voice in how products/services are constructed and in future decision making, their contribution and ideas are truly valued.",
                //    LogoPath = "/images/sponsors/landmark.png"
                //},
                new SponsorDto
                {
                    Title = "IO Associates",
                    Url = "http://www.ioassociates.co.uk",
                    DisplayUrl = "www.ioassociates.co.uk",
                    Description = "iO Associates​​​ source the highest calibre technology & digital professionals for market leading clients across the UK.​",
                    LogoPath = "/images/sponsors/io_associates.png"
                },
                new SponsorDto
                {
                    Title = "NDC London",
                    Url = "https://ndc-london.com",
                    LogoPath = "/images/sponsors/ndc_london.png"
                },
                new SponsorDto
                {
                    Title = "Elastic Mint",
                    Url = "https://www.elasticmint.com/",
                    LogoPath = "/images/sponsors/elastic_mint.png"
                },
                new SponsorDto
                {
                    Title = "BJSS",
                    Url = "https://www.bjss.com/",
                    LogoPath = "/images/sponsors/bjss.svg"
                },
                new SponsorDto
                {
                    Title = "ALD Automotive",
                    Url = "https://www.aldautomotive.com/",
                    LogoPath = "/images/sponsors/ALD.jpg"
                },
                new SponsorDto
                {
                    Title = "SECCL",
                    Url = "https://seccl.tech/",
                    LogoPath = "/images/sponsors/seccl.svg"
                },
                new SponsorDto
                {
                    Title = "Avanade",
                    Url = "https://www.avanade.com/en-gb",
                    LogoPath = "/images/sponsors/avanade.svg"
                },
                new SponsorDto
                {
                    Title = "UK Hydrographic Office",
                    Url = "https://www.gov.uk/government/organisations/uk-hydrographic-office",
                    LogoPath = "/images/sponsors/ukho.svg"
                },
                new SponsorDto
                {
                    Title = "Elastic",
                    Url = "https://www.elastic.co/",
                    LogoPath = "/images/sponsors/elastic.png" 
                },
                new SponsorDto
                {
                    Title = "Warp",
                    Url = "https://www.warp.co.uk/",
                    LogoPath = "/images/sponsors/warp.jpg"
                },
                new SponsorDto
                {
                    Title = "Rock Solid Knowledge",
                    Url = "https://www.identityserver.com/",
                    LogoPath = "/images/sponsors/rock_solid.png"
                },
                new SponsorDto
                {
                    Title = "ClearBank",
                    Url = "https://clear.bank/",
                    LogoPath = "/images/sponsors/clearbank.png"
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