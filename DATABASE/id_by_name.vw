create or replace force view id_by_name as
select b.cshort, d.id_bd
    from bbuildings b,bdevices d
   where d.id_bu=b.id_bu;

