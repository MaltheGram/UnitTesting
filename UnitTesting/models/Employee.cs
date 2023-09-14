using System.Text.RegularExpressions;

namespace UnitTesting.models;

public class Employee
{
    private Regex rgx = new Regex(@"^[A-Za-z \-]{1,30}$");
    private string cpr, firstName, lastName, department, country;
    private int educationalLevel;
    private int baseSalary = 20000;
    private List<string> departments = new List<string>{
        "IT",
        "HR",
        "Finance",
        "Sales",
        "General Services"
    };
    
    private DateOnly birthDate, dateOfEmployeement;

    public Employee(
        string cpr, 
        string firstName, 
        string lastName, 
        string department, 
        string country, 
        int educationalLevel, 
        DateOnly birthDate, // Not needed. We can do calculations based on the CPR. Restricts system to DK.
        DateOnly dateOfEmployeement)
    {
        if (cpr != null && cpr.Length == 10 && cpr.All(char.IsDigit))
        {
            this.cpr = cpr;
        }
        else
        {
            throw new ArgumentException("CPR should be a 10-digit number");
        }

        var currentDate = DateOnly.FromDateTime(DateTime.Now);
        var age = currentDate.Year - birthDate.Year;
        
        if (birthDate > currentDate.AddYears(-age))
        {
            age--; // Adjust age if the birth date hasn't occurred yet this year
        }
        // Check if the age is less than 18
        if (age < 18 || (age == 18 && currentDate.DayOfYear < birthDate.DayOfYear))
        {
            throw new ArgumentException("Employee must be 18 years or older");
        }

        if (!departments.Contains(department))
        {
            throw new ArgumentException("Please use a valid department.");
        }
        else
        {
            this.department = department;
        }

        if (rgx.IsMatch(firstName) && rgx.IsMatch(lastName))
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }
        else
        {
            throw new ArgumentException("First & last name requirements:\nA minimum of 1 and a maximum of 30 characters. The characters can be alphabetic, spaces or a dash");
        }
        
        this.dateOfEmployeement = dateOfEmployeement;
        this.country = country;
        this.educationalLevel = educationalLevel;
    }
    
    public string Cpr
    {
        get => cpr;
        set => cpr = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string FirstName
    {
        get => firstName;
        set => firstName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string LastName
    {
        get => lastName;
        set => lastName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Department
    {
        get => department;
        set => department = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Country
    {
        get => country;
        set => country = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int BaseSalary
    {
        get => baseSalary;
        set
        {
            if (value < 20000 || value > 100000)
            {
                throw new ArgumentException("Base salary must be between 20000 and 100000");
            }
            else
            {
                baseSalary = value;
            }
        }
    }


    public int EducationalLevel
    {
        get => educationalLevel;
        set => educationalLevel = value;
    }

    public DateOnly BirthDate
    {
        get => birthDate;
        set => birthDate = value;
    }

    public DateOnly DateOfEmployeement
    {
        get => dateOfEmployeement;
        set => dateOfEmployeement = value;
    }
}