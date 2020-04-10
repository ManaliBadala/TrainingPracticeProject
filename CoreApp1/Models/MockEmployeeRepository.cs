using EmployeeManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagment.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;
        
        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee(){ Id=1, Name="Mary" ,Department=Dept.HR , Email="mary@gmail.com"},
                new Employee(){ Id=2, Name="Alice" ,Department=Dept.IT , Email="alice@gmail.com"},
                new Employee(){ Id=3, Name="Jack" ,Department=Dept.Accounts , Email="jack@gmail.com"},
                new Employee(){ Id=4, Name="John" ,Department=Dept.Infra , Email="john@gmail.com"},
            };

        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        
        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e=>e.Id==Id);
        }

        public Employee Delete(int id)
        {
          Employee employee= _employeeList.FirstOrDefault(e => e.Id == id);
          if(employee != null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }


        public Employee Update(Employee employeeChanges)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == employeeChanges.Id);
            if (employee != null)
            {
                employee.Name= employeeChanges.Name;
                employee.Email = employeeChanges.Email;
                employee.Department = employeeChanges.Department;
            }
            return employee;
        }
    }
}
