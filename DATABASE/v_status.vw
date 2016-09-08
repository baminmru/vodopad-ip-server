create or replace force view v_status as
select bgroups.id_grp, bdevices.id_bd,  bgroups.cgrpnm as group_name, bbuildings.cshort as NODE,
case when (CCURR =1 and DNEXTCURR <sysdate) then ceil((sysdate-DNEXTCURR)*24 *60) else 0 end as CURTIME   ,
case when (CHOUR =1 and DNEXTHOUR <sysdate)then ceil((sysdate-DNEXTHOUR)*24 *60) else 0 end as HOURTIME ,
case when (C24 =1 and DNEXT24 <sysdate) then ceil((sysdate-DNEXT24)*24 *60) else 0 end as DAYTIME ,
case when (CSUM =1 and DNEXTSUM <sysdate) then ceil((sysdate-DNEXTSUM )*24 *60) else 0 end as TOTALTIME  ,
  analizer.COLOR,analizer.INFO,analizer.hmissing,analizer.dmissing
from plancall
join bdevices on  plancall.id_bd = bdevices.id_bd
join bbuildings on bdevices.id_bu = bbuildings.id_bu
join bgroups on bbuildings.id_grp = bgroups.id_grp
left join analizer on analizer.id_bd=   plancall.id_bd
where(bdevices.hiderow = 0 And bgroups.hiderow = 0
And (plancall.CSTATUS = 0 Or NPQUERY = 1))
order by bgroups.cgrpnm,bbuildings.cshort;

