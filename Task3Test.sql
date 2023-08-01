clear screen
set termout off
set feedback off

set heading off
select '*****************TODO1*****************' from dual;
set heading on

select fypId, title, category, type, minStudents, maxStudents, status
from FYP
order by title;

set heading off
select '*****************TODO2*****************' from dual;
set heading on

-- select * 
-- from FYP 
-- where fypId={fypId};

set heading off
select '*****************TODO3*****************' from dual;
set heading on

select username, firstName || ' ' || lastName as name
from Faculty natural join Supervises
where fypId=1
order by lastName, firstname;


set heading off
select '*****************TODO4*****************' from dual;
set heading on

select distinct category
from FYP
order by category;

set heading off
select '*****************TODO5*****************' from dual;
set heading on

set heading off
select '*****************TODO6*****************' from dual;
set heading on


set heading off
select '*****************TODO7*****************' from dual;
set heading on

Select username, (firstName || ' ' || lastName) as NAME
from Faculty
order by lastName asc, firstName asc;

set heading off
select '*****************TODO8*****************' from dual;
set heading on

set heading off
select '*****************TODO9*****************' from dual;
set heading on

set heading off
select '*****************TODO10*****************' from dual;
set heading on

set heading off
select '*****************TODO11*****************' from dual;
set heading on

set heading off
select '*****************TODO12*****************' from dual;
set heading on


set heading off
select '*****************TODO13*****************' from dual;
set heading on

Select distinct groupId, groupCode, assignedFypId 
from ProjectGroup pg join Supervises s on s.fypId = pg.assignedfypid
where reader='cafarella' 
order by groupCode asc;


set heading off
select '*****************TODO14*****************' from dual;
set heading on

set heading off
select '*****************TODO15*****************' from dual;
set heading on

set heading off
select '*****************TODO16*****************' from dual;
set heading on


set heading off
select '*****************TODO17*****************' from dual;
set heading on

Select pg.groupId, priority, (s.firstName || ' ' || s.lastName) AS MEMBERS, pg.groupCode, (F.firstName || ' ' || F.lastName) AS READER, i.fypid, pg.assignedfypid
from ProjectGroup pg
left join Faculty F ON pg.reader = F.username
left join CSEStudent s on pg.groupId = s.groupId
join InterestedIn I on pg.groupId = I.groupid
where assignedFypId = 1 and I.fypid = pg.assignedfypid;
order by pg.groupCode ASC, s.lastName ASC, s.firstName ASC;

set heading off
select '*****************TODO18*****************' from dual;
set heading on

set heading off
select '*****************TODO19*****************' from dual;
set heading on

set heading off
select '*****************TODO20*****************' from dual;
set heading on


set heading off
select '*****************TODO21*****************' from dual;
set heading on

Select groupId, groupCode, fyp.fypId
from ProjectGroup pg join FYP fyp on pg.assignedFypId = fyp.fypId
where reader='chang'
order by groupCode asc;

set heading off
select '*****************TODO22*****************' from dual;
set heading on

set heading off
select '*****************TODO23*****************' from dual;
set heading on

set heading off
select '*****************TODO24*****************' from dual;
set heading on

set heading off
select '*****************TODO25*****************' from dual;
set heading on


set heading off
select '*****************TODO26*****************' from dual;
set heading on

Update RequirementGrades set proposalReport='30',progressReport='40', finalReport= '50', presentation='60' where facultyUserName in (Select username from Supervises where fypId=1) and studentUsername='frankfung';


set heading off
select '*****************TODO27*****************' from dual;
set heading on

set heading off
select '*****************TODO28*****************' from dual;
set heading on

set heading off
select '*****************TODO29*****************' from dual;
set heading on


set heading off
select '*****************TODO30*****************' from dual;
set heading on

Select fypId, title, category, type, minStudents, maxStudents
from FYP
where status='Available' and minStudents <= (Select count(*) from ProjectGroup natural join InterestedIn where fypId = FYP.fypId) and maxStudents >= (Select count(*) from ProjectGroup natural join InterestedIn where fypId = FYP.fypId) and groupId=1
order by title asc;


set heading off
select '*****************TODO31*****************' from dual;
set heading on

set heading off
select '*****************TODO32*****************' from dual;
set heading on

set heading off
select '*****************TODO33*****************' from dual;
set heading on

set heading off
select '*****************TODO34*****************' from dual;
set heading on

set heading off
select '*****************TODO35*****************' from dual;
set heading on

set heading off
select '*****************TODO36*****************' from dual;
set heading on

set heading off
select '*****************TODO37*****************' from dual;
set heading on

set heading off
select '*****************TODO38*****************' from dual;
set heading on


set heading off
select '*****************TODO39*****************' from dual;
set heading on

Select fypId, title, reader
from FYP fyp join ProjectGroup pg on pg.assignedFypId = fyp.fypId natural join CSEStudent
where username='chanv';

set heading off
select '*****************TODO40*****************' from dual;
set heading on

set heading off
select '*****************TODO41*****************' from dual;
set heading on

set heading off
select '*****************TODO42*****************' from dual;
set heading on

set heading off
select '*****************TODO43*****************' from dual;
set heading on

set heading off
select '*****************TODO44*****************' from dual;
set heading on

set heading off
select '*****************TODO45*****************' from dual;
set heading on


set heading off
select '*****************TODO46*****************' from dual;
set heading on

Select groupId, groupCode, assignedFypId
from ProjectGroup
where assignedFypId is not NULL
order by groupCode asc;
