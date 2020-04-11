select FirstName,LastName,BirthDate,Name,Semester 
from Student as st
inner join 
Enrollment as e
on st.IdEnrollment = e.IdEnrollment
inner join
Studies as s
on e.IdStudy = s.IdStudy

