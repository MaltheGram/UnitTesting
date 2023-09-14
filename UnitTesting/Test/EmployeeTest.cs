using NUnit.Framework.Internal;
using UnitTesting.models;
using UnitTesting.service;

namespace UnitTesting;

[TestFixture]
public class EmployeeTest
{
    private static Employee e = new Employee(
        "3001980000",
        "Malthe",
        "Gram",
        "IT",
        "Denmark",
        1,
        new DateOnly(1998,01,30),
        new DateOnly(
            DateOnly.FromDateTime(DateTime.Now).Year,
            DateOnly.FromDateTime(DateTime.Now).Month,
            DateOnly.FromDateTime(DateTime.Now).Day)
    );
    private static Employee e2 = new Employee(
        "3001980000",
        "Malthe",
        "Gram",
        "IT",
        "Finland",
        3,
        new DateOnly(1998, 01, 30),
        new DateOnly(2013, 01, 01)
    ); 
    private static Employee e3 = new Employee(
        "3001980000",
        "Malthe",
        "Gram",
        "IT",
        "Finland",
        3,
        new DateOnly(1998, 01, 30),
        new DateOnly(2027, 01, 01)
    );
    private static Employee e4 = new Employee(
        "3001980000",
        "Malthe",
        "Gram",
        "IT",
        "Finland",
        2,
        new DateOnly(1998, 01, 30),
        new DateOnly(
            DateOnly.FromDateTime(DateTime.Now).Year - 25,
            DateOnly.FromDateTime(DateTime.Now).Month,
            DateOnly.FromDateTime(DateTime.Now).Day)
    ); 
    
    private static EmployeeService employeeService;
    
    [SetUp]
    public static void Initialize()
    {
        e.BaseSalary = 50000;
        e2.BaseSalary = 20000;
        e3.BaseSalary = 99999;
        employeeService = new EmployeeService();
    }

    static IEnumerable<TestCaseData> EmployeeSalaryData()
    {
        yield return new TestCaseData(e, 51220).SetName("Test: Median base salary");
        yield return new TestCaseData(e2, 23660).SetName("Test: Lower boundary base salary");
        yield return new TestCaseData(e3,103659).SetName("Test: Upper boundary base salary");
    }

    [Test]
    [TestCaseSource(nameof(EmployeeSalaryData))]
    public void Should_Succeed_GetSalary(Employee testEmp, int expectedSalary)
    {
        int result = employeeService.getSalary(testEmp);
        Assert.That(result, Is.EqualTo(expectedSalary));
    }
    static IEnumerable<TestCaseData> EmployeeNegativeSalaryData()
    {
        yield return new TestCaseData(
            new Employee(
                "3001982123",
                "Malthe",
                "Gram",
                "IT",
                "Denmark",
                3,
                new DateOnly(1998, 01, 30),
                new DateOnly(2013, 01, 01)
            ),
            19999
        ).SetName("Test: Invalid lower boundary --> 19999");

        yield return new TestCaseData(
            new Employee(
                "3001982123",
                "Malthe",
                "Gram",
                "IT",
                "Denmark",
                3,
                new DateOnly(1998, 01, 30),
                new DateOnly(2013, 01, 01)
            ),
            100001
        ).SetName("Test: Invalid Upper boundary --> 100001");
    }

    [Test]
    [TestCaseSource(nameof(EmployeeNegativeSalaryData))]
    public void Should_Succeed_InvalidBaseSalary_ThrowArgumentException(Employee testEmp, int invalidBaseSalary)
    {
        Assert.Throws<ArgumentException>(() => testEmp.BaseSalary = invalidBaseSalary);
    }

    
    static IEnumerable<TestCaseData> EmployeeDiscountData()
    {
        yield return new TestCaseData(e, 0).SetName("Test: Hired Today");
        yield return new TestCaseData(e2, 5).SetName("Test: 10 Years employment");
        yield return new TestCaseData(e3, 0).SetName("Test: Future employment");
        yield return new TestCaseData(e4, 12.5).SetName("Test: 25 Year Anniversary Today");
    }
    [Test]
    [TestCaseSource(nameof(EmployeeDiscountData))]
    public void Should_Succeed_GetDiscount(Employee testEmp, double expectedDiscount)
    {
        double result = employeeService.getDiscount(testEmp);
        Assert.That(result, Is.EqualTo(expectedDiscount));
    }
    
