select e.full_name, e.salary, d.name 
	from department as d, employee as e
where 
	e.id_department = d.id;