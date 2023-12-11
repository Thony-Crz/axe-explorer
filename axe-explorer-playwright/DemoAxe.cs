using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Deque.AxeCore.Commons;
using Deque.AxeCore.Playwright;
using System.Collections;
using System.Threading.Tasks;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    private static IPlaywright playwright;
    private static IBrowser browser;
    private static IPage page;

    public static List<AxeSelector> AxeSelectorInclude
    {
        get
        {
            return new();
        }
    }
    public static List<AxeSelector> AxeSelectorExclude
    {
        get
        {
            return new()
            {
                new AxeSelector("[class^='k-']")
            };
        } 
    }

    [SetUp]
    public async Task Setup()
    {
        //using var playwright = await Playwright.CreateAsync();
        //browser = await playwright.Chromium.LaunchAsync();
        //page = await browser.NewPageAsync();

        // Configure your application login here
        //await page.GotoAsync("https://your-app-url");

        // Perform login actions if needed
        // For example: await page.FillAsync("#username", "your-username");
        //              await page.FillAsync("#password", "your-password");
        //              await page.ClickAsync("#login-button");
    }

    public static IEnumerable UrlToCkeck
    {
        get
        {
            yield return new TestCaseData("https://www.nvda.fr/");
            yield return new TestCaseData("https://demos.telerik.com/kendo-ui/accessibility/grid");
        }
    }

    [Test, TestCaseSource(nameof(UrlToCkeck))]
    public async Task CheckAxe(string urlToCkeck)
    {
        await Page!.GotoAsync(urlToCkeck);


        AxeRunContext axeRunContext = new AxeRunContext()
        {
            Include = AxeSelectorInclude,
            Exclude = AxeSelectorExclude
        };

        AxeResult axeResults = await Page!.RunAxe(axeRunContext);
        Assert.That(axeResults.Violations, Is.Null.Or.Empty);
    }
}