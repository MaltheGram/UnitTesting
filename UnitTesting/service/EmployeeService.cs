using UnitTesting.models;

namespace UnitTesting.service;

public class EmployeeService
{
    private int _actualSalary = 0;
    private double _employeeDiscount = 0;
    private readonly double _discountModifier = 0.5;
    private double _shippingCost = 1;
    
    public int getSalary(Employee e)
    {
     _actualSalary = e.BaseSalary + (e.EducationalLevel * 1220);
     return _actualSalary;
    }

    private int calculateEmploymentPeriod(Employee e)
    {
        
        DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);

        // Calculate the difference in years, including the current month and day
        int yearsDifference = currentDate.Year - e.DateOfEmployeement.Year;

        if (currentDate.Month < e.DateOfEmployeement.Month ||
            (currentDate.Month == e.DateOfEmployeement.Month && currentDate.Day < e.DateOfEmployeement.Day))
        {
            yearsDifference--;
        }

        if (yearsDifference < 0)
        {
            return 0;
        }
        return yearsDifference;
    }

    public double getDiscount(Employee e)
    {
        _employeeDiscount = calculateEmploymentPeriod(e) * 0.5;
        return _employeeDiscount;
    }

    public double getShipping(Employee e)
    {
        switch (e.Country)
        {
            case "Denmark":
            case "Norway":
            case "Sweden":
                _shippingCost = 0;
                break;
            case "Iceland":
            case "Finland":
                _shippingCost = 0.5;
                break;
        }

        return _shippingCost;
    }
}