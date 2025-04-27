using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

public class Login{
    public static IWebDriver startUp(){

        ChromeOptions options = new ChromeOptions();

        options.AddArgument("--guest");
        options.AddArgument("--start-maximized");

        IWebDriver driver = new ChromeDriver(options);
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");

        return driver;
    }

    public static void find(IWebDriver driver, string field, string text){
        var searchBox = driver.FindElement(By.Id(field));
        searchBox.SendKeys(text);
    }

    public static string test(IWebDriver driver, string name, string pass){
        Login.find(driver, "username", name);
        Login.find(driver, "password", pass);
        driver.FindElement(By.CssSelector("button[type='submit']")).Click();

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
        wait.Until(driver => driver.FindElement(By.Id("flash")).Displayed);

        var flash = driver.FindElement(By.Id("flash"));
        string flashClass = flash.GetAttribute("class");

        Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
        string filePath = $"Screenshot_{DateTime.Now:yyyyMMdd_HHmmss_fff}.png";

        screenshot.SaveAsFile(filePath);


        return flashClass;
    }

    public static void cleanup(IWebDriver driver){
        var flash = driver.FindElement(By.Id("flash"));
        string flashClass = flash.GetAttribute("class");

        if (flashClass.Contains("flash success"))
        {
            driver.FindElement(By.CssSelector("a.button.secondary.radius")).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("button.radius")));
        }
        driver.Navigate().Refresh();
    }
}