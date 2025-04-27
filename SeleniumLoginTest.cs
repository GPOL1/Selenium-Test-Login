using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

//Test File to check main functions
public class Tests
{
    IWebDriver driver = Login.startUp();

    public void assertCheck(string flashClass){
        if (flashClass.Contains("success"))
        {
            Console.WriteLine("Login Successful!");
            Assert.Pass();
        }
        else if (flashClass.Contains("error"))
        {
            Console.WriteLine("Login Failed!");
            Assert.Fail();
        }
        else
        {
            Assert.Fail("Unknown error!");
        }
    }

    [Test]
    public void test1()
    {
        string flashClass = Login.test(driver, "username", "password");
        assertCheck(flashClass);
    }

    [Test]
    public void test2()
    {
        string flashClass = Login.test(driver, "tomsmith", "SuperSecretPassword!");
        assertCheck(flashClass);
    }

    [Test]
    public void test3()
    {
        string flashClass = Login.test(driver, "John", "Dragon");
        assertCheck(flashClass);
    }

    [Test]
    public void test4()
    {
        string flashClass = Login.test(driver, "Shash", "qwerty");
        assertCheck(flashClass);
    }

    [Test]
    public void test5()
    {
        string flashClass = Login.test(driver, "stuGkim", "SmartAlek");
        assertCheck(flashClass);
    }

    [TearDown]
    public void cleanup(){
        Login.cleanup(driver);

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
        wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("button[type='submit']")));
    }

    [OneTimeTearDown]
    public void GlobalCleanup()
    {
        if (driver != null)
        {
            Thread.Sleep(2000);
            driver.Quit();
            driver.Dispose();
            driver = null;
            Console.WriteLine("Browser closed and disposed after all tests.");
        }
    }
}
