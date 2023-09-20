using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    [Test]
    public async Task InputFieldIsVisable()
    {
        await Page.GotoAsync("https://demo.playwright.dev/todomvc");

        var inputField = Page.GetByPlaceholder("What needs to be done?");
        await Expect(inputField).ToBeVisibleAsync();
        await Expect(inputField).ToBeEditableAsync();
        await Expect(inputField).ToBeEmptyAsync();
    }

    [Test]
    public async Task ToDoBannerIsVisable()
    {
        await Page.GotoAsync("https://demo.playwright.dev/todomvc");
        var toDoBanner = Page.GetByRole(AriaRole.Heading, new() { Name = "todos" });
        var header = Page.GetByText("This is just a demo of TodoMVC for testing, not the real TodoMVC app.");
        var link = Page.GetByRole(AriaRole.Link, new() { Name = "TodoMVC", Exact = true });
        
        await Expect(toDoBanner).ToBeVisibleAsync();
        await Expect(header).ToBeVisibleAsync();
        await Expect(link).ToBeVisibleAsync();
    }

    [Test]
    public async Task FillOutToDo()
    {
        await Page.GotoAsync("https://demo.playwright.dev/todomvc");

        var inputField = Page.GetByPlaceholder("What needs to be done?");

        await inputField.ClickAsync();
        await inputField.FillAsync("My ToDo Test Works!");
        await inputField.PressAsync("Enter");

        var toDoItem = Page.GetByText("My toDo Test Works!");

        await toDoItem.IsVisibleAsync();
    }
    
    [TestCase("What up big boy!", 500, 500)]
    [TestCase("What up small fry!", 200, 800)]
    public async Task FillOutToDoInlinedTest(string text, int width, int height)
    {
        await Page.SetViewportSizeAsync(width, height);
        await Page.GotoAsync("https://demo.playwright.dev/todomvc");

        var inputField = Page.GetByPlaceholder("What needs to be done?");

        await inputField.ClickAsync();
        await inputField.FillAsync(text);
        await inputField.PressAsync("Enter");

        var toDoItem = Page.GetByText(text);

        await toDoItem.IsVisibleAsync();
    }
    
    
}