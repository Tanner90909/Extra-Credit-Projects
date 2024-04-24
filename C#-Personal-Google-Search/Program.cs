using System;
using PuppeteerSharp;

class Program
{
    static async System.Threading.Tasks.Task Main(string[] args)
    {
        Console.WriteLine("Welcome to the Google Search Console");
        Console.WriteLine("This app will search Google for your name and display the results");
        Console.WriteLine("Please enter your name:");
        var name = Console.ReadLine();

        //Launch a new browser and navigate to Google
        var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true,
            ExecutablePath = "C:/Program Files/Google/Chrome/Application/chrome.exe"
        });
        var page = await browser.NewPageAsync();
        await page.GoToAsync("https://www.google.com");

        await page.TypeAsync("textarea[name=q]", name);
        await page.Keyboard.PressAsync("Enter");
        await page.WaitForNavigationAsync();

        //Get the search results
        var searchResults = await page.EvaluateExpressionAsync("Array.from(document.querySelectorAll('.g')).map(result => result.innerText)");

        //Display the search results in users console
        foreach (var result in searchResults)
        {
            Console.WriteLine(result);
        }

        Console.WriteLine("Wow, you're a pretty famous dude!");
        Console.WriteLine("Press any key to close the browser");
        await browser.CloseAsync();
    }
}
