export interface Department 
{
    Id:number,
    Pid:number,
    Childrens: Array<Department>
}