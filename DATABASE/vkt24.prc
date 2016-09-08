create or replace procedure VKT24(pid_bd varchar2,pdcounter varchar2) is
m1s number(14,4);
m2s number(14,4);
v1s number(14,4);
v2s number(14,4);
t1s number(14,4);
t2s number(14,4);
p1s number(14,4);
p2s number(14,4);
q1s number(14,4);
q2s number(14,4);
dqs number(14,4);
nn  number(14,4);
d1  date;
begin
  select sum(t1),sum(t2),sum(p1),sum(p2),sum(m1),sum(m2),sum(dq),sum(q1),sum(q2),
              sum(v1),sum(v2),nvl(count(*),0)
      into t1s,t2s,p1s,p2s,m1s,m2s,dqs,q1s,q2s,v1s,v2s,nn
      from datacurr 
      where id_bd=pid_bd
          and id_ptype=3 and dcounter>=to_date(substr(pdcounter,1,8),'yyyymmdd')
          and dcounter<to_date(substr(pdcounter,1,8),'yyyymmdd')+1;
    select max(dcounter) into d1 from datacurr where id_bd=pid_bd
        and id_ptype=4 and dcounter=to_date(substr(pdcounter,1,8),'yyyymmdd');
    if d1 is null then
      insert into datacurr(id_bd,id_ptype,dcall,dcounter)
        values(pid_bd,4,to_date(substr(pdcounter,1,8),'yyyymmdd'),
                 to_date(substr(pdcounter,1,8),'yyyymmdd'));
    end if;
    if nn = 24 then
      update datacurr set t1=t1s/nn,t2=t2s/nn,p1=p1s/nn,p2=p2s/nn,
                          m1=m1s,m2=m2s,q1=q1s,q2=q2s,dq=dqs,v1=v1s,v2=v2s
       where id_bd=pid_bd
        and id_ptype=4 and dcounter=to_date(substr(pdcounter,1,8),'yyyymmdd');
    end if;
    commit;
end VKT24;
/

