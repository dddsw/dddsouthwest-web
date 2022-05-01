-- Insert test user
INSERT INTO Users (id, givenname, familyname, emailaddress, password, salt, isblocked, isactivated, roles, receivenewsletter, dateregistered) VALUES (59, null, null, 'admin@demo.com', 'VQsNxJhTyED9flOfRMf3FCS0l0eCYOmDdiAid9HUWwU=', '7509d9ab-f5c7-4a82-9b00-ac2cb414f846', false, true, '["registered","organiser","speaker"]', true, '2018-02-02 01:19:02.311662');

-- Insert test pages
INSERT INTO Pages (id, title, filename, body, bodymarkdown, bodyhtml, islive, isdeleted, pageorder, datecreated, lastmodified) VALUES (1, 'About', 'about', null, '# What is DDD South West?

DDD South West is a free one day technical event organised by developers for developers.

It is a day of learning, discussing, contributing and being part of the developer community in the South West.

Our goal is to provide free technical education, the opportunity to mix with peers and to make and develop relationships in the .NET community.

## By The Community, For The Community

## Getting Involved

DDD South West is a community event and there are several ways you can get involved:

*   Help us to advertise DDD South West by getting a badge and putting it on your website/blog.
*   If you have ever felt like speaking at a conference then DDD South West is looking for you. We are looking for experienced and new speakers alike to present 60 minute sessions. See the Call For Speakers page.
*   Want to give speaking a try but don''t fancy the full on 60 minute sessions? No problem, we are looking for members of the community to give grok talks or micro-presentations at lunchtime. Grok talks are 10 minute presentations on any subject relevant to the DDD South West audience. Micro-presentations are 20 slides of 20 seconds each (totaling 6 minutes 40 seconds) again on any subject relevant to the DDD South West audience. Want to know more? Email us on [groktalks@dddsouthwest.com](mailto:groktalks@dddsouthwest.com).
*   Don''t fancy the public speaking but still want to be part of it? Please get in touch as we may be looking for more volunteers to help organise the event!', '<h1>What is DDD South West?</h1>
<p>DDD South West is a free one day technical event organised by developers for developers.</p>
<p>It is a day of learning, discussing, contributing and being part of the developer community in the South West.</p>
<p>Our goal is to provide free technical education, the opportunity to mix with peers and to make and develop relationships in the .NET community.</p>
<h2>By The Community, For The Community</h2>
<h2>Getting Involved</h2>
<p>DDD South West is a community event and there are several ways you can get involved:</p>
<ul>
<li>Help us to advertise DDD South West by getting a badge and putting it on your website/blog.</li>
<li>If you have ever felt like speaking at a conference then DDD South West is looking for you. We are looking for experienced and new speakers alike to present 60 minute sessions. See the Call For Speakers page.</li>
<li>Want to give speaking a try but don''t fancy the full on 60 minute sessions? No problem, we are looking for members of the community to give grok talks or micro-presentations at lunchtime. Grok talks are 10 minute presentations on any subject relevant to the DDD South West audience. Micro-presentations are 20 slides of 20 seconds each (totaling 6 minutes 40 seconds) again on any subject relevant to the DDD South West audience. Want to know more? Email us on <a href="mailto:groktalks@dddsouthwest.com">groktalks@dddsouthwest.com</a>.</li>
<li>Don''t fancy the public speaking but still want to be part of it? Please get in touch as we may be looking for more volunteers to help organise the event!</li>
</ul>
', true, false, 0, '2018-01-03 23:15:06.084932', '2018-01-06 01:50:50.912653');

INSERT INTO Pages (id, title, filename, body, bodymarkdown, bodyhtml, islive, isdeleted, pageorder, datecreated, lastmodified) VALUES (2, 'Venue', 'venue', null, '# The Venue

## How to get to DDD South West

### By Train to Bristol Temple Meads station

The station is about 10 - 15 minutes walk from the venue. Leave the station through the car park - go past the main exit and out the door at the side of the building, across both the indoor & outdoor car parks - & turn left down the hill. You should be able to see the spire of St. Mary Redcliffe Church in the middle distance on the left as you cross the car park. Head for the church, turn left at the large roundabout and go up the hill to the right of the church. The Sixth Form Centre is on the left, opposite the Mercure Holland House hotel.

### By Bus to Bristol Bus Station

Then take bus no. A1 (bay 8 in bus station) to Redcliffe Way (stop Tn). Go up the hill to the right of the church. The Sixth Form Centre is on the left, opposite the Mercure Holland House hotel.

Or if you want to walk it takes about 20 mins (but it''s not a straight-forward route).

[http://www.firstgroup.com/ukbus/bristol_bath/](http://www.firstgroup.com/ukbus/bristol_bath/)

### By Car

The nearest car park is Portwall Lane Car Park (BS1 6NB), opposite the front of St. Mary Redcliffe Church & costs £5.00 for the day. From the car park, go up the hill to the right of the church. The Sixth Form Centre is on the left, opposite the Mercure Holland House hotel.

Wapping Wharf car park (BS1 6UD) is nearby and costs £3.50 for the day. From the car park, cross the road before crossing the bridge and go along the right hand side of the river. Cross the bridge by the Ostrich pub. Go up the steps to the left of the pub. Continue straight to Redcliff Hill. The Sixth Form Centre will be facing you on the other side of the road and a bit to the right.

[Bristol parking information](http://www.bristol.gov.uk/page/transport-and-streets/where-park-bristol)

## Hotels:

The nearest hotels are:

*   [Mercure Bristol Holland House Hotel and Spa](http://www.mercure.com/gb/hotel-6698-mercure-bristol-holland-house-hotel-and-spa/index.shtml)  
    Redcliffe Hill Bristol, England, BS1 6SQ United Kingdom  
*   [Doubletree by Hilton Bristol](http://doubletree3.hilton.com/en/hotels/united-kingdom/doubletree-by-hilton-hotel-bristol-city-centre-BRSRWDI/index.html)  
    Redcliffe Way Bristol, England, BS1 6NJ United Kingdom  
*   [Holiday Inn Express Bristol City Centre](http://www.ihg.com/holidayinnexpress/hotels/us/en/bristol/brsct/hoteldetail)  
    South End, Temple Gate House Bristol, England, BS1 6PL United Kingdom  

(Disclaimer: these instructions are provided for your convenience. We have tried to ensure that they are accurate but can take no responsibility for errors. This is not an exhaustive list of how to get to the venue, other routes (and hotels) are available)', '<h1>The Venue</h1>
<h2>How to get to DDD South West</h2>
<h3>By Train to Bristol Temple Meads station</h3>
<p>The station is about 10 - 15 minutes walk from the venue. Leave the station through the car park - go past the main exit and out the door at the side of the building, across both the indoor &amp; outdoor car parks - &amp; turn left down the hill. You should be able to see the spire of St. Mary Redcliffe Church in the middle distance on the left as you cross the car park. Head for the church, turn left at the large roundabout and go up the hill to the right of the church. The Sixth Form Centre is on the left, opposite the Mercure Holland House hotel.</p>
<h3>By Bus to Bristol Bus Station</h3>
<p>Then take bus no. A1 (bay 8 in bus station) to Redcliffe Way (stop Tn). Go up the hill to the right of the church. The Sixth Form Centre is on the left, opposite the Mercure Holland House hotel.</p>
<p>Or if you want to walk it takes about 20 mins (but it''s not a straight-forward route).</p>
<p><a href="http://www.firstgroup.com/ukbus/bristol_bath/">http://www.firstgroup.com/ukbus/bristol_bath/</a></p>
<h3>By Car</h3>
<p>The nearest car park is Portwall Lane Car Park (BS1 6NB), opposite the front of St. Mary Redcliffe Church &amp; costs £5.00 for the day. From the car park, go up the hill to the right of the church. The Sixth Form Centre is on the left, opposite the Mercure Holland House hotel.</p>
<p>Wapping Wharf car park (BS1 6UD) is nearby and costs £3.50 for the day. From the car park, cross the road before crossing the bridge and go along the right hand side of the river. Cross the bridge by the Ostrich pub. Go up the steps to the left of the pub. Continue straight to Redcliff Hill. The Sixth Form Centre will be facing you on the other side of the road and a bit to the right.</p>
<p><a href="http://www.bristol.gov.uk/page/transport-and-streets/where-park-bristol">Bristol parking information</a></p>
<h2>Hotels:</h2>
<p>The nearest hotels are:</p>
<ul>
<li><a href="http://www.mercure.com/gb/hotel-6698-mercure-bristol-holland-house-hotel-and-spa/index.shtml">Mercure Bristol Holland House Hotel and Spa</a><br />
Redcliffe Hill Bristol, England, BS1 6SQ United Kingdom</li>
<li><a href="http://doubletree3.hilton.com/en/hotels/united-kingdom/doubletree-by-hilton-hotel-bristol-city-centre-BRSRWDI/index.html">Doubletree by Hilton Bristol</a><br />
Redcliffe Way Bristol, England, BS1 6NJ United Kingdom</li>
<li><a href="http://www.ihg.com/holidayinnexpress/hotels/us/en/bristol/brsct/hoteldetail">Holiday Inn Express Bristol City Centre</a><br />
South End, Temple Gate House Bristol, England, BS1 6PL United Kingdom</li>
</ul>
<p>(Disclaimer: these instructions are provided for your convenience. We have tried to ensure that they are accurate but can take no responsibility for errors. This is not an exhaustive list of how to get to the venue, other routes (and hotels) are available)</p>
', true, false, 0, '2018-01-03 23:17:27.726334', '2018-01-03 23:31:10.757745');

INSERT INTO Pages (id, title, filename, body, bodymarkdown, bodyhtml, islive, isdeleted, pageorder, datecreated, lastmodified) VALUES (3, 'Code of Conduct', 'code-of-conduct', null, '# DDD South West Code of Conduct
All attendees, speakers, sponsors and volunteers at our conference are required to agree with the following code of conduct.

## Summary
Our conference is dedicated to providing a harassment-free conference experience for everyone, regardless of gender, age, sexual orientation, disability, physical appearance, body size, race, or religion (or lack thereof). We do not tolerate harassment of conference participants in any form. Sexual language and imagery is not appropriate for any conference venue, including talks, workshops, parties, Twitter and other online media. Conference participants violating these rules may be sanctioned or expelled from the conference without a refund at the discretion of the conference organisers.

## Code of Conduct
Harassment includes offensive verbal comments related to gender, age, sexual orientation, disability, physical appearance, body size, race, religion, sexual images in public spaces, deliberate intimidation, stalking, following, harassing photography or recording, sustained disruption of talks or other events, inappropriate physical contact, and unwelcome sexual attention.
Participants asked to stop any harassing behavior are expected to comply immediately.

Sponsors are also subject to the anti-harassment policy. In particular, sponsors should not use sexualised images, activities, or other material. Booth staff (including volunteers) should not use sexualised clothing/uniforms/costumes, or otherwise create a sexualised environment.

If a participant engages in harassing behavior, the conference organisers may take any action they deem appropriate, including warning the offender or expulsion from the conference with no refund.

If you are being harassed, notice that someone else is being harassed, or have any other concerns, please contact a member of conference staff immediately. Conference staff can be identified as they’ll be wearing the conference t-shirts.

Conference staff will be happy to help participants contact hotel/venue security or local police, provide escorts, or otherwise assist those experiencing harassment to feel safe for the duration of the conference. We value your attendance.

We expect participants to follow these rules at conference and workshop venues and conference-related social events.', '<h1>DDD South West Code of Conduct</h1>
<p>All attendees, speakers, sponsors and volunteers at our conference are required to agree with the following code of conduct.</p>
<h2>Summary</h2>
<p>Our conference is dedicated to providing a harassment-free conference experience for everyone, regardless of gender, age, sexual orientation, disability, physical appearance, body size, race, or religion (or lack thereof). We do not tolerate harassment of conference participants in any form. Sexual language and imagery is not appropriate for any conference venue, including talks, workshops, parties, Twitter and other online media. Conference participants violating these rules may be sanctioned or expelled from the conference without a refund at the discretion of the conference organisers.</p>
<h2>Code of Conduct</h2>
<p>Harassment includes offensive verbal comments related to gender, age, sexual orientation, disability, physical appearance, body size, race, religion, sexual images in public spaces, deliberate intimidation, stalking, following, harassing photography or recording, sustained disruption of talks or other events, inappropriate physical contact, and unwelcome sexual attention.
Participants asked to stop any harassing behavior are expected to comply immediately.</p>
<p>Sponsors are also subject to the anti-harassment policy. In particular, sponsors should not use sexualised images, activities, or other material. Booth staff (including volunteers) should not use sexualised clothing/uniforms/costumes, or otherwise create a sexualised environment.</p>
<p>If a participant engages in harassing behavior, the conference organisers may take any action they deem appropriate, including warning the offender or expulsion from the conference with no refund.</p>
<p>If you are being harassed, notice that someone else is being harassed, or have any other concerns, please contact a member of conference staff immediately. Conference staff can be identified as they’ll be wearing the conference t-shirts.</p>
<p>Conference staff will be happy to help participants contact hotel/venue security or local police, provide escorts, or otherwise assist those experiencing harassment to feel safe for the duration of the conference. We value your attendance.</p>
<p>We expect participants to follow these rules at conference and workshop venues and conference-related social events.</p>
', true, false, 0, '2018-01-06 01:48:46.381620', '2018-01-06 01:49:51.155571');

-- Insert team members
INSERT INTO Team (FullName, EmailAddress, PicturePath, YearJoined, Twitter) VALUES ('Hannah Price', 'hannahprice@dddsouthwest.com', '/images/team/hannah.png', 2021, 'Handalf1994');
INSERT INTO Team (FullName, EmailAddress, PicturePath, YearJoined) VALUES ('Lorraine Pearce', 'lorrainepearce@dddsouthwest.com', '/images/team/lorraine.jpg', 2020);
INSERT INTO Team (FullName, EmailAddress, PicturePath) VALUES ('Russell Day', 'russell.day@dddsouthwest.com', '/images/team/russ.png');
INSERT INTO Team (FullName, EmailAddress) VALUES ('Martyn Fewtrell', 'martynfewtrell@dddsouthwest.com');
INSERT INTO Team (FullName, EmailAddress, Twitter) VALUES ('Stuart Lang', 'stuartlang@dddsouthwest.com', 'stuartblang');
INSERT INTO Team (FullName, EmailAddress) VALUES ('Joseph Woodward', 'josephwoodward@dddsouthwest.com');