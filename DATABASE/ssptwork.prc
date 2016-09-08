create or replace procedure SSPTWORK(pprz varchar2,pid_bd varchar2,pid_ip varchar2,
         pdt varchar2,pptype varchar2,
         pp1 varchar2,pp2 varchar2,pt1 varchar2,pt2 varchar2,pv1 varchar2,
         pv2 varchar2,pv3 varchar2,pm1 varchar2,pm2 varchar2,pm3 varchar2,
         pq1 varchar2,pts1 varchar2,
         pp4 varchar2,pp5 varchar2,pt4 varchar2,pt5 varchar2,pv4 varchar2,
         pv5 varchar2,pv6 varchar2,pm4 varchar2,pm5 varchar2,pm6 varchar2,
         pq2 varchar2,pts2 varchar2,
         phc varchar2,
         psp_tb1 varchar2 default null,psp_tb2 varchar2 default null,
         psp varchar2 default null,
         phc1 varchar2 default null,phc2 varchar2 default null) is
d1  date;         
id1 number;
begin
  d1 := to_date(pdt,'yyyymmddhh24miss');
  select max(id_bd) into id1 from datawork 
    where id_bd=pid_bd and dcounter=d1 and id=pid_ip;
  if id1 is null then
    insert into datawork(id,dcounter,dcall,id_bd,id_ptype)
      values(pid_ip,d1,sysdate,pid_bd,pptype);
  end if;
  if pprz = '3' then
    update DATAWORK set p1=ToNumber(pp1),p2=ToNumber(pp2),t1=ToNumber(pt1),
           t2=ToNumber(pt2),dt12=ToNumber(pt1)-ToNumber(pt2),
           v1=ToNumber(pv1),v2=ToNumber(pv2),dv12=ToNumber(pv1)-ToNumber(pv2),
           v3=ToNumber(pv3),m1=ToNumber(pm1),m2=ToNumber(pm2),dm12=ToNumber(pm1)-ToNumber(pm2),
           m3=ToNumber(pm3),q1=ToNumber(pq1),tsum1=ToNumber(pts1),
           p4=ToNumber(pp4),p5=ToNumber(pp5),t4=ToNumber(pt4),t5=ToNumber(pt5),
           dt45=ToNumber(pt4)-ToNumber(pt5),
           v4=ToNumber(pv4),v5=ToNumber(pv5),dv45=ToNumber(pv4)-ToNumber(pv5),v6=ToNumber(pv6),
           m4=ToNumber(pm4),m5=ToNumber(pm5),dm45=ToNumber(pm4)-ToNumber(pm5),m6=ToNumber(pm6),
           q2=ToNumber(pq2),tsum2=ToNumber(pts2),hc=phc,
           sp=ToNumber(psp),sp_tb1=ToNumber(psp_tb1),sp_tb2=ToNumber(psp_tb2),
           hc_1=phc1,hc_2=phc2
      where dcounter=d1 and id_ptype=pptype and id_bd+0=pid_bd;
  end if;
  if pprz = '1' then
    update DATAWORK set p1=ToNumber(pp1),p2=ToNumber(pp2),t1=ToNumber(pt1),t2=ToNumber(pt2),
           dt12=ToNumber(pt1)-ToNumber(pt2),
           v1=ToNumber(pv1),v2=ToNumber(pv2),dv12=ToNumber(pv1)-ToNumber(pv2),
           v3=ToNumber(pv3),m1=ToNumber(pm1),m2=ToNumber(pm2),dm12=ToNumber(pm1)-ToNumber(pm2),
           m3=ToNumber(pm3),q1=ToNumber(pq1),tsum1=ToNumber(pts1),hc=phc,
           sp=ToNumber(psp),sp_tb1=ToNumber(psp_tb1),sp_tb2=ToNumber(psp_tb2),
           hc_1=phc1,hc_2=phc2
      where dcounter=d1 and id_ptype=pptype and id_bd+0=pid_bd;
  end if;
  if pprz = '2' then
    update DATAWORK set 
           p4=ToNumber(pp4),p5=ToNumber(pp5),t4=ToNumber(pt4),t5=ToNumber(pt5),
           dt45=ToNumber(pt4)-ToNumber(pt5),
           v4=ToNumber(pv4),v5=ToNumber(pv5),dv45=ToNumber(pv4)-ToNumber(pv5),v6=ToNumber(pv6),
           m4=ToNumber(pm4),m5=ToNumber(pm5),dm45=ToNumber(pm4)-ToNumber(pm5),m6=ToNumber(pm6),
           q2=ToNumber(pq2),tsum2=ToNumber(pts2),hc=phc,
           sp=ToNumber(psp),sp_tb1=ToNumber(psp_tb1),sp_tb2=ToNumber(psp_tb2),
           hc_1=phc1,hc_2=phc2
      where dcounter=d1 and id_ptype=pptype and id_bd+0=pid_bd;
  end if;
  Commit;
end SSPTWORK;
/

