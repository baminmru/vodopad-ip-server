create or replace force view v_grp2 as
select CGRPNM GroupName,id_grp from bgroups
where id_grp in ( select id_grp from v_dev2) and bgroups.hiderow=0;