    static Dictionary<Employee, double> EmployeesShippingData()
    {
        Employee employee1 = new Employee(
            "3001980000",
            "Malthe",
            "Gram",
            "IT",
            "Denmark",
            1,
            new DateOnly(1998, 01, 30),
            new DateOnly(2013, 01, 01)
        );

        Employee employee2 = new Employee(
            "1234567890",
            "John",
            "Doe",
            "HR",
            "Finland",
            2,
            new DateOnly(1990, 01, 15),
            new DateOnly(2020, 05, 20)
        );

        Employee employee3 = new Employee(
            "9876543210",
            "Jane",
            "Smith",
            "Sales",
            "Canada",
            3,
            new DateOnly(1985, 05, 20),
            new DateOnly(2015, 09, 10)
        );

        Employee employee4 = new Employee(
            "5678901234",
            "Alice",
            "Johnson",
            "Finance",
            "UK",
            0,
            new DateOnly(1982, 09, 10),
            new DateOnly(2010, 11, 30)
        );

        Employee employee5 = new Employee(
            "3456789012",
            "Bob",
            "Williams",
            "HR",
            "Australia",
            1,
            new DateOnly(1995, 11, 30),
            new DateOnly(2018, 03, 25)
        );
        Dictionary<Employee, double> employees = new Dictionary<Employee, double>
        {
            {employee1, 0},
            {employee2, 0.5},
            {employee3, 1},
            {employee4, 1},
            {employee5, 1},
            
        };

        return employees;
    }
    [Test]
    [TestCaseSource(nameof(EmployeesShippingData))]
    public void Should_Succeed_GetShipping(KeyValuePair<Employee, double> employeeTestData)
    {
        Employee employee = employeeTestData.Key;
        double employeeCountryDiscount = employeeTestData.Value;
        
        double result = employeeService.getShipping(employee);
        
        Assert.That(result, Is.EqualTo(employeeCountryDiscount));
    }

    [Test]
    public void Should_Succeed_EmployeeConstructor_CorrectData()
    {
        Assert.DoesNotThrow(() =>
        {
            Employee employee = new Employee(
                "3001980000",
                "Malthe",
                "Gram",
                "IT",
                "Denmark",
                1,
                new DateOnly(1998, 01, 30),
                new DateOnly(2013, 01, 01)
            );
        });
    }
    /*static IEnumerable<string[]> InvalidEmployeeData()
    {
        yield return new[] { "300198", "Malthe", "Gram", "IT", "Denmark", "1", "1998-01-30", "2013-01-01" };
        yield return new[] { "3001980000", "", "Gram", "IT", "Denmark", "1", "1998-01-30", "2013-01-01" };
        yield return new[] { "3001980000", "Malthe", "Gram", "IT", "Denmark", "1", DateTime.Now.ToString("yyyy-MM-dd"), "2013-01-01" };
    }
    [TestCaseSource(nameof(InvalidEmployeeData))]
    public void Should_Succeed_EmployeeConstructor_InvalidData(
        string cpr,
        string firstName,
        string lastName,
        string department,
        string country,
        string educationalLevel,
        string birthDate,
        string dateOfEmployment)
    {
        Assert.Throws<ArgumentException>(() => new Employee(
            cpr,
            firstName,
            lastName,
            department,
            country,
            int.Parse(educationalLevel),
            DateOnly.Parse(birthDate),
            DateOnly.Parse(dateOfEmployment)
        ));
    }*/
    
    [Test]
    public void Should_Succeed_EmployeeConstructor_InvalidCpr()
    {
        Assert.Throws<ArgumentException>(() => new Employee(
            "300198",
            "Malthe",
            "Gram",
            "IT",
            "Denmark",
            1,
            new DateOnly(1998, 01, 30),
            new DateOnly(2013, 01, 01)
        ));
    }
    [Test]
    public void Should_Succeed_EmployeeConstructor_InvalidName()
    {
        Assert.Throws<ArgumentException>(() => new Employee(
            "3001980000",
            "",
            "Gram",
            "IT",
            "Denmark",
            1,
            new DateOnly(1998, 01, 30),
            new DateOnly(2013, 01, 01)
        ));
    }
    [Test]
    public void Should_Succeed_EmployeeConstructor_InvalidAge()
    {
        Assert.Throws<ArgumentException>(() => new Employee(
            "3001980000",
            "Malthe",
            "Gram",
            "IT",
            "Denmark",
            1,
            new DateOnly(
                DateOnly.FromDateTime(DateTime.Now).Year,
                DateOnly.FromDateTime(DateTime.Now).Month,
                DateOnly.FromDateTime(DateTime.Now).Day),
            new DateOnly(2013, 01, 01)
        ));
    }
}