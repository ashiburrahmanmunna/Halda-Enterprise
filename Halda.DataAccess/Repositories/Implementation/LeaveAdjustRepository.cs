using Halda.Core.DTO.Attendance;
using Halda.Core.Models.Attendance;
using Halda.Core.Models.Onboarding;
using Halda.DataAccess.Persistence;
using Halda.DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories.Implementation
{
    public class LeaveAdjustRepository : BaseRepository<EmpLeaveAdjust, string>, ILeaveAdjustRepository
    {
        public LeaveAdjustRepository(HaldaDbContext dbContext) : base(dbContext)
        {

        }


        public async Task AddLeaveAdjustmentsAsync(LeaveAdjustmentDto model, string companyId, string userId, CancellationToken token)
        {
            if (!string.IsNullOrEmpty(model.EmpID))
            {
                // Check if an adjustment already exists for this employee on the same date
                var existingReplacement = await _dbContext.EmpLeaveAdjusts
                    .Where(e => e.EmpID == model.EmpID && e.AdjustDate == model.AdjustDate)
                    .FirstOrDefaultAsync(token);

                if (existingReplacement == null)
                {
                    // No duplicate found, proceed with saving the adjustment
                    var leaveAdjustment = new EmpLeaveAdjust
                    {
                        EmpID = model.EmpID,
                        AdjustType = model.AdjustType,
                        AdjustLeaveId = model.AdjustLeaveId,
                        AdjustDate = model.AdjustDate,
                        ReplaceDate = model.ReplaceDate,
                        Remarks = model.Remarks,
                        CompanyId = companyId,
                        UserId = userId
                    };

                    // Add the leave adjustment entry to the context
                    await _dbContext.EmpLeaveAdjusts.AddAsync(leaveAdjustment, token);
                }
                // Optionally, you could return some response to indicate the duplicate was skipped
            }
            else
            {
                // Handle department or designation selection
                var employeesQuery = _dbContext.Employees.AsQueryable();

                if (!string.IsNullOrEmpty(model.DepartmentId))
                {
                    employeesQuery = employeesQuery.Where(e => e.DepartmentId == model.DepartmentId);
                }

                if (!string.IsNullOrEmpty(model.DesignationId))
                {
                    employeesQuery = employeesQuery.Where(e => e.DesignationId == model.DesignationId);
                }

                var employees = await employeesQuery
                    .Where(e => e.CompanyId == companyId)
                    .ToListAsync(token);

                // Loop through each employee to create a leave adjustment entry
                foreach (var employee in employees)
                {
                    // Check if an adjustment already exists for this employee on the same date
                    var existingReplacement = await _dbContext.EmpLeaveAdjusts
                        .Where(e => e.EmpID == employee.Id && e.ReplaceDate == model.ReplaceDate)
                        .FirstOrDefaultAsync(token);

                    if (existingReplacement == null)
                    {
                        // No duplicate found, proceed with saving the adjustment
                        var leaveAdjustment = new EmpLeaveAdjust
                        {
                            EmpID = employee.Id,
                            AdjustType = model.AdjustType,
                            AdjustLeaveId = model.AdjustLeaveId,
                            AdjustDate = model.AdjustDate,
                            ReplaceDate = model.ReplaceDate,
                            Remarks = model.Remarks,
                            CompanyId = companyId,
                            UserId = userId
                        };

                        // Add the leave adjustment entry to the context
                        await _dbContext.EmpLeaveAdjusts.AddAsync(leaveAdjustment, token);
                    }
                }
            }
        }

        public async Task<EmpLeaveAdjust> GetLeaveAdjustmentByIdAsync(string empId, CancellationToken token)
        {
            return await _dbContext.EmpLeaveAdjusts
                .Where(e => e.EmpID == empId) // Only filter by EmpID
                .FirstOrDefaultAsync(token);
        }



        //public async Task AddLeaveAdjustmentsAsync(LeaveAdjustmentDto model, string companyId, string userId, CancellationToken token)
        //{
        //    if (!string.IsNullOrEmpty(model.EmpID))
        //    {
        //        // Handle the case where a single employee is selected
        //        var employee = await _dbContext.Employees
        //            .Where(e => e.CompanyId == companyId && e.Id == model.EmpID)
        //            .FirstOrDefaultAsync(token);

        //        if (employee != null)
        //        {
        //            var leaveAdjustment = new EmpLeaveAdjust
        //            {
        //                EmpID = employee.Id,
        //                AdjustType = model.AdjustType,
        //                AdjustLeaveId = model.AdjustLeaveId,
        //                AdjustDate = model.AdjustDate,
        //                ReplaceDate = model.ReplaceDate,
        //                Remarks = model.Remarks,
        //                CompanyId = companyId,
        //                UserId = userId
        //            };

        //            // Add the leave adjustment entry to the context
        //            await _dbContext.EmpLeaveAdjusts.AddAsync(leaveAdjustment, token);
        //        }
        //    }
        //    else
        //    {
        //        // Handle department or designation selection as before
        //        var employeesQuery = _dbContext.Employees.AsQueryable();

        //        if (!string.IsNullOrEmpty(model.DepartmentId))
        //        {
        //            employeesQuery = employeesQuery.Where(e => e.DepartmentId == model.DepartmentId);
        //        }

        //        if (!string.IsNullOrEmpty(model.DesignationId))
        //        {
        //            employeesQuery = employeesQuery.Where(e => e.DesignationId == model.DesignationId);
        //        }

        //        var employees = await employeesQuery
        //            .Where(e => e.CompanyId == companyId)
        //            .ToListAsync(token);

        //        // Loop through each employee to create a leave adjustment entry
        //        foreach (var employee in employees)
        //        {
        //            var leaveAdjustment = new EmpLeaveAdjust
        //            {
        //                EmpID = employee.Id,
        //                AdjustType = model.AdjustType,
        //                AdjustLeaveId = model.AdjustLeaveId,
        //                AdjustDate = model.AdjustDate,
        //                ReplaceDate = model.ReplaceDate,
        //                Remarks = model.Remarks,
        //                CompanyId = companyId,
        //                UserId = userId
        //            };

        //            // Add the leave adjustment entry to the context
        //            await _dbContext.EmpLeaveAdjusts.AddAsync(leaveAdjustment, token);
        //        }
        //    }
        //}


        public async Task<List<EmpLeaveAdjust>> GetLeaveAdjustListAsync(string searchTerm, string startDate, string endDate, string companyId, CancellationToken token, int page = 1, int size = 10)
        {
            var query = _dbContext.EmpLeaveAdjusts
                .Include(e => e.Emp)
                .ThenInclude(emp => emp.Department)
                .Include(e => e.Emp.Designation)
                .Include(e => e.AdjustLeave)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(x => x.AdjustType.ToLower().Contains(searchTerm) ||
                                         x.AdjustLeaveId.ToLower().Contains(searchTerm) ||
                                         x.Emp.FirstName.ToLower().Contains(searchTerm) ||
                                          x.Emp.EmployeeCode.ToLower().Contains(searchTerm) ||
                                         x.Emp.LastName.ToLower().Contains(searchTerm));
            }

            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                var start = DateOnly.Parse(startDate);
                var end = DateOnly.Parse(endDate);
                query = query.Where(x => x.AdjustDate >= start && x.AdjustDate <= end);
            }

            query = query.Where(x => x.CompanyId == companyId);

            return await query
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(token);
        }

        public async Task<int> GetTotalRecordCountAsync(string searchTerm, string startDate, string endDate, string companyId, CancellationToken token)
        {
            var query = _dbContext.EmpLeaveAdjusts.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(x => x.AdjustType.ToLower().Contains(searchTerm) ||
                                         x.AdjustLeaveId.ToLower().Contains(searchTerm) ||
                                         x.Emp.FirstName.ToLower().Contains(searchTerm) ||
                                         x.Emp.LastName.ToLower().Contains(searchTerm));
            }

            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                var start = DateOnly.Parse(startDate);
                var end = DateOnly.Parse(endDate);
                query = query.Where(x => x.AdjustDate >= start && x.AdjustDate <= end);
            }

            query = query.Where(x => x.CompanyId == companyId);

            return await query.CountAsync(token);
        }



    }
}
