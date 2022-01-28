export interface Employee{
    Id: number,
    Full_name: string,
    Id_department: number,
    Skills: Array<number>,
    Salary: number
}

export interface EmployeeFilter{
    Id?: number,
    Full_name?: string,
    Departments?: Array<number>,
    Skills?: Array<number>,
    Salary?: number
}

export interface Paginator{
    LowIndex: number,
    TopIndex: number,
    RowsPerPage: number,
    LastPage: number,
    RowsCount: number,
    AVGSalary: number
}