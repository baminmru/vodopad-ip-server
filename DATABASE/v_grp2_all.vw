create or replace force view v_grp2_all as
select CGRPNM GroupName,id_grp from bgroups
where id_grp in ( select id_grp from v_dev2_all);

