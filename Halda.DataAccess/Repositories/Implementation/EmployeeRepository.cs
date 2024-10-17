using Halda.Core.Const;
using Halda.Core.DTO;
using Halda.Core.DTO.Onboarding;
using Halda.Core.Models;
using Halda.Core.Models.Onboarding;
using Halda.DataAccess.Persistence;
using Halda.DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories.Implementation
{
    public class EmployeeRepository : BaseRepository<Employee, string>, IEmployeeRepository
    {
        public EmployeeRepository(HaldaDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Employee>> GetEmployess(string searchterm, DateTime? Dob, CancellationToken token)
        {


            var query = _dbContext.Employees.AsQueryable();
            if (!string.IsNullOrEmpty(searchterm))
            {
                query = query.Where(x => x.FirstName == searchterm);
            }

            if (Dob is not null)
            {
                query = query.Where(x => x.DOB == Dob);
            }

            var result =await query.ToListAsync(token);

            return result;
        }
        public async Task<bool> SaveEmployeeAsync(Employee model)
        {
            try
            {
                model.DOB = model.DOB.ToUniversalTime();
                model.PassportIssueDate = model.PassportIssueDate?.ToUniversalTime();

                await _dbContext.Employees.AddAsync(model);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Employee>> GetEmployeeListAsync(string searchTerm, string companyId, CancellationToken token, int page = 1, int size = 5)
        {

            var query = _dbContext.Employees
                .Include(x => x.Line)
                .Include(x => x.Floor)
                .AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(x => x.FirstName.ToLower().Contains(searchTerm) || x.PrimaryEmail.ToLower().Contains(searchTerm) ||
                        x.Line.LineName.ToLower().Contains(searchTerm) ||
                        x.Floor.FloorName.ToLower().Contains(searchTerm)
                );                
            }

            query = query.Where(x => x.CompanyId == companyId);
            int totalRecordCount = await query.CountAsync(token);

            int pageCount = (int)Math.Ceiling((decimal)totalRecordCount / size);

            // Paginate the results
            var employees = await query
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(token);

            // Return only the employee list as expected by the interface
            return employees;

        }

        public async Task<int> GetTotalRecordCountAsync(string searchTerm, string companyId, CancellationToken token)
        {           
            var query = _dbContext.Employees
                .Include(x => x.Line)
                .Include(x => x.Floor)
                .AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower(); 
                query = query.Where(x => x.FirstName.ToLower().Contains(searchTerm) || x.PrimaryEmail.ToLower().Contains(searchTerm) ||
                        x.Line.LineName.ToLower().Contains(searchTerm) ||
                        x.Floor.FloorName.ToLower().Contains(searchTerm)
                );               
            }

            return await query.CountAsync(token);
        }

        public async Task<EmpListResponseDTO> GetEmployeeDataAsync(string employeeId, CancellationToken token)
        {
            // Fetch the employee data by employeeId
            var employeeData = await _dbContext.Employees               
                .Where(x => x.Id == employeeId)
                .Select(x=>new EmpListResponseDTO
                {
                  Id=  x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                FatherName = x.FatherName,
                MotherName = x.MotherName,
                EmployeeCode = x.EmployeeCode,
                EmployeeType = x.EmployeeType,
                FingerId = x.FingerId,
                OldFingerId = x.OldFingerId,
                Gender = x.Gender,
                Religion = x.Religion,
                MeritalStatus = x.MeritalStatus,
                Nationality = x.Nationality,
                JoiningDate = x.JoiningDate.Value.ToString("dd-MMM-yyyy"),
                PrimaryEmail = x.PrimaryEmail,
                DOB = x.DOB.ToString("dd-MMM-yyyy"),
                NID = x.NID,
                PassportNumber = x.PassportNumber,
                PassportIssueDate = x.PassportIssueDate.Value.ToString("dd-MMM-yyyy"),
                PrimaryMNo = x.PrimaryMNo,
                SecMNo = x.SecMNo,
                EmergencyContact = x.EmergencyContact,
                DepartmentId = x.DepartmentId,
                DesignationId = x.DesignationId,
                DesigName = x.Designation.DesigName,
                DeptName = x.Department.DeptName,
                SecName = x.Section.SecName,
                SectionId = x.SectionId,
                LineId = x.LineId,
                LineName = x.Line.LineName,
                FloorId = x.FloorId,
                FloorName = x.Floor.FloorName,
                IsActive = x.IsActive,
                Location = x.Location
                })
                .FirstOrDefaultAsync(token);

            

            return employeeData;
        }

        public async Task<bool> IsEmployeeCodeExistsAsync(string employeeCode, string excludedEmployeeId = null)
        {
            var query = _dbContext.Employees.AsQueryable();

            // If we're updating an employee, exclude that employee from the check
            if (!string.IsNullOrEmpty(excludedEmployeeId))
            {
                query = query.Where(e => e.Id != excludedEmployeeId);
            }

            return await query.AnyAsync(e => e.EmployeeCode == employeeCode);
        }


        public async Task<bool> UpdateEmployeeAsync(Employee employee, CancellationToken token)
        {
            try
            {
                // Fetch the existing employee
                var existingEmployee = await _dbContext.Employees.FindAsync(new object[] { employee.Id }, token);

                if (existingEmployee == null)
                {
                    return false;
                }

                // Update fields
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.FatherName = employee.FatherName;
                existingEmployee.MotherName = employee.MotherName;
                existingEmployee.DOB = employee.DOB.ToUniversalTime();
                existingEmployee.Gender = employee.Gender;
                existingEmployee.Religion = employee.Religion;
                existingEmployee.MeritalStatus = employee.MeritalStatus;
                existingEmployee.Nationality = employee.Nationality;
                existingEmployee.NID = employee.NID;
                existingEmployee.PassportNumber = employee.PassportNumber;
                existingEmployee.PassportIssueDate = employee.PassportIssueDate?.ToUniversalTime();
                existingEmployee.PrimaryMNo = employee.PrimaryMNo;
                existingEmployee.SecMNo = employee.SecMNo;
                existingEmployee.EmergencyContact = employee.EmergencyContact;
                existingEmployee.PrimaryEmail = employee.PrimaryEmail;
                existingEmployee.SalaryProfileId = employee.SalaryProfileId;

                _dbContext.Employees.Update(existingEmployee);
                await _dbContext.SaveChangesAsync(token);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<List<SelectListdto>> GetAllEmployees(string searchTerm, CancellationToken token)
        {
            var query = _dbContext.Employees.AsQueryable();

            // Create a list to hold the results
            var resultList = new List<SelectListdto>();

            // Add the "Select All" option at the beginning
            resultList.Add(new SelectListdto
            {
                Id = "all", // or any appropriate identifier
                Text = "Select All" // Text for the "Select All" option
            });

            // Check if searchTerm is null or empty, if so load the first 10 employees
            if (string.IsNullOrEmpty(searchTerm))
            {
                var firstTenEmployees = await query.Take(10).Select(e => new SelectListdto
                {
                    Id = e.Id,
                    Text = $"{e.FirstName} {e.LastName}" // Concatenated name
                }).ToListAsync(token);

                // Add first ten employees to the result list after the "Select All" option
                resultList.AddRange(firstTenEmployees);
            }
            else
            {
                var lowerCaseSearchTerm = searchTerm.ToLower();
                var matchingEmployees = await query.Where(e =>
                    (e.FirstName + " " + e.LastName).ToLower().Contains(lowerCaseSearchTerm) ||
                    e.EmployeeCode.ToLower().Contains(lowerCaseSearchTerm))
                    .Select(e => new SelectListdto
                    {
                        Id = e.Id,
                        Text = $"{e.FirstName} {e.LastName}" // Concatenated name
                    }).ToListAsync(token);

                // Add matching employees to the result list after the "Select All" option
                resultList.AddRange(matchingEmployees);
            }

            return resultList;
        }


        //public async Task<List<Employee>> GetAllEmployeesByCompanyId(string companyId, CancellationToken token)
        //{
        //    return await _dbContext.Employees
        //        .Where(e => e.CompanyId == companyId)
        //        .ToListAsync(token);
        //}


    }
}




   


