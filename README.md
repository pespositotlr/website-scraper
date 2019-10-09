# Website Scraper
This is a program that will scrape a website (in this case google) and show where a certain input string can be found in which results. 
It can be used to check how well a company is doing in SEO for a certain set of search terms. When a search is performed it should show the user which of the top 100 results for their search contained their input text and show them a preview of the result. For example "1,5,10,99". If none found, it shows 0.

This was originally part of a coding challenge to create an over-designed application using .NET MVC for seeing where a company name appeared in the top 100 google search results for certain keywords.
Part of the requirements were to avoid using HTMLAgilityPack or a similar service to scrape the data on the website, but to still follow clean coding practices.

Also part of the requirements were to make it a single-page application using KnockoutJS to handle the front-end. I hadn't used Knockout before this and learned the basics over the weekend, so the implementation of Knockout functionality is fairly basic.

Created By: Peter Esposito

Date: 10/5/2019
