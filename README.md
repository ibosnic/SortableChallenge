# Sortable Auction Challenge
This repo provides my implementation of the Sortable coding challenge found here: https://github.com/sortable/auction-challenge .

# Running the Application With Docker
You can run the code with the following commands:
```bash
$ docker build -t challenge .
$ docker run -i -v /path/to/test/config.json:/auction/config.json challenge < /path/to/test/input.json
```
When running the docker commands make sure that the docker ignore file is included as it stops copying of unit testing projects 
and bin folders which would break the build. Examples of the config and input are provided in the config/input.json.  

# Application Details
This application is written as C# .NET Core Console Application that takes in a list of auction sites and their bids for each unit in the auction site and 
returns a list of winning bids for each auction site in the output console as JSON. It does this by going through each bid for an auction site, tracking 
what unit it's for and if it is the largest bid for that specific unit. The caveat being that each bid is a valid bid and that the auction site is valid.

#### Assumptions Made
* There can be no duplicate units for each auction site to bid for
* There can be no duplicate bidders configured. Bidder can only be configured once.
* There can be no duplicate sites configured. Site can only be configured once.
* If two bids have the same price  and are candidates for the winning bid the first one is taken.

#### Frameworks Used
* XUnit - for unit testing 
* Json.NET - for json serializing and deserializing 
* LINQ - for data querying manipulation 