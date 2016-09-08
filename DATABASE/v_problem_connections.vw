create or replace force view v_problem_connections as
select cshort,bdevices.transport count from bbuildings join bdevices on bbuildings.id_bu= bdevices.id_bu  where id_bd in (
select  distinct id_bd from datacurr where HC like '%Попытка%' and id_ptype=1 and dcall >sysdate-1  group by id_bd having count(*) >2)
order by cshort;

