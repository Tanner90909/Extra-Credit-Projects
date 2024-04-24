Imports System
Imports PuppeteerSharp

Module Program

    Public Sub Main(args As String())
        MainAsync(args).GetAwaiter().GetResult()
    End Sub
    Async Function MainAsync(args As String()) As Task
        ' Prompt the user for their name
        Console.WriteLine("Welcome to the Google Search Console!")
        Console.WriteLine("This app will search Google for your name and display the results.")
        Console.WriteLine("Please enter your name:")
        Dim name As String = Console.ReadLine()


        ' Launch a new browser and navigate to Google
        Dim browser = Await Puppeteer.LaunchAsync(New LaunchOptions With {
            .Headless = True,
            .ExecutablePath = "C:\Program Files\Google\Chrome\Application\chrome.exe"
        })

        Dim page = Await browser.NewPageAsync()
        Await page.GoToAsync("https://www.google.com")

        ' Search for the user's name
        Await page.TypeAsync("textarea[name=q]", name)
        Await page.Keyboard.PressAsync("Enter")
        Await page.WaitForNavigationAsync()

        ' Extract the search results
        Dim searchResults = Await page.EvaluateExpressionAsync(Of String())("Array.from(document.querySelectorAll('.g')).map(result => result.innerText)")
        
        ' Display the search results in users console
        For Each result As String In searchResults
            Console.WriteLine(result)
        Next

        'Tell your user how famous they are
        Console.WriteLine("Wow, you're a pretty famous dude!")
        Console.WriteLine("Press any key to close the browser")
        Await browser.CloseAsync()
    End Function
End Module
