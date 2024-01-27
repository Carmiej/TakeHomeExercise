using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;

namespace TakeHomeExercise
{
    public class Tests
    {
        IWebDriver driver = new EdgeDriver();


        [SetUp]
        public void Setup()
        {
            driver.Url = "https://www.saucedemo.com/v1/index.html";
        }

        [Test]
        public void VerifyCartButtonsDisplayed()
        {
            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(10));

            //Login
            //username and password are in plain text as they are not a secret
            driver.Manage().Window.Maximize();
            driver.FindElement(By.XPath("//input[@name='user-name']")).SendKeys("standard_user");
            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("secret_sauce");
            driver.FindElement(By.XPath("//input[@id='login-button']")).Click();

            //Wait for products page to Load
            wait.Until(drv => drv.FindElement(By.XPath("//div[contains(text(), 'Products')]")));

            //Click on add to cart for the backpack and navigate to cart.
            driver.FindElement(By.XPath("//a[@id='item_4_title_link']/../..//button")).Click();
            driver.FindElement(By.XPath("//div[@id='shopping_cart_container']//a")).Click();

            //Wait for Your Cart page to Load
            wait.Until(drv => drv.FindElement(By.XPath("//div[contains(text(), 'Your Cart')]")));

            //Assert that the 3 buttons are present on the page
            Assert.That(driver.FindElement(By.XPath("//button[contains(text(), 'REMOVE')]")).Displayed);
            Assert.That(driver.FindElement(By.XPath("//a[contains(text(), 'CHECKOUT')]")).Displayed);
            Assert.That(driver.FindElement(By.XPath("//a[contains(text(), 'Continue Shopping')]")).Displayed);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}