using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repository.IRepository
{
    public interface IDepartmentRepository
    {
        bool insertDepartment(DepartmentModel position);

        List<DepartmentModel> listDepartments();

        DepartmentModel viewDepartment(int id);

        DepartmentModel modifyDepartment(DepartmentModel position);

        bool deleteDepartment(int id);
    }
}
