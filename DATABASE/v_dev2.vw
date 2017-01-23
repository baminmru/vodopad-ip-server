create or replace force view v_dev2 as
select CSHORT NodeName,ID_BD,ID_GRP,dev.cdevname,dev.cdevdesc from  bbuildings b
join bdevices d on b.id_bu= d.id_bu
join devices dev on d.id_dev= dev.id_dev
where d.HIDEROW=0;

