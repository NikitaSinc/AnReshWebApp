using AnReshWebApp.Models;
using AnReshWebApp.Models.FilterEntity;
using AnReshWebApp.Repositories;
using AnReshWebApp.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnReshWebApp.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<AbstractDBFactory>().To<MSSQLFactory>().When(configCheck => DataBaseConfiguration.ChosenSQLConnection == DataBaseConfiguration.MSSQLConnection);
            Bind<ISQLCommands>().To<MSSQLCommands>().When(configCheck => DataBaseConfiguration.ChosenSQLConnection == DataBaseConfiguration.MSSQLConnection); 

            Bind<AbstractDBFactory>().To<MySQLFactory>().When(configCheck => DataBaseConfiguration.ChosenSQLConnection == DataBaseConfiguration.MySQLConnection); 
            Bind<ISQLCommands>().To<MySQLCommands>().When(configCheck => DataBaseConfiguration.ChosenSQLConnection == DataBaseConfiguration.MySQLConnection); 

            Bind<GenericRepository<DepartmentFilterModel, Department>>().To<DepartmentRepository>().WithConstructorArgument("tableName", AppConfiguration.DepartmentTableName);
            Bind<GenericRepository<EmployeeFilterModel, Employee>>().To<EmployeeRepository>().WithConstructorArgument("tableName", AppConfiguration.EmployeeTableName);
            Bind<GenericRepository<BaseFilterEntity, Skills>>().To<SkillsRepository>().WithConstructorArgument("tableName", AppConfiguration.SkillTableName);
            Bind<GenericRepository<BaseFilterEntity, Users>>().To<UsersRepository>().WithConstructorArgument("tableName", AppConfiguration.UserTableName);

        }
    }
}