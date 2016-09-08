create or replace force view v_dev2 as
select CSHORT NodeName,ID_BD,ID_GRP from  bbuildings b
join bdevices d on b.id_bu= d.id_bu where d.HIDEROW=0;

